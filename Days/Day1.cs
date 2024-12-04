using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCalender2024.Days;

internal class Day1
{
    // Day 1
    //get a list with two columns (left and right) with different distances. ("3 2\n1 4")
    //sort them into two lists. (3,1), (2,4)

    //part 1
    //sort the two lists in rising order. (3,1), (2,4) -> (1,3), (2,4)
    //then get the total difference in distance between the two lists (2-1 =1, 4-3 = 1. 1+1=2. correct anwver =2)

    //part 2
    //find reoccuring numbers in list
    //multiply with original number (3,4,2,1,3,3)(4,3,5,3,9,3) 3 found 3 times (3*3=9) aso. and (9+4+0+0+9+9 = 31)

    public async Task Run(string sessionToken)
    {
        //HttpRequestSetup
        string url = $"https://adventofcode.com/2024/day/1/input";

        try
        {
            //HttpRequest
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Cookie", $"session={sessionToken}");
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string input = await response.Content.ReadAsStringAsync();

                //Convert input to two lists
                var allNumbers = input
                    .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                    .SelectMany(row=> row.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                    .Select(int.Parse) 
                    .ToList();

                List<int> leftColumn = new List<int>();
                List<int> rightColumn = new List<int>();


                for (int i = 0; i < allNumbers.Count; i++)
                {
                    if (i % 2 == 0)
                        leftColumn.Add(allNumbers[i]);
                    else
                        rightColumn.Add(allNumbers[i]);
                }

                // Call part1 or part2

                //Part1(leftColumn, rightColumn);
                //Part2(leftColumn, rightColumn);
            }
            else
            {
                Console.WriteLine($"Failed to fetch input: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        Console.ReadKey();
    }

    //Part 1
    //Sort lists
    //leftColumn = leftColumn.OrderBy(x => x).ToList();
    //rightColumn = rightColumn.OrderBy(x => x).ToList();
    public void Part1(List<int> leftColumn, List<int> rightColumn)
    {
        //Count distance
        int totalDistance = 0;
        int newDistance = 0;
        for (int i = 0; i < leftColumn.Count && i < rightColumn.Count; i++)
        {

            if (leftColumn[i] < rightColumn[i])
            {
                newDistance = rightColumn[i] - leftColumn[i];
            }

            else if (rightColumn[i] < leftColumn[i])
            {
                newDistance = leftColumn[i] - rightColumn[i];
            }

            else if (rightColumn[i] == leftColumn[i])
            {
                newDistance = 0;
            }

            totalDistance += newDistance;
        }
        Console.WriteLine("totalDistance: " + totalDistance);
    }

    //Part 2
    public  void Part2(List<int> leftColumn, List<int> rightColumn)
    {
        int similairitiesFoundAmount = 0;
        bool isRepeated = false;

        for (int i = 0; i < leftColumn.Count && i < rightColumn.Count; i++)
        {
            int numberIsReapetedTimes = rightColumn.Count(x => x == leftColumn[i]);
            int similairityScore = leftColumn[i] * numberIsReapetedTimes;
            similairitiesFoundAmount = similairitiesFoundAmount + similairityScore;
        }

        Console.WriteLine(similairitiesFoundAmount.ToString());

    }
}
