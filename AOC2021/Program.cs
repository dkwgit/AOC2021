using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2021 // Note: actual namespace depends on the project name.
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int result_01_01 = Day_01_01();
            int result_01_02 = Day_01_02();
            int result_01_02_Variant = Day_01_02_Variant();
            int result_02_01 = Day_02_01();
            int result_02_02 = Day_02_02();
        }

        public static int[] Day_01_01_Input = File.ReadAllLines("../../../Day_01_01.txt").Select(x =>
        {
            int.TryParse(x, out int result);
            return result;
        }).ToArray();

        public static List<(int,int)> Day_02_01_Input = File.ReadAllLines("../../../Day_02_01.txt").Select(s => 
        {
            int forward = 0;
            int vertical = 0;

            if (s.Contains("forward"))
            {
                s = s.Replace("forward ","");
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

        private static int aim = 0;

        public static List<(int, int)> Day_02_02_Input = File.ReadAllLines("../../../Day_02_01.txt").Select(s =>
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
    }
}