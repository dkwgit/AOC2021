// -----------------------------------------------------------------------
// <copyright file="Operator.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Bits
{
    using System.Collections;
    using AOC2021.Models.Bits.ValueCalculator;

    internal abstract class Operator : Packet, IOperatorPacket
    {
        private long? value = null;

        internal Operator(int version, int type, SubPacketLengthDescriptor descriptor, Action<IPacket, int> packetRegistrationFunction)
           : base(version, type, packetRegistrationFunction)
        {
            this.LengthType = descriptor;

            this.Calculator = type switch
            {
                0 => new SumCalculator(),
                1 => new ProductCalculator(),
                2 => new MinimumCalculator(),
                3 => new MaximumCalculator(),
                5 => new GreaterThanCalculator(),
                6 => new LessThanCalculator(),
                7 => new EqualsCalculator(),
                _ => throw new InvalidDataException(),
            };
        }

        public SubPacketLengthDescriptor LengthType { get; }

        public List<IPacket> Children { get; } = new();

        public IValueCalculator Calculator { get; }

        public override long Value
        {
            get
            {
                if (!this.value.HasValue)
                {
                    throw new InvalidOperationException("Retrieving uninitialized value from operator.");
                }

                return this.value.Value;
            }
        }

        public void CalculateValue()
        {
            List<long> values = new();
            foreach (var child in this.Children)
            {
                values.Add(child.Value);
            }

            this.value = this.Calculator.Calculate(values);
        }
    }
}
