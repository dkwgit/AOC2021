// -----------------------------------------------------------------------
// <copyright file="PathCounter.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Graph
{
    internal class PathCounter
    {
        private int count;

        internal int Count
        {
            get
            {
                return this.count;
            }
        }

        internal void Increment()
        {
            this.count++;
        }
    }
}
