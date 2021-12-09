// -----------------------------------------------------------------------
// <copyright file="One.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Display
{
    internal class One : Digit, IDigit
    {
        internal One(Display display, string signal)
            : base(display, signal, 1)
        {
        }
    }
}
