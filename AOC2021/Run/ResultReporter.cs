// -----------------------------------------------------------------------
// <copyright file="ResultReporter.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Run
{
    using System.Collections.Generic;
    using System.Drawing;
    using AOC2021.Data;
    using AOC2021.Days;
    using Pastel;

    /// <summary>
    /// Checks results of runs of days against known verified results. Also prints out exexution time for a day's run.
    /// </summary>
    public class ResultReporter
    {
        private readonly DataStore dataStore;

        public ResultReporter(DataStore datastore)
        {
            this.dataStore = datastore;
        }

        /// <summary>
        /// Checks passed in run results and tries to verify them against known results.
        /// </summary>
        /// <param name="runResults">The results of running a day(s).</param>
        public void CheckResults(List<RunResult> runResults)
        {
            List<VerifiedResult> verifiedResults = this.dataStore.GetVerifiedResultData();

            bool badResults = false;
            foreach (RunResult runResult in runResults)
            {
                Console.WriteLine($"\n{runResult.DayResults[0].DayName}: {runResult.DayDescription}");

                foreach (DayResult dayResult in runResult.DayResults)
                {
                    var verifiedValue = verifiedResults.Where(x => x.Day == runResult.DayName).Select(x => dayResult.ResultNumber == 1 ? x.Result1 : x.Result2).FirstOrDefault();

                    string resultStatus = verifiedValue switch
                    {
                        default(string) => "Unverified".Pastel(Color.Black).PastelBg("FFFFFF"),
                        string x when x == dayResult.Result => "Correct".Pastel(Color.Black).PastelBg("00FF00"),
                        string x when x != dayResult.Result => "False".Pastel(Color.Black).PastelBg("FF0000"),
                        _ => throw new InvalidDataException("Unexpected data condition."),
                    };

                    if (resultStatus.ToLower() == "False".ToLower())
                    {
                        badResults = true;
                    }

                    Console.WriteLine($"\tResult {dayResult.ResultNumber}: {dayResult.Result}. Status: {resultStatus}. ExecutionTime: {dayResult.ExecutionTime}ms. Type: {dayResult.Type}." + (dayResult.ResultDescription.Length > 0 ? dayResult.ResultDescription : string.Empty));
                }

                Console.WriteLine($"\tExecution time: {runResult.ExecutionTime}ms.");
            }

            if (badResults)
            {
                throw new Exception("At least one result is bad.");
            }
        }
    }
}
