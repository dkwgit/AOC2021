// -----------------------------------------------------------------------
// <copyright file="ResultChecker.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021
{
    using System.Collections.Generic;
    using AOC2021.Data;

    /// <summary>
    /// Checks results of runs of days against known verified results. Also prints out exexution time for a day's run.
    /// </summary>
    public class ResultChecker
    {
        private readonly DataStore dataStore;

        public ResultChecker(DataStore datastore)
        {
            this.dataStore = datastore;
        }

        /// <summary>
        /// Checks passed in run results and tries to verify them against known results.
        /// </summary>
        /// <param name="runResults">The results of running a day(s).</param>
        public void CheckResults(List<RunResult> runResults)
        {
            List<RunResult> verifiedResults = this.dataStore.GetVerifiedResultData();

            foreach (RunResult runResult in runResults)
            {
                var verifiedResult = verifiedResults.Where(x => x.Day == runResult.Day).Select(x => x).FirstOrDefault();

                string result1Status = verifiedResult switch
                {
                    null => "Unverfied.",
                    { Result1: var r } when r == runResult.Result1 => "Correct.",
                    { Result1: var r } when r != runResult.Result1 => "False.",
                    _ => throw new InvalidDataException("Unexpected data condition."),
                };

                string result2Status = verifiedResult switch
                {
                    null => "Unverfied.",
                    { Result2: var r } when r == runResult.Result2 => "Correct.",
                    { Result2: var r } when r != runResult.Result2 => "False.",
                    _ => throw new InvalidDataException("Unexpected data condition."),
                };

                Console.WriteLine($"\nDay: {runResult.Day}:");
                Console.WriteLine($"\tResult1: {runResult.Result1}. Status: {result1Status}");
                Console.WriteLine($"\tResult2: {runResult.Result2}. Status: {result2Status}");
                Console.WriteLine($"\tExecution time:{runResult.ExecutionTime}.");
            }
        }
    }
}
