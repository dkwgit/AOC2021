// -----------------------------------------------------------------------
// <copyright file="Vertex.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Dijkstra
{
    internal class Vertex : IComparable<Vertex>
    {
        internal Vertex(int row, int column, int entryCost, Graph graph) => (this.Row, this.Column, this.EntryCost, this.Graph) = (row, column, entryCost, graph);

        internal int Row { get; }

        internal int Column { get; }

        internal int EntryCost { get; }

        internal int Distance { get; set;  } = int.MaxValue;

        internal Graph Graph { get; }

        public int CompareTo(Vertex? other)
        {
            if (other == null)
            {
                return 1;
            }

            if (this.Distance < other.Distance)
            {
                return -1;
            }
            else if (this.Distance > other.Distance)
            {
                return 1;
            }
            else
            {
                if (this.Row < other.Row)
                {
                    return -1;
                }
                else if (this.Row > other.Row)
                {
                    return 1;
                }
                else
                {
                    if (this.Column < other.Column)
                    {
                        return -1;
                    }
                    else if (this.Column > other.Column)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

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
