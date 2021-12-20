// -----------------------------------------------------------------------
// <copyright file="OperatorWithCountOfSubPackets.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Bits
{
    using System.Collections;

    internal class OperatorWithCountOfSubPackets : Operator
    {
        internal OperatorWithCountOfSubPackets(int version, int type, BitArray bits, Packet? parent, SubPacketLengthDescriptor descriptor, Action<IPacket> packetRegistrationFunction)
           : base(version, type, bits, parent, descriptor, packetRegistrationFunction)
        {
            int subPacketCount = 0;

            for (int i = 0; i < 11; i++)
            {
                subPacketCount |= bits[^(8 + i)] ? 1 : 0;
                if (i + 1 < 11)
                {
                    subPacketCount <<= 1;
                }
            }

            this.SubPacketCount = subPacketCount;
            this.consumedBits = 18; // 3 version bits + 3 type bits + 1 length type bit + 11 count bits.
            this.subPacketBits = bits.CopyBottomBits(bits.Length - this.consumedBits);
        }

        internal int SubPacketCount { get; }

        public override int ProcessChildPackets()
        {
            int totalBitsInChildPackets = 0;
            int subPacketsToProcess = this.SubPacketCount;
            while (subPacketsToProcess > 0)
            {
                IPacket childPacket = Packet.BuildPacket(this.SubPacketBits, this, this.PacketRegistrationFunction);
                if (childPacket is IOperatorPacket)
                {
                    totalBitsInChildPackets += (childPacket as IOperatorPacket).ProcessChildPackets();
                    totalBitsInChildPackets += childPacket.ConsumedBits;
                    if (this.subPacketBits.Length > totalBitsInChildPackets)
                    {
                        this.subPacketBits = this.SubPacketBits.CopyBottomBits(this.SubPacketBits.Length - totalBitsInChildPackets);
                    }
                }
                else if (childPacket is Literal)
                {
                    totalBitsInChildPackets += childPacket.ConsumedBits;
                    this.subPacketBits = this.SubPacketBits.CopyBottomBits(this.SubPacketBits.Length - childPacket.ConsumedBits);
                }
                else
                {
                    throw new NotImplementedException();
                }

                this.Children.Add(childPacket);
                subPacketsToProcess--;
            }

            return totalBitsInChildPackets;
        }
    }
}
