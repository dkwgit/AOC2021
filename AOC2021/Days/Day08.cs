// -----------------------------------------------------------------------
// <copyright file="Day08.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using AOC2021.Data;
    using AOC2021.Models.Display;

    public class Day08 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        public Day08(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public override string GetDescription()
        {
            return "Scrambled LED Displays.";
        }

        public override string Result1()
        {
            Observation[] observations = this.PrepData();
            var outcome = observations.SelectMany(o => o.Outputs).Where(x => x.Length == 7 || x.Length == 4 || x.Length == 3 || x.Length == 2).Select(x => x).ToArray();

            long result = outcome.Length;
            return result.ToString();
        }

        public override string Result2()
        {
            long result = 0;
            Observation[] observations = this.PrepData();
            foreach (Observation o in observations)
            {
                Display display = new();
                foreach (var signal in o.Signals)
                {
                    display.AddSignal(signal);
                }

                int number = display.Solve(o.Outputs);
                result += number;
            }

            return result.ToString();
        }

        private Observation[] PrepData()
        {
            string[] lines = this.datastore.GetRawData(this.GetName());
            Observation[] observations = new Observation[lines.Length];

            int i = 0;
            foreach (string line in lines)
            {
                string[] halves = line.Split(" | ");
                observations[i++] = new Observation(halves[0].Split(" "), halves[1].Split(" "));
            }

            return observations;
        }

        internal record Observation(string[] Signals, string[] Outputs);
    }
}
