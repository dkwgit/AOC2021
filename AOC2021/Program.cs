using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;


namespace AOC2021 // Note: actual namespace depends on the project name.
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Trace.Listeners.RemoveAt(0);
            // Create and add a new default trace listener.
            DefaultTraceListener defaultListener;
            defaultListener = new DefaultTraceListener();
            Trace.Listeners.Add(defaultListener);

            int result_01_01 = Day_01_01();
            int result_01_01_actual = 1167;
            Assert.IsTrue(result_01_01 == result_01_01_actual, "Day_01_01 has a wrong result");
            int result_01_02 = Day_01_02();
            int result_01_02_actual = 1130;
            Assert.IsTrue(result_01_02 == result_01_02_actual, "Day_01_02 has a wrong result");
            int result_01_02_Variant = Day_01_02_Variant();
            Assert.IsTrue(result_01_02_Variant == result_01_02_actual, "Day_01_02_Variant has a wrong result");
            int result_02_01 = Day_02_01();
            int result_02_01_actual = 1947824;
            Assert.IsTrue(result_02_01 == result_02_01_actual, "Day_02_01 has a wrong result");
            int result_02_02 = Day_02_02();
            int result_02_02_actual = 1813062561;
            Assert.IsTrue(result_02_02 == result_02_02_actual, "Day_02_02 has a wrong result");
            int result_03_01 = Day_03_01();
            int result_03_01_actual = 2967914;
            Assert.IsTrue(result_03_01 == result_03_01_actual, "Day_03_01 has a wrong result");
            int result_03_02 = Day_03_02();
            int result_03_02_actual = 7041258;
            Assert.IsTrue(result_03_02 == result_03_02_actual, "Day_03_02 has a wrong result");
            //int result_04_01 = Day_04_01();
            //int result_04_01_actual = 7041258;
            //Assert.IsTrue(result_04_01 == result_04_01_actual, "Day_04_01 has a wrong result");
        }

        public static int[] Day_01_01_Input { get; set; } =  Process_Day_01_01_Input();
        public static int[] Process_Day_01_01_Input()
        {
            return File.ReadAllLines("../../../Day_01_01.txt").Select(x =>
            {
                int.TryParse(x, out int result);
                return result;
            }).ToArray();
        }

        public static List<(int, int)> Day_02_01_Input { get; set; } = Process_Day_02_01_Input();
        public static List<(int, int)> Process_Day_02_01_Input()
        {
            return File.ReadAllLines("../../../Day_02_01.txt").Select(s =>
            {
                int forward = 0;
                int vertical = 0;

                if (s.Contains("forward"))
                {
                    s = s.Replace("forward ", "");
                    int.TryParse(s, out forward);
                }
                if (s.Contains("up"))
                {
                    s = s.Replace("up ", "");
                    int.TryParse(s, out vertical);
                    vertical *= -1;
                }
                if (s.Contains("down"))
                {
                    s = s.Replace("down ", "");
                    int.TryParse(s, out vertical);
                }
                return (forward, vertical);
            }).ToList();
        }

        public static List<(int, int)> Day_02_02_Input { get; set; } = Process_Day_02_02_Input();

        public static List<(int, int)> Process_Day_02_02_Input()
        {
            int aim = 0;

            return File.ReadAllLines("../../../Day_02_01.txt").Select(s =>
            {
                int forward = 0;
                int vertical = 0;
                int aimChange = 0;

                if (s.Contains("forward"))
                {
                    s = s.Replace("forward ", "");
                    int.TryParse(s, out forward);
                    vertical = aim * forward;
                }

                if (s.Contains("up"))
                {
                    s = s.Replace("up ", "");
                    int.TryParse(s, out aimChange);
                    aim -= aimChange;

                }
                if (s.Contains("down"))
                {
                    s = s.Replace("down ", "");
                    int.TryParse(s, out aimChange);
                    aim += aimChange;
                }
                return (forward, vertical);
            }).ToList();
        }

        public static (List<BitVector32>, int) Day_03_01_Input { get; set; } = Process_Day_03_01_Input();

        public static (List<BitVector32>, int) Process_Day_03_01_Input()
        {
            int columnCount = -1;
            var list = File.ReadAllLines("../../../Day_03_01.txt").Select(s =>
            {
                if (columnCount == -1)
                {
                    columnCount = s.Length;
                }
                else
                {
                    Assert.IsTrue(s.Length == columnCount);
                }
                BitVector32 bits = new(0);
                int mask = BitVector32.CreateMask();
                for (int i = 0; i < s.Length; i++)
                {
                    /*if (s[^(i + 1)] == '1')
                    {
                        bits[mask] = s[^(i + 1)] == '1';
                    }*/
                    if(s[i] == '1')
                    {
                        bits[mask] = s[i] == '1';
                    }
                    mask = BitVector32.CreateMask(mask);
                }
                return bits;
            }).ToList();

            return (list, columnCount);
        }

        public static (List<int>, List<int[,]>) Day_04_01_Input { get; set; } = Process_Day_04_01_Input();

        public static (List<int>, List<int[,]>) Process_Day_04_01_Input()
        {
            List<string> lines = File.ReadAllLines("../../../Day_04_01.txt").ToList();
            List<int[,]> boards = new();

            List<int> numbers = lines[0].Split(",").Select(x =>
            {
                int.TryParse(x, out int number);
                return number;
            }).ToList();

            int lineNumber = 1;
            int[,] board = new int[5,5];
            string pattern = @"((?: \d)|\d{2}) ?";
            while(lineNumber < lines.Count)
            {
                if (string.IsNullOrEmpty(lines[lineNumber++]))
                {
                    ;
                }

                for (int row = 0; row < 5; row++)
                {
                    string line = lines[lineNumber++];
                    var matches = Regex.Matches(line, pattern);

                    for (int column = 0; column < 5; column++)
                    {
                        int.TryParse(matches[column].Value, out int number);
                        board[row, column] = number;
                    }
                }
                boards.Add(board);

                board = new int[5,5];
            }
            return (numbers, boards);
        }

        public static int Day_01_01()
        {
            int[] depths = Day_01_01_Input;
            
            int result = depths.Where((item, index) =>
            {
                if (index > 0 && (depths[index - 1] < depths[index]))
                {
                    return true;
                }
                return false;
            }).Count();


            Console.WriteLine($"Day_01_01 result: {result}");
            return result;
        }

        public static int Day_01_02()
        {
            int[] depths = Day_01_01_Input;

            List<int> sums = new();

            for (int i = 2; i < depths.Length; i++)
            {
                sums.Add(depths[i - 2] + depths[i - 1] + depths[i]);
            }

            int result = sums.Where((item, index) =>
            {
                if (index > 0 && (sums[index - 1] < sums[index]))
                {
                    return true;
                }
                return false;
            }).Count();

            Console.WriteLine($"Day_01_02 result: {result}");
            return result;
        }

        public static int Day_01_02_Variant()
        {
            int[] depths = Day_01_01_Input;

            int? priorValue = null;

            int result = depths.WindowTraverse(3, (int accumulate, int source) => accumulate + source).Where(item => { bool ret = priorValue.HasValue && priorValue.Value < item; priorValue = item; return ret; }).Count();
 
            Console.WriteLine($"Day_01_02 result: {result}");
            return result;
        }

        public static int Day_02_01()
        {
            List<(int, int)> coords = Day_02_01_Input;

            (int, int) result = coords.Aggregate<(int, int)>(
                ((int, int) element, (int, int) accumulate) =>
                {
                    (int forward, int vertical) = element;
                    (int f, int v) = accumulate;
                    return (forward + f, vertical + v);
                }
            );

            int product = result.Item1 * result.Item2;
            Console.WriteLine($"Day_02_01 result: {product}");
            return product;
        }

        public static int Day_02_02()
        {
            List<(int, int)> coords = Day_02_02_Input;

            (int, int) result = coords.Aggregate<(int, int)>(
                ((int, int) element, (int, int) accumulate) =>
                {
                    (int forward, int vertical) = element;
                    (int f, int v) = accumulate;
                    return (forward + f, vertical + v);
                }
            );

            int product = result.Item1 * result.Item2;
            Console.WriteLine($"Day_02_02 result: {product}");
            return product;
        }

        public static int Day_03_01()
        {
            (List<BitVector32> diags, int columnCount) = Day_03_01_Input;

            int rowCount = diags.Count;

            BitArray epsilon = new(columnCount);
            int[] columnSums = new int[columnCount];

            for (int r = 0; r < diags.Count; r++)
            {
                var row = diags[r];
                int mask = BitVector32.CreateMask();
                for (int c = 0; c < columnCount; c++)
                {
                    columnSums[c] += row[mask] ? 1 : 0;
                    mask = BitVector32.CreateMask(mask);
                }
            }

            for (int c = 0; c < columnCount; c++)
            {
                epsilon[c] = columnSums[c] > (rowCount / 2);
            }

            int epsilonAsInt = 0;
            int gammaAsInt = 0;

            for (int c = 0; c < columnCount; c++)
            {
                epsilonAsInt |= epsilon[c] ? 1 : 0;
                gammaAsInt |= epsilon[c] ? 0 : 1;
                if (c + 1 < columnCount)
                {
                    epsilonAsInt <<= 1;
                    gammaAsInt <<= 1;
                }
            }

            int product = epsilonAsInt * gammaAsInt;
            Console.WriteLine($"Day_03_01 result: {product}");
            return product;
        }

        public static int Day_03_02()
        {
            (List<BitVector32> diags, int columnCount) = Day_03_01_Input;
           
            List<BitVector32> oxygenItems = new(diags);
            List<BitVector32> scrubberItems = new(diags);

            int mask = BitVector32.CreateMask();
            for (int c = 0; c < columnCount; c++)
            {
                var groupings = oxygenItems.
                    GroupBy(x => x[mask]).
                    Select(x => new { Value = x.Key, Count = x.Count() }).
                    OrderByDescending(x => x.Count).
                    ThenByDescending(x => x.Value).ToArray();

                bool searchValue = groupings[0].Value;

                oxygenItems = oxygenItems.Where(x => x[mask] == searchValue).ToList();
                if (oxygenItems.Count == 1)
                {
                    break;
                }

                mask = BitVector32.CreateMask(mask);
            }

            mask = BitVector32.CreateMask(); 
            for (int c = 0; c < columnCount; c++)
            {
                var groupings = scrubberItems.
                     GroupBy(x => x[mask]).
                     Select(x => new { Value = x.Key, Count = x.Count() }).
                     OrderBy(x => x.Count).
                     ThenBy(x => x.Value).ToArray();

                bool searchValue = groupings[0].Value;

                scrubberItems = scrubberItems.Where(x => x[mask] == searchValue).ToList();
                if (scrubberItems.Count == 1)
                {
                    break;
                }
                mask = BitVector32.CreateMask(mask);
            }
            int oxygenNumber = 0;
            int scrubberNumber = 0;
            mask = BitVector32.CreateMask();
            for (int i = 0; i < columnCount; i++)
            {
                oxygenNumber |= oxygenItems[0][mask] ? 1 : 0;
                scrubberNumber |= scrubberItems[0][mask] ? 1 : 0;
                if (i + 1 < columnCount)
                {
                    oxygenNumber <<= 1;
                    scrubberNumber <<= 1;
                }
                mask = BitVector32.CreateMask(mask);
            }
            int product = oxygenNumber * scrubberNumber;
            Console.WriteLine($"Day_03_02 result: {product}");
            return product;
        }
    }
}