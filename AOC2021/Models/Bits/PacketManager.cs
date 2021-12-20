// -----------------------------------------------------------------------
// <copyright file="PacketManager.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Bits
{
    internal class PacketManager
    {
        public List<IPacket> Packets { get; } = new();

        internal void AddPacket(IPacket packet)
        {
            this.Packets.Add(packet);
        }
    }
}
