// -----------------------------------------------------------------------
// <copyright file="Four.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Display
{
    internal class Four : Digit, IDigit
    {
        internal Four(Display display, string signal)
           : base(display, signal, 4)
        {
        }
    }
}
