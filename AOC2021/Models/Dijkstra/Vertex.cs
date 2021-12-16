// -----------------------------------------------------------------------
// <copyright file="Vertex.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Dijkstra
{
    internal class Vertex
    {
        internal Vertex(int row, int column, int entryCost, Graph graph) => (this.Row, this.Column, this.EntryCost, this.Graph) = (row, column, entryCost, graph);

        internal int Row { get; }

        internal int Column { get; }

        internal int EntryCost { get; }

        internal int Distance { get; set;  } = int.MaxValue;

        internal Graph Graph { get; }

        internal bool Equals(Vertex other)
        {
            if (other.Row != this.Row || other.Column != this.Column)
            {
                return false;
            }

            return true;
        }
    }
}
