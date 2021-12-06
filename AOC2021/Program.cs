// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021
{
    using System;
    using System.Diagnostics;
    using AOC2021.Data;
    using AOC2021.Days;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Entry class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Standard console entry function.
        /// </summary>
        /// <!-- param name="args">Command line args.</param-->
        public static void Main(/* string[] args*/)
        {
            Stopwatch sw = new();
            sw.Start();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            using var serviceProvider = serviceCollection.BuildServiceProvider();

            var days = serviceProvider.GetServices<IDay>();
            DayRunner runner = new();
            List<RunResult> results = new();

            foreach (var day in days)
            {
                RunResult result = runner.RunDay(day);
                results.Add(result);
            }

            sw.Stop();
            ResultChecker checker = serviceProvider.GetRequiredService<ResultChecker>();
            checker.CheckResults(results);

            Console.WriteLine($"\nTotal run time in ms: {sw.ElapsedMilliseconds}");
        }

        private static void ConfigureServices(IServiceCollection collection)
        {
            collection
                .AddSingleton<DataStore>()
                .AddTransient<IDay, Day01>()
                .AddTransient<IDay, Day02>()
                .AddTransient<IDay, Day03>()
                .AddTransient<IDay, Day04>()
                .AddTransient<IDay, Day05>()
                .AddTransient<ResultChecker>();
        }
    }
}