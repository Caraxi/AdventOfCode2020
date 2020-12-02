using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020 {
    class Program {
        static void Main(string[] args) {
            Day01(); Day02(); Day03(); Day04(); Day05();
            Day06(); Day07(); Day08(); Day09(); Day10();
            Day11(); Day12(); Day13(); Day14(); Day15();
            Day16(); Day17(); Day18(); Day19(); Day20();
            Day21(); Day22(); Day23(); Day24(); Day25();
            Console.ReadKey();
        }

        static T[] GetInputs<T>(byte day, Func<string, T> selectFunc) {
            return File.ReadAllLines($"./input/day-{day}.txt").Where(a => a.Length > 0).Select(selectFunc).ToArray();
        }

        static void Day01() {
            var inputs = GetInputs(1, int.Parse);
            var target = 2020;

            for (var i = 0; i < inputs.Length; i++) {
                for (var j = i + 1; j < inputs.Length; j++) {
                    if (inputs[i] + inputs[j] == target) {
                        Console.WriteLine($"Day 1-1 Answer: {inputs[i] * inputs[j]}");
                    }

                    for (var k = j + 1; k < inputs.Length; k++) {
                        if (inputs[i] + inputs[j] + inputs[k] == target) {
                            Console.WriteLine($"Day 1-2 Answer: {inputs[i] * inputs[j] * inputs[k]}");
                        }
                    }
                }
            }
        }
        static void Day02() {
        }

        static void Day03() {
        }

        static void Day04() {
        }

        static void Day05() {
        }

        static void Day06() {
        }

        static void Day07() {
        }

        static void Day08() {
        }

        static void Day09() {
        }

        static void Day10() {
        }

        static void Day11() {
        }

        static void Day12() {
        }

        static void Day13() {
        }

        static void Day14() {
        }

        static void Day15() {
        }

        static void Day16() {
        }

        static void Day17() {
        }

        static void Day18() {
        }

        static void Day19() {
        }

        static void Day20() {
        }

        static void Day21() {
        }

        static void Day22() {
        }

        static void Day23() {
        }

        static void Day24() {
        }

        static void Day25() {
        }
    }
}
