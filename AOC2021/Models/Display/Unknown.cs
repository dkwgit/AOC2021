// -----------------------------------------------------------------------
// <copyright file="Unknown.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Display
{
    internal class Unknown : Digit, IDigit
    {
        internal Unknown(Display display, string signal)
           : base(display, signal, -1)
        {
        }
    }
}
