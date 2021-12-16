// -----------------------------------------------------------------------
// <copyright file="Day15.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using AOC2021.Data;
    using AOC2021.Models.Dijkstra;
    using FluentAssertions;

    public class Day15 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        public Day15(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public override string GetDescription()
        {
            return "Polymers";
        }

        public override string Result1()
        {
            Graph graph = this.PrepData();
            Vertex destination = graph.Solve();

            long result = destination.Distance;
            return result.ToString();
        }

        public override string Result2()
        {
            Graph graph = this.PrepData();
            Graph newGraph = new Graph(graph.Size * 5);
            newGraph.ComposeFromSmallerGraph(graph);
            Vertex destination = newGraph.Solve();

            long result = destination.Distance;
            return result.ToString();
        }

        internal Graph PrepData()
        {
            string[] lines = this.datastore.GetRawData(this.GetName());

            int squareSize = lines.Length;

            Graph graph = new Graph(squareSize);

            int y = 0;
            int x = 0;
            foreach (var row in lines)
            {
                var columns = row.ToCharArray();
                columns.Length.Should().Be(squareSize);

                x = 0;
                foreach (var column in columns)
                {
                    Vertex v = new Vertex(y, x, column - '0', graph);
                    if (y == 0 && x == 0)
                    {
                        v.Distance = 0;
                    }

                    graph.AddVertex(v);
                    x++;
                }

                y++;
            }

            return graph;
        }
    }
}
