// -----------------------------------------------------------------------
// <copyright file="Graph.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Dijkstra
{
    using FluentAssertions;

    internal class Graph
    {
        internal Graph(int squareSize)
        {
            this.Vertices = new Vertex[squareSize, squareSize];
            this.Size = squareSize;
        }

        internal Vertex[,] Vertices { get; }

        internal HashSet<Vertex> Unvisited { get;  } = new();

        internal List<Vertex> UnvisitedWithKnownDistance { get; set; } = new();

        internal List<Vertex> Visited { get; } = new();

        internal int Size { get; }

        internal void AddVertex(Vertex v)
        {
            this.Vertices[v.Row, v.Column] = v;
            this.Unvisited.Add(v);
        }

        internal Vertex[] GetUnvisitedNeighbors(Vertex v)
        {
            List<Vertex> neighbors = new();

            if (v.Row > 0)
            {
                Vertex n = this.Vertices[v.Row - 1, v.Column];
                if (this.Unvisited.Contains(n))
                {
                    neighbors.Add(n);
                }
            }

            if (v.Row + 1 < this.Size)
            {
                Vertex n = this.Vertices[v.Row + 1, v.Column];
                if (this.Unvisited.Contains(n))
                {
                    neighbors.Add(n);
                }
            }

            if (v.Column > 0)
            {
                Vertex n = this.Vertices[v.Row, v.Column - 1];
                if (this.Unvisited.Contains(n))
                {
                    neighbors.Add(n);
                }
            }

            if (v.Column + 1 < this.Size)
            {
                Vertex n = this.Vertices[v.Row, v.Column + 1];
                if (this.Unvisited.Contains(n))
                {
                    neighbors.Add(n);
                }
            }

            return neighbors.ToArray();
        }

        internal Vertex Solve()
        {
            Vertex currentVertex = this.Vertices[0, 0];
            Vertex destinationVertex = this.Vertices[this.Size - 1, this.Size - 1];

            while (true)
            {
                Vertex[] neighbors = this.GetUnvisitedNeighbors(currentVertex);
                foreach (Vertex neighbor in neighbors)
                {
                    if (currentVertex.Distance + neighbor.EntryCost < neighbor.Distance)
                    {
                        neighbor.Distance = currentVertex.Distance + neighbor.EntryCost;
                        this.UnvisitedWithKnownDistance.Add(neighbor);
                    }
                }

                this.Visited.Add(currentVertex);
                this.Unvisited.Remove(currentVertex);
                if (currentVertex.Equals(destinationVertex))
                {
                    break;
                }

                this.UnvisitedWithKnownDistance.Sort();
                currentVertex = this.UnvisitedWithKnownDistance[0];
                this.UnvisitedWithKnownDistance.RemoveAt(0);
            }

            return currentVertex;
        }

        internal void ComposeFromSmallerGraph(Graph g)
        {
            int graphsPerRow = this.Size / g.Size;
            int graphsPerColumn = this.Size / g.Size;

            graphsPerRow.Should().Be(graphsPerColumn);
            (this.Size % g.Size).Should().Be(0);

            for (int bigRow = 0; bigRow < graphsPerRow; bigRow++)
            {
                for (int bigColumn = 0; bigColumn < graphsPerColumn; bigColumn++)
                {
                    for (int row = 0; row < g.Size; row++)
                    {
                        for (int column = 0; column < g.Size; column++)
                        {
                            Vertex gVertex = g.Vertices[row, column];
                            int transpositionCount = bigRow + bigColumn;
                            int newEntryCost = gVertex.EntryCost + transpositionCount;
                            while (newEntryCost > 9)
                            {
                                newEntryCost -= 9;
                            }

                            Vertex v = new Vertex((bigRow * g.Size) + row, (bigColumn * g.Size) + column, newEntryCost, this);
                            if (v.Row == 0 && v.Column == 0)
                            {
                                v.Distance = 0;
                            }

                            this.AddVertex(v);
                        }
                    }
                }
            }
        }
    }
}
