using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventCalender2024.Days
{
    internal class Day4
    {
        public async Task Run(string sessionToken)
        {
            //HttpRequestSetup
            string url = $"https://adventofcode.com/2024/day/4/input";

            try
            {
                //HttpRequest
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Cookie", $"session={sessionToken}");
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string input = await response.Content.ReadAsStringAsync();

                    var inputGrid = input
                        .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                        .Select(line => new string(line
                            .ToArray()))
                        .ToList();

                    //call part 1 or part 2

                    //Part1(inputGrid);
                    //Part2(inputGrid);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void Part1(List<string> inputGrid)
        {
            int rows = inputGrid.Count;
            int cols = inputGrid[0].Length;
            string target = "XMAS";
            int targetLength = target.Length;

            // Define all 8 directions (dy, dx)
            int[][] directions = {
                        new[] { 0, 1 },  // Right
                        new[] { 1, 0 },  // Down
                        new[] { 1, 1 },  // Diagonal Down-Right
                        new[] { 1, -1 }, // Diagonal Down-Left
                        new[] { 0, -1 }, // Left
                        new[] { -1, 0 }, // Up
                        new[] { -1, -1 },// Diagonal Up-Left
                        new[] { -1, 1 }  // Diagonal Up-Right
                    };

            int XmasOccuranceCount = 0;

            bool IsValid(int y, int x) => y >= 0 && y < rows && x >= 0 && x < cols;

            bool SearchFrom(int y, int x, int dy, int dx)
            {
                var coords = Enumerable.Range(0, targetLength)
                                       .Select(i => (y + i * dy, x + i * dx))
                                       .ToList();

                if (coords.Any(c => !IsValid(c.Item1, c.Item2) || inputGrid[c.Item1][c.Item2] != target[coords.IndexOf(c)]))
                {
                    return false;
                }

                XmasOccuranceCount++;
                return true;
            }

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    foreach (var direction in directions)
                    {
                        int dy = direction[0], dx = direction[1];
                        SearchFrom(row, col, dy, dx);
                    }
                }
            }

            Console.WriteLine(XmasOccuranceCount.ToString());

        }
        public void Part2(List<string> inputGrid)
        {
            try
            {
                int rows = inputGrid.Count;
                int cols = inputGrid[0].Length;

                // Define the 4 diagonal directions (dy, dx)
                int[][] directions = {
                new[] { -1, -1 }, // Diagonal Up-Left
                new[] { -1, 1 },  // Diagonal Up-Right
                new[] { 1, -1 },  // Diagonal Down-Left
                new[] { 1, 1 }    // Diagonal Down-Right
            };

                int masOccurrenceCount = 0;

                bool IsValid(int y, int x) => y >= 0 && y < rows && x >= 0 && x < cols;

                bool SearchFrom(int y, int x)
                {
                    char center = inputGrid[y][x];

                    if (center is not 'A') return false;

                    char[] diagonalArms = {
                    ' ',  // upLeftChar
                    ' ',  // upRightChar
                    ' ',  // downLeftChar
                    ' '   // downRightChar
                    };

                    for (int i = 0; i < directions.Length && i < diagonalArms.Length; i++)
                    {
                        if (IsValid(y + directions[i][0], x + directions[i][1]))
                        {
                            diagonalArms[i] = inputGrid[y + directions[i][0]][x + directions[i][1]];
                        }
                    }

                    if (diagonalArms.All(c => c == 'S' || c == 'M') &&
                        diagonalArms[0] != diagonalArms[3] &&
                        diagonalArms[1] != diagonalArms[2])
                    {
                        masOccurrenceCount++;
                        return true;
                    }

                    return false;
                }

                // Loop through the grid to find occurrences
                for (int row = 1; row < rows - 1; row++)  // Start at row 1 to avoid edge cases
                {
                    for (int col = 1; col < cols - 1; col++) // Start at col 1 to avoid edge cases
                    {
                        SearchFrom(row, col);
                    }
                }

                // Output the result
                Console.WriteLine(masOccurrenceCount.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }            
        }

    }
}
