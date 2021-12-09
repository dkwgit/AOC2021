// -----------------------------------------------------------------------
// <copyright file="Zero.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Display
{
    internal class Zero : Digit, IDigit
    {
        internal Zero(Display display, string signal)
           : base(display, signal, 0)
        {
        }
    }
}
