// -----------------------------------------------------------------------
// <copyright file="Day16.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using System.Collections;
    using System.Collections.Generic;
    using AOC2021.Data;
    using AOC2021.Models.Bits;
    using FluentAssertions;

    public class Day16 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        private BitArray bits;

        public Day16(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public BitArray Bits => this.bits;

        internal Stack<IOperatorPacket> ParseStack { get; } = new();

        public override string GetDescription()
        {
            return "BITS";
        }

        internal int FulfillOperatorWithCount(int bitPosition)
        {
            this.ParseStack.Peek().Should().BeAssignableTo<OperatorWithCountOfSubPackets>();
            OperatorWithCountOfSubPackets op = (OperatorWithCountOfSubPackets)this.ParseStack.Peek();
            int countNeeded = op.SubPacketCount;
            bitPosition += op.ConsumedBits;
            while (countNeeded > 0)
            {
                IPacket packet = Packet.BuildPacket(this.Bits, bitPosition, op.PacketRegistrationFunction);
                Func<int> advanceByBits = packet switch
                {
                    Literal lit => () => bitPosition + lit.ConsumedBits,
                    Operator subOperator => () =>
                    {
                        this.ParseStack.Push(subOperator);
                        int newBitPosition = this.FulfillOperator(bitPosition);
                        return newBitPosition;
                    },
                    _ => throw new InvalidDataException(),
                };
                bitPosition = advanceByBits();
                countNeeded--;
            }

            return bitPosition;
        }

        internal int FulfillOperatorWithBitLength(int bitPosition)
        {
            this.ParseStack.Peek().Should().BeAssignableTo<OperatorWithFixedSubPacketBitLength>();
            OperatorWithFixedSubPacketBitLength op = (OperatorWithFixedSubPacketBitLength)this.ParseStack.Peek();
            bitPosition += op.ConsumedBits;
            int bitsNeeded = op.SubPacketBitLength;
            int currentBits = 0;

            while (currentBits < bitsNeeded)
            {
                IPacket packet = Packet.BuildPacket(this.Bits, bitPosition, op.PacketRegistrationFunction);
                Func<int> advanceByBits = packet switch
                {
                    Literal lit => () => bitPosition + lit.ConsumedBits,
                    Operator subOperator => () =>
                    {
                        this.ParseStack.Push(subOperator);
                        int newBitPosition = this.FulfillOperator(bitPosition);
                        return newBitPosition;
                    },
                    _ => throw new InvalidDataException(),
                };
                int newBitPosition = advanceByBits();
                currentBits += newBitPosition - bitPosition;
                bitPosition = newBitPosition;
            }

            return bitPosition;
        }

        public int FulfillOperator(int bitPosition)
        {
            IOperatorPacket packet = this.ParseStack.Peek();
            int newBitPosition = packet switch
            {
                OperatorWithCountOfSubPackets _ => this.FulfillOperatorWithCount(bitPosition),
                OperatorWithFixedSubPacketBitLength _ => this.FulfillOperatorWithBitLength(bitPosition),
                _ => throw new InvalidDataException(),
            };
            IOperatorPacket fulfilled = this.ParseStack.Pop();
            fulfilled.Should().BeSameAs(packet);
            return newBitPosition;
        }

        public override string Result1()
        {
            this.bits = this.PrepData();

            Console.WriteLine(Bits.Print());

            PacketManager packetManager = new();
            Dictionary<int, IPacket> positionsToPackets = new();

            void PackageRegistrationFunction(IPacket packet, int distanceFromTop)
            {
                packetManager.AddPacket(packet);
                positionsToPackets.Add(distanceFromTop, packet);
            }

            IPacket packet = Packet.BuildPacket(this.Bits, 0, PackageRegistrationFunction);
            packet.Should().BeAssignableTo<IOperatorPacket>();
            this.ParseStack.Push((IOperatorPacket)packet);
            this.FulfillOperator(0);

            int versionSum = 0;
            foreach (IPacket p in packetManager.Packets)
            {
                versionSum += p.Version;
            }

            long result = versionSum;
            return result.ToString();
        }

        public override string Result2()
        {
            long result = 0;
            return result.ToString();
        }

        internal BitArray PrepData()
        {
            string[] lines = this.datastore.GetRawData(this.GetName());
            lines.Length.Should().Be(1);
            string line = lines[0];
            Console.WriteLine(line);

            BitArray bits = new(Convert.FromHexString(line).Reverse().ToArray());
            return bits;
        }
    }
}
