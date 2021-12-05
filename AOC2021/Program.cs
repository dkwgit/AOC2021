// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021
{
    using System;
    using System.Diagnostics;
    using FluentAssertions;
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
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            using var serviceProvider = serviceCollection.BuildServiceProvider();

            Stopwatch sw = new();
            sw.Start();

            Day01 day01 = serviceProvider.GetRequiredService<Day01>();
            day01.Result1().Should().Be(1167, "Day_01 has a wrong result 1");
            day01.Result2().Should().Be(1130, "Day_01 has a wrong result 2");
            day01.Result2Variant().Should().Be(1130, "Day_01 has a wrong result 2 variant");

            Day02 day02 = serviceProvider.GetRequiredService<Day02>();
            day02.Result1().Should().Be(1947824, "Day_02 has a wrong result 1");
            day02.Result2().Should().Be(1813062561, "Day_02 has a wrong result 2");

            Day03 day03 = serviceProvider.GetRequiredService<Day03>();
            day03.Result1().Should().Be(2967914, "Day_03 has a wrong result 1");
            day03.Result2().Should().Be(7041258, "Day_03 has a wrong result 2");

            Day04 day04 = serviceProvider.GetRequiredService<Day04>();
            day04.Result1().Should().Be(39984, "Day_04 has a wrong result 1");
            day04.Result2().Should().Be(8468, "Day_04 has a wrong result 2");

            Day05 day05 = serviceProvider.GetRequiredService<Day05>();
            day05.Result1().Should().Be(8111, "Day_05 has a wrong result 1");
            day05.Result2().Should().Be(22088, "Day_05 has a wrong result 2");

            sw.Stop();
            Console.WriteLine($"Total run time in ms: {sw.ElapsedMilliseconds}");
        }

        private static void ConfigureServices(IServiceCollection collection)
        {
            collection
                .AddSingleton<AOC2021.Data.DataStore>()
                .AddTransient<Day01>()
                .AddTransient<Day02>()
                .AddTransient<Day03>()
                .AddTransient<Day04>()
                .AddTransient<Day05>();
        }
    }
}