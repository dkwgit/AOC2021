// -----------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1009:Closing parenthesis should be spaced correctly", Justification = "Wanted to get lambda on a single line.", Scope = "member", Target = "~M:AOC2021.Days.Day01.Result1~System.String")]
[assembly: SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1009:Closing parenthesis should be spaced correctly", Justification = "Wanted to get lambda on a single line.", Scope = "member", Target = "~M:AOC2021.Days.Day01.Result2~System.String")]
[assembly: SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1009:Closing parenthesis should be spaced correctly", Justification = "Wanted to get lambda on a single line.", Scope = "member", Target = "~M:AOC2021.Days.Day01.Result2Variant~System.Int64")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1111:Closing parenthesis should be on line of last parameter", Justification = "Wanted to get lambda on a single line.", Scope = "member", Target = "~M:AOC2021.Days.Day01.Result1~System.String")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1111:Closing parenthesis should be on line of last parameter", Justification = "Wanted to get lambda on a single line.", Scope = "member", Target = "~M:AOC2021.Days.Day01.Result2~System.String")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1111:Closing parenthesis should be on line of last parameter", Justification = "Wanted to get lambda on a single line.", Scope = "member", Target = "~M:AOC2021.Days.Day01.Result2Variant~System.Int64")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1106:Code should not contain empty statements", Justification = "I tried doing it differently. It was less clear and broke something.", Scope = "member", Target = "~M:AOC2021.Days.Day04.PrepData~System.ValueTuple{System.Int32[],AOC2021.Models.Bingo.BingoBoard[]}")]
[assembly: SuppressMessage("Style", "IDE0042:Deconstruct variable declaration", Justification = "Clearer in this case, with named tuple, imo.", Scope = "member", Target = "~M:AOC2021.Days.Day02.Result1~System.String")]
[assembly: SuppressMessage("Style", "IDE0042:Deconstruct variable declaration", Justification = "Clearer in this case, with named tuple, imo.", Scope = "member", Target = "~M:AOC2021.Days.Day02.Result2~System.String")]
[assembly: SuppressMessage("Style", "IDE0042:Deconstruct variable declaration", Justification = "I like it better with tuple name, naming what it is.", Scope = "member", Target = "~M:AOC2021.Days.Day02.Result2~System.String")]
[assembly: SuppressMessage("Style", "IDE0042:Deconstruct variable declaration", Justification = "I like it better with tuple name, naming what it is.", Scope = "member", Target = "~M:AOC2021.Days.Day02.Result1~System.String")]
[assembly: SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1515:Single-line comment should be preceded by blank line", Justification = "I want the comment to take up the least space.", Scope = "member", Target = "~M:AOC2021.Models.Display.Display.Solve(System.String[])~System.Int32")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "I don't want to call it statically.", Scope = "member", Target = "~M:AOC2021.Days.Day07.SumSequence(System.Int64)~System.Int64")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "I don't want to call it statically.", Scope = "member", Target = "~M:AOC2021.Days.Day09.IsMinimum(System.Int32[,],System.Int32,System.Int32,System.Int32,System.Int32)~System.Boolean")]
[assembly: SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "I may still want this property.", Scope = "member", Target = "~P:AOC2021.Models.Basin.LowPoint")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:Elements should appear in the correct order", Justification = "Best to have the options right before the method that processes them.", Scope = "member", Target = "~M:AOC2021.Program.ProcessArgs(System.String[])~System.Collections.Generic.IEnumerable{AOC2021.Days.IDay}")]
