using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventCalender2024.Days
{
    internal class Day3
    {
        public async Task Run()
        {
            //HttpRequestSetup
            string sessionToken = "53616c7465645f5fc8bb81cd507326a704fc03bf2f220a371e76902074c79d7d29c4cf8b1be94115098b6a9f269f3653a8e0839733f85cc423714213f52af5a6";
            string url = $"https://adventofcode.com/2024/day/3/input";

            try
            {
                //HttpRequest
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Cookie", $"session={sessionToken}");
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string input = await response.Content.ReadAsStringAsync();

                    //call part 1 or part 2

                    //Part1(input);
                    Part2(input);
                }
            }
            catch (Exception ex) { }
        }

        public void Part1(string input)
        {
            string pattern = @"\bmul\((\d{1,3}),(\d{1,3})\)";

            MatchCollection matches = Regex.Matches(input, pattern);
            int result = 0;

            foreach (Match match in matches)
            {
                int tempNumber = 0;

                // Parse the two numbers from the match groups
                int num1 = int.Parse(match.Groups[1].Value);
                int num2 = int.Parse(match.Groups[2].Value);

                tempNumber = num1 * num2;
                if (tempNumber > 0)
                {
                    result = result + tempNumber;
                }

            }

            Console.WriteLine(result);
            Console.ReadKey();
        }

        public void Part2(string input)
        {
            string[] segments = Regex.Split(input, @"(?=do\(\)|don't\(\))");
            string pattern = @"\bmul\((\d{1,3}),(\d{1,3})\)";

            int result = 0;

            foreach (string segment in segments)
            {
                if (segment.StartsWith("don't()") is false)
                {
                    MatchCollection matches = Regex.Matches(segment, pattern);

                    foreach (Match match in matches)
                    {
                        int tempNumber = 0;

                        // Parse the two numbers from the match groups
                        int num1 = int.Parse(match.Groups[1].Value);
                        int num2 = int.Parse(match.Groups[2].Value);

                        tempNumber = num1 * num2;
                        if (tempNumber > 0)
                        {
                            result = result + tempNumber;
                        }

                    }
                }
            }
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
