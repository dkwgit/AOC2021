// -----------------------------------------------------------------------
// <copyright file="VertexComparer.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Dijkstra
{
    internal class VertexComparer : IEqualityComparer<Vertex>
    {
        public bool Equals(Vertex? p1, Vertex? p2)
        {
            if (p1 == null && p2 == null)
            {
                return true;
            }

            if (p1 == null || p2 == null)
            {
                return false;
            }

            return p1.Equals(p2);
        }

        public int GetHashCode(Vertex p)
        {
            /*int bitsInSize = (int)Math.Log(p.Graph.Size, 2.0) + 1;

            int hCode = p.Column ^ (p.Row << bitsInSize);
            return hCode.GetHashCode();*/
            return p.GetHashCode();
        }
    }
}
