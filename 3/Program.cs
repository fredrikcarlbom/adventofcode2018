using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;

namespace _3
{
    class Program
    {
        static void Main(string[] args) {
            var claims = new List<Claim>();
            var maximumSize = new Size(0, 0);
            do {
                var token = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(token)) {
                    var claim = Claim.Parse(token);
                    maximumSize.Width = Math.Max(maximumSize.Width, claim.Position.X + claim.Size.Width);
                    maximumSize.Height = Math.Max(maximumSize.Height, claim.Position.Y + claim.Size.Height);
                    claims.Add(claim);
                } else {
                    break;
                }
            } while (true);
            Console.WriteLine("X " + maximumSize.Width + " Y " + maximumSize.Height);
            var fabric = new List<int> [maximumSize.Width+1, maximumSize.Height+1];
            var numberOfSquaresWithMultipleClaims = 0;
            foreach (var claim in claims) {
                for (var x = 0; x < claim.Size.Width; x++) {
                    for (var y = 0; y < claim.Size.Height; y++) {
                        if (fabric[claim.Position.X + x, claim.Position.Y + y] == null) {
                            fabric[claim.Position.X + x, claim.Position.Y + y] = new List<int>();
                        }
                        fabric[claim.Position.X + x, claim.Position.Y + y].Add(claim.Identifier);
                        if (fabric[claim.Position.X + x, claim.Position.Y + y].Count == 2) {
                           numberOfSquaresWithMultipleClaims++; 
                        }
                    }
                }
            }
            Console.WriteLine("Number of Squares:" + numberOfSquaresWithMultipleClaims);
            var notOverlappingClaims = claims.ConvertAll(claim => claim.Identifier);
            for (var x = 0; x < maximumSize.Width+1; x++) {
                for (var y = 0; y < maximumSize.Height+1; y++) {
                    if (fabric[x, y] != null && fabric[x, y].Count > 1) {
                        foreach(var claim in fabric[x, y]) {
                            notOverlappingClaims.Remove(claim);
                        }
                    }
                }
            }
            Console.WriteLine("Does not overlap:" + notOverlappingClaims[0]);
        }
    }

    class Claim {
        public int Identifier { get; set; }
        public Point Position { get; set; }
        public Size Size { get; set; }

        public static Claim Parse(string token) {
            // #123 @ 3,2: 5x4
            var match = Regex.Match(token, @"^#(?<identifier>\d+)\s+@\s+(?<left>\d+),(?<top>\d+):\s+(?<width>\d+)x(?<height>\d+)$");
            return new Claim {
                Identifier = Int32.Parse(match.Groups["identifier"].Value),
                Position = new Point(Int32.Parse(match.Groups["left"].Value), Int32.Parse(match.Groups["top"].Value)),
                Size = new Size(Int32.Parse(match.Groups["width"].Value), Int32.Parse(match.Groups["height"].Value))
            };
        }
    }
}
