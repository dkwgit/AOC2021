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
        public static int Main(string[] args)
        {
            ServiceProvider serviceProvider = SetupDependencyInjection();
            IEnumerable<IDay> daysToRun = serviceProvider.GetServices<IDay>();
            daysToRun = FilterDaysByCommandLineArgs(args, daysToRun);

            Stopwatch sw = new();
            sw.Start();

            var results = serviceProvider.GetRequiredService<DayRunner>().RunDays(daysToRun);

            sw.Stop();

            serviceProvider.GetRequiredService<ResultChecker>().CheckResults(results);

            Console.WriteLine($"\nTotal run time in ms: {sw.ElapsedMilliseconds}");
            return 0;
        }

        public class Options
        {
            [Option(
                'd',
                "days",
                Required = false,
                HelpText = "Run only particular days. Input as as null padded, space separated two digit strings, such as '--days 05' for day 5, or '--days 05 13' for days 5 and 13.")]
            public IEnumerable<string>? Days { get; set; }
        }

        private static IEnumerable<IDay> FilterDaysByCommandLineArgs(string[] args, IEnumerable<IDay> daysToRun)
        {
            ParserResult<Options> options = Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    if (o.Days != null && o.Days.Count() > 0)
                    {
                        daysToRun = daysToRun.Where(d => o.Days.Any(dayString => d.GetType().Name == $"Day{dayString}"));
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
                .AddTransient<ResultChecker>()
                .AddTransient<DayRunner>();
        }
    }
}