// -----------------------------------------------------------------------
// <copyright file="DataStore.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// Handles data, such as input data or storing results.
    /// </summary>
    public class DataStore
    {
        private readonly Dictionary<string, string[]> cache;

        public DataStore()
        {
            this.cache = new();
        }

        /// <summary>
        /// Load input data for a day from a text file.
        /// </summary>
        /// <param name="key">A string such as "01" for day one.</param>
        /// <returns>All the lines of the input file in a list.</returns>
        internal string[] GetRawData(string key)
        {
            string filePath = $"./Data/InputData/Day_{key}.txt";
            if (!this.cache.ContainsKey(filePath))
            {
                this.cache.Add(filePath, File.ReadAllLines(filePath));
            }

            return this.cache[filePath];
        }
    }
}
