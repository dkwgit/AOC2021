// -----------------------------------------------------------------------
// <copyright file="Day12.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// ------------------------12----------------------------------------------

namespace AOC2021.Days
{
    using System.Text;
    using AOC2021.Data;
    using AOC2021.Models.Graph;

    public class Day12 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        public Day12(DataStore datastore)
        {
            this.datastore = datastore;
        }

        private Dictionary<string, List<string>> ConnectedCaves { get; } = new();

        private HashSet<string> AllPaths { get; } = new();

        private HashSet<string> SmallCaves { get; } = new();

        public override string GetDescription()
        {
            return "Cave traversal";
        }

        public override long Result1()
        {
            this.PrepData();

            GraphNode caveStart = new(null, "start");
            caveStart.BuildGraph(new List<string>(), this.ConnectedCaves, this.AllPaths, "end", (string s) => char.IsLower(s[0]), string.Empty);

            long result = this.AllPaths.Count;
            return result;
        }

        public override long Result2()
        {
            this.AllPaths.Clear();
            this.PrepData();

            foreach (var smallCaveWithTwoVisits in this.SmallCaves)
            {
                GraphNode caveStart = new(null, "start");
                caveStart.BuildGraph(new List<string>(), this.ConnectedCaves, this.AllPaths, "end", (string s) => char.IsLower(s[0]), smallCaveWithTwoVisits);

                /*
                 * int countOfNodes = 0;
                 * caveStart.CountNodes("end", ref countOfNodes);
                 */
            }

            // Dedupe the paths before counting
            long result = this.AllPaths.Count;
            return result;
        }

        private void PrepData()
        {
            string[] lines = this.datastore.GetRawData(this.GetName());

            foreach (var line in lines)
            {
                var caves = line.Split("-");
                bool produceBothDirections = true;
                if (caves.Contains("start"))
                {
                    produceBothDirections = false;
                    var temp = new string[2];
                    temp[0] = "start";
                    temp[1] = caves[0] == "start" ? caves[1] : caves[0];
                    caves = temp;
                }

                if (caves.Contains("end"))
                {
                    produceBothDirections = false;
                    var temp = new string[2];
                    temp[1] = "end";
                    temp[0] = caves[0] == "end" ? caves[1] : caves[0];
                    caves = temp;
                }

                if (!this.ConnectedCaves.ContainsKey(caves[0]))
                {
                    this.ConnectedCaves[caves[0]] = new List<string>();
                }

                if (!this.ConnectedCaves[caves[0]].Contains(caves[1]))
                {
                    this.ConnectedCaves[caves[0]].Add(caves[1]);
                }

                if (produceBothDirections)
                {
                    if (!this.ConnectedCaves.ContainsKey(caves[1]))
                    {
                        this.ConnectedCaves[caves[1]] = new List<string>();
                    }

                    if (!this.ConnectedCaves[caves[1]].Contains(caves[0]))
                    {
                        this.ConnectedCaves[caves[1]].Add(caves[0]);
                    }
                }

                if (caves[0] != "start" && char.IsLower(caves[0][0]))
                {
                    this.SmallCaves.Add(caves[0]);
                }

                if (caves[1] != "end" && char.IsLower(caves[1][0]))
                {
                    this.SmallCaves.Add(caves[1]);
                }
            }
        }
    }
}
