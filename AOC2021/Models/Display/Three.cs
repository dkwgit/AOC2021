// -----------------------------------------------------------------------
// <copyright file="Three.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Display
{
    internal class Three : Digit, IDigit
    {
        internal Three(Display display, string signal)
           : base(display, signal, 3)
        {
        }
    }
}
