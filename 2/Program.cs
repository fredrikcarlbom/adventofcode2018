using System;
using System.Collections.Generic;

namespace _2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> input = new List<string>();
            var numberOfThree = 0;
            var numberOfTwo = 0;
            var matchedDifferByOne = false;
            var commonDenominator = "";
            do {
                var token = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(token)) {
                    if (!matchedDifferByOne) {
                        foreach(var existingToken in input) {
                            if (DifferByOneCharacter(existingToken, token)) {
                                matchedDifferByOne = true;
                                commonDenominator = CommonDenominator(existingToken, token);
                            }
                        }
                    }
                    input.Add(token);
                    if (ContainsExactly(token, 2)) {
                        numberOfTwo++;
                    }
                    if (ContainsExactly(token, 3)) {
                        numberOfThree++;
                    }
                } else {
                    break;
                }
            } while (true);

            Console.WriteLine(numberOfTwo + " x " + numberOfThree + " = " + (numberOfTwo * numberOfThree));
            Console.WriteLine(commonDenominator);
        }

        private static bool ContainsExactly(string input, int expectedNumber) {
            for (var outer = 0; outer < input.Length; outer++) {
                var firstMatchIndex = input.IndexOf(input[outer]);
                if (firstMatchIndex != -1 && firstMatchIndex < outer) {
                    continue;
                }
                var numberOfMatches = 1;
                for (var inner = outer+1; inner < input.Length; inner++) {
                    if (input[outer] == input[inner]) {
                        numberOfMatches++;
                    }
                }
                if (expectedNumber == numberOfMatches) {
                    return true;
                }
            }
            return false;
        }

        private static bool DifferByOneCharacter(string s1, string s2) {
            var numberOfDifferentiatingCharacters = 0;
            for (var index=0; index < s1.Length; index++) {
                numberOfDifferentiatingCharacters += s1[index] != s2[index] ? 1 : 0;
                if (numberOfDifferentiatingCharacters > 1) {
                    return false;
                }
            }
            return numberOfDifferentiatingCharacters == 1;
        }

        private static string CommonDenominator(string s1, string s2) {
            string commonString = "";
            for (var index=0; index < s1.Length; index++) {
                commonString += s1[index] == s2[index] ? s1[index].ToString() : "";
            }
            return commonString;
        }
    }
}
