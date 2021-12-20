// -----------------------------------------------------------------------
// <copyright file="OperatorWithFixedSubPacketBitLength.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Bits
{
    using System.Collections;
    using System.Linq;

    internal class OperatorWithFixedSubPacketBitLength : Operator
    {
        internal OperatorWithFixedSubPacketBitLength(int version, int type, BitArray bits, Packet? parent, SubPacketLengthDescriptor descriptor, Action<IPacket> packetRegistrationFunction)
           : base(version, type, bits, parent, descriptor, packetRegistrationFunction)
        {
            int subPacketBitLength = 0;

            for (int i = 0; i < 15; i++)
            {
                subPacketBitLength |= bits[^(8 + i)] ? 1 : 0;
                if (i + 1 < 15)
                {
                    subPacketBitLength <<= 1;
                }
            }

            this.SubPacketBitLength = subPacketBitLength;
            this.consumedBits = 22;
            this.subPacketBits = bits.CopyBottomBits(bits.Length - this.consumedBits);
        }

        internal int SubPacketBitLength { get; }

        public override int ProcessChildPackets()
        {
            int totalBitsInChildPackets = 0;
            int bitsToConsume = this.SubPacketBitLength;
            while (bitsToConsume > 0)
            {
                IPacket childPacket = Packet.BuildPacket(this.subPacketBits, this, this.PacketRegistrationFunction);
                if (childPacket.Bits.Length >= bitsToConsume)
                {
                    if (childPacket is IOperatorPacket)
                    {
                        int bitConsumedByChildren = (childPacket as IOperatorPacket).ProcessChildPackets();
                        totalBitsInChildPackets += bitConsumedByChildren;
                        totalBitsInChildPackets += childPacket.ConsumedBits;
                        if (this.subPacketBits.Length > totalBitsInChildPackets)
                        {
                            this.subPacketBits = this.subPacketBits.CopyBottomBits(this.subPacketBits.Length - totalBitsInChildPackets);
                        }
                    }
                    else if (childPacket is Literal)
                    {
                        totalBitsInChildPackets += childPacket.ConsumedBits;
                        this.subPacketBits = this.SubPacketBits.CopyBottomBits(this.SubPacketBits.Length - childPacket.ConsumedBits);
                    }
                    else
                    {
                        throw new InvalidDataException();
                    }
                }

                bitsToConsume -= totalBitsInChildPackets;

                this.Children.Add(childPacket);
            }

            return totalBitsInChildPackets;
        }
    }
}
