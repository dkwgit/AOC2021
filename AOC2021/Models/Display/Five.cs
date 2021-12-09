// -----------------------------------------------------------------------
// <copyright file="Five.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Display
{
    internal class Five : Digit, IDigit
    {
        internal Five(Display display, string signal)
           : base(display, signal, 5)
        {
        }
    }
}
