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
    using CommandLine;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Entry class.
    /// </summary>
    public class Program
    {
        private static ServiceProvider DIContainer { get;  } = SetupDependencyInjection();

        public static int Main(string[] args)
        {
            IEnumerable<IDay> daysToRun = ProcessArgs(args);
            Run(daysToRun);

            return 0;
        }

        private static void Run(IEnumerable<IDay> daysToRun)
        {
            DayRunner runner = DIContainer.GetRequiredService<DayRunner>();
            ResultChecker checker = DIContainer.GetRequiredService<ResultChecker>();

            Stopwatch sw = new();
            sw.Start();

            var results = runner.RunDays(daysToRun);

            sw.Stop();

            checker.CheckResults(results);

            Console.WriteLine($"\nTotal run time in ms: {sw.ElapsedMilliseconds}");
        }

        private class Options
        {
            [Option(
                'd',
                "days",
                Required = false,
                HelpText = "Run only particular days. Input as as optionally zero padded, space separated two digit strings, such as '--days 05' for day 5, or '--days 5 13' for days 5 and 13.")]
            public IEnumerable<string> Days { get; set; } = new List<string>();
        }

        /// <summary>
        /// If no switches passed in, return *all* days so that they can be run. Otherwise if "--days x [y] [z]" passed in, return only those days.
        /// If error in command line, will print help and exit.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns>All the days that should be run.</returns>
        private static IEnumerable<IDay> ProcessArgs(string[] args)
        {
            IEnumerable<IDay> daysToRun = DIContainer.GetServices<IDay>();

            ParserResult<Options> options = Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    if (o.Days.Any())
                    {
                        daysToRun = daysToRun.Where(d => o.Days.Any(dayString =>
                        {
                            if (dayString.Length == 1)
                            {
                                dayString = $"0{dayString}";
                            }

                            return d.GetType().Name == $"Day{dayString}";
                        }));
                    }
                });

            if (options.Tag == ParserResultType.NotParsed)
            {
                // Help text has already been displayed
                Environment.Exit(1);
            }

            return daysToRun;
        }

        private static ServiceProvider SetupDependencyInjection()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
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
                .AddTransient<IDay, Day06>()
                .AddTransient<IDay, Day07>()
                .AddTransient<IDay, Day08>()
                .AddTransient<IDay, Day09>()
                .AddTransient<IDay, Day10>()
                .AddTransient<IDay, Day11>()
                .AddTransient<ResultChecker>()
                .AddTransient<DayRunner>();
        }
    }
}