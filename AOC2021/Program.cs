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
        public static void Main(/* string[] args*/)
        {
            ServiceProvider serviceProvider = SetupDependencyInjection();

            Stopwatch sw = new();
            sw.Start();

            var days = serviceProvider.GetServices<IDay>();
            var results = serviceProvider.GetRequiredService<DayRunner>().RunDays(days);

            sw.Stop();

            serviceProvider.GetRequiredService<ResultChecker>().CheckResults(results);

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
                .AddTransient<IDay, Day06>()
                .AddTransient<IDay, Day07>()
                .AddTransient<IDay, Day08>()
                .AddTransient<IDay, Day09>()
                .AddTransient<IDay, Day10>()
                .AddTransient<ResultChecker>()
                .AddTransient<DayRunner>();
        }

        private static ServiceProvider SetupDependencyInjection()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}