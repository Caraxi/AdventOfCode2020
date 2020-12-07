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

        static T[] GetInputs<T>(byte day, Func<string, T> selectFunc, bool blankLines = false) {
            return GetInputs(day, blankLines).Select(selectFunc).ToArray();
        }

        static string[] GetInputs(byte day, bool blankLines = false) {
            return File.ReadAllLines($"../../../input/day-{day:D2}.txt").Where(a => blankLines || a.Length > 0).ToArray();
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
            var inputs = GetInputs(2);
            var validPart1 = 0;
            var validPart2 = 0;
            for (var i = 0; i < inputs.Length; i++) {
                var l = inputs[i].Split(':', '-', ' ');
                var min = int.Parse(l[0]);
                var max = int.Parse(l[1]);
                var chr = char.Parse(l[2]);
                var pass = l[4];

                var count = 0;
                foreach (var c in pass) {
                    if (c == chr) {
                        count++;
                    }
                }

                if (count >= min && count <= max) {
                    validPart1++;
                }

                if (pass[min - 1] == chr ^ pass[max - 1] == chr) {
                    validPart2++;
                }
            }

            Console.WriteLine($"Day 2-1 Answer: {validPart1}");
            Console.WriteLine($"Day 2-2 Answer: {validPart2}");
        }

        static void Day03() {
            var inputs = GetInputs(3);
            var multiple = 1UL;
            var yy = 1;
            for (var xx = 1; xx < 9; xx += 2) {
                var x = 0;
                var treesHit = 0UL;
                for (var y = 0; y < inputs.Length; y += yy) {
                    if (inputs[y][x] == '#') treesHit++;
                    x = (x + xx) % inputs[y].Length;
                }

                if (xx == 3 && yy == 1) {
                    Console.WriteLine($"Day 3-1 Answer: {treesHit}");
                }

                multiple *= treesHit;
                if (yy == 2) break;
                if (xx != 7) continue;
                xx = -1;
                yy++;
            }

            Console.WriteLine($"Day 3-2 Answer: {multiple}");
        }

        static void Day04() {
            var inputs = GetInputs(4, true);
            var valid = 0;
            var valid2 = 0;

            var fields = new System.Collections.Generic.Dictionary<string, bool?> {
                { "byr", null}, { "iyr", null},
                { "eyr", null}, { "hgt", null},
                { "hcl", null}, { "ecl", null},
                { "pid", null}, { "cid", null},
            };
            
            for (var i = 0; i <= inputs.Length; i++) {
                if (i >= inputs.Length || inputs[i].Length == 0) {
                    if (fields.All(kvp => kvp.Key == "cid" || kvp.Value.HasValue)) valid++;
                    if (fields.All(kvp => kvp.Key == "cid" || kvp.Value != null && kvp.Value.Value)) valid2++;
                    foreach (var k in fields.Keys.ToArray()) {
                        fields[k] = null;
                    }
                    continue;
                }

                foreach (var kvp in inputs[i].Split(' ').Select(a => a.Split(':'))) {
                    if (fields.ContainsKey(kvp[0])) {
                        fields[kvp[0]] = false;

                        switch (kvp[0]) {
                            case "byr": {
                                if (int.TryParse(kvp[1], out var val)) {
                                    fields[kvp[0]] = val >= 1920 && val <= 2002;
                                }
                                break;
                            }
                            case "iyr": {
                                if (int.TryParse(kvp[1], out var val)) {
                                    fields[kvp[0]] = val >= 2010 && val <= 2020;
                                }
                                break;
                            }
                            case "eyr": {
                                if (int.TryParse(kvp[1], out var val)) {
                                    fields[kvp[0]] = val >= 2020 && val <= 2030;
                                }
                                break;
                            }
                            case "hgt": {
                                if (int.TryParse(kvp[1].Substring(0, kvp[1].Length - 2), out var val)) {
                                    fields[kvp[0]] = (kvp[1].EndsWith("cm") && val >= 150 && val <= 193) || (kvp[1].EndsWith("in") && val >= 59 && val <= 76);
                                }
                                break;
                            }
                            case "hcl": {
                                if (kvp[1].Length == 7 && kvp[1][0] == '#') {
                                   fields[kvp[0]] = int.TryParse(kvp[1].Substring(1), System.Globalization.NumberStyles.HexNumber, null, out _);
                                }
                                break;
                            }
                            case "ecl": {
                                fields[kvp[0]] = (new string[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"}).Contains(kvp[1]);
                                break;
                            }
                            case "pid": {
                                fields[kvp[0]] = kvp[1].Length == 9 && int.TryParse(kvp[1], out _);
                                break;
                            }
                            case "cid": {
                                fields[kvp[0]] = true;
                                break;
                            }
                        }

                    }
                }
            }

            Console.WriteLine($"Day 4-1 Answer: {valid}");
            Console.WriteLine($"Day 4-2 Answer: {valid2}");
        }

        static void Day05() {
            var inputs = GetInputs(5);

            var found = new bool[128 * 8];
            var maxId = 0;
            var minId = found.Length;
            foreach (var input in inputs) {
                var id = 0;
                var s = found.Length / 2;
                for (var i = 0; i < 10; i++, s/=2) {
                    if (input[i] == 'B' || input[i] == 'R') id += s;
                }

                if (id > maxId) maxId = id;
                if (id < minId) minId = id;
                found[id] = true;
            }

            Console.WriteLine($"Day 5-1 Answer: {maxId}");

            for (var i = minId; i < maxId; i++) {
                if (found[i]) continue;
                Console.WriteLine($"Day 5-2 Answer: {i}");
                break;
            }
        }

        static void Day06() {
            var inputs = GetInputs(6, true);
            var any = 0;
            var all = 0;

            var groupAnswers = new int[127];
            var groupCount = 0;
            foreach (var person in inputs) {
                if (person.Length == 0) {
                    foreach (var a in groupAnswers) {
                        if (a > 0) any++;
                        if (a == groupCount) all++;
                    }
                    groupAnswers = new int[127];
                    groupCount = 0;
                    continue;
                }

                groupCount++;
                foreach (var c in person) {
                    groupAnswers[c]++;
                }
            }

            if (groupCount > 0) {
                foreach (var a in groupAnswers) {
                    if (a > 0) any++;
                    if (a == groupCount) all++;
                }
            }

            Console.WriteLine($"Day 6-1 Answer: {any}");
            Console.WriteLine($"Day 6-2 Answer: {all}");
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
