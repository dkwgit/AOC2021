// -----------------------------------------------------------------------
// <copyright file="Day08.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using AOC2021.Data;

    /// <summary>
    /// Day 8:Crossed Wires.
    /// </summary>
    public class Day08 : IDay
    {
        private readonly DataStore datastore;

        public Day08(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public string Name { get; init; } = "Day08";

        public long Result1()
        {
            Observation[] observations = this.PrepData();
            var outcome = observations.SelectMany(o => o.Outputs).Where(x => x.Length == 7 || x.Length == 4 || x.Length == 3 || x.Length == 2).Select(x => x).ToArray();

            long result = outcome.Count();
            return result;
        }

        public long Result2()
        {
            Observation[] observations = this.PrepData();
            Dictionary<int, List<char>> digitsToSegments = new();

            Dictionary<int, List<int>> lengthsToDigits = new();
            lengthsToDigits.Add(2, new List<int> { 1 });
            lengthsToDigits.Add(3, new List<int> { 7 });
            lengthsToDigits.Add(4, new List<int> { 4 });
            lengthsToDigits.Add(5, new List<int> { 2, 3, 5 });
            lengthsToDigits.Add(6, new List<int> { 0, 6, 9 });
            lengthsToDigits.Add(7, new List<int> { 8 });

            foreach (var observation in observations)
            {
                digitsToSegments.Add(1, observation.Signals.Where(x => x.Length == 2).SelectMany(x => x.ToCharArray()).Select(x => x).ToList());
                digitsToSegments.Add(7, observation.Signals.Where(x => x.Length == 3).SelectMany(x => x.ToCharArray()).Select(x => x).ToList());
                digitsToSegments.Add(4, observation.Signals.Where(x => x.Length == 4).SelectMany(x => x.ToCharArray()).Select(x => x).ToList());
                digitsToSegments.Add(8, observation.Signals.Where(x => x.Length == 7).SelectMany(x => x.ToCharArray()).Select(x => x).ToList());


            }

            long result = 0;
            return result;
        }

        private Observation[] PrepData()
        {
            string[] lines = this.datastore.GetRawData("08");
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
