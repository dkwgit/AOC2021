// -----------------------------------------------------------------------
// <copyright file="Day14.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using System.Text;
    using AOC2021.Data;

    public class Day14 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        public Day14(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public override string GetDescription()
        {
            return "Polymers";
        }

        public override string Result1()
        {
            (string template, Dictionary<string, string> productionRules) = this.PrepData();

            int steps = 0;
            StringBuilder sb = new StringBuilder();
            while (steps++ < 10)
            {
                bool copyFirstItemOfPair = true;
                List<List<char>> pairs = template.ProduceWindows(2).ToList();

                foreach (var pair in pairs)
                {
                    string pairString = string.Join(string.Empty, pair.ToArray());
                    string pairProduces = productionRules[pairString];
                    if (copyFirstItemOfPair)
                    {
                        sb.Append(pair[0]);
                        copyFirstItemOfPair = false;
                    }

                    sb.Append(pairProduces);
                    sb.Append(pair[1]);
                }

                template = sb.ToString();

                sb.Clear();
            }

            var groups = template.GroupBy(x => x).Select(g => new { Key = g.Key, Count = g.Count() }).OrderBy(g => g.Count).ToArray();

            long result = groups[^1].Count - groups[0].Count;
            return result.ToString();
        }

        public override string Result2()
        {
            this.PrepData();

            long result = 0;
            return result.ToString();
        }

        public (string Template, Dictionary<string, string> Rules) PrepData()
        {
            string[] lines = this.datastore.GetRawData(this.GetName());

            string template = string.Empty;
            Dictionary<string, string> productionRules = new();

            string state = "start";
            foreach (string line in lines)
            {
                if (state == "start")
                {
                    template = line;
                    state = "rules";
                    continue;
                }

                if (line == string.Empty)
                {
                    continue;
                }

                string[] splits = line.Split(" -> ");
                productionRules.Add(splits[0], splits[1]);
            }

            return (template, productionRules);
        }
    }
}
