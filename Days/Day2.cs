namespace AdventCalender2024.Days;

public class Day2
{
    //Day 2
    //Part 1
    //7 6 4 2 1
    //1 2 7 8 9
    //9 7 6 2 1
    //1 3 2 4 5
    //8 6 4 4 1
    //1 3 6 7 9

    //7 6 4 2 1: Safe because the levels are all decreasing by 1 or 2.
    //1 2 7 8 9: Unsafe because 2 7 is an increase of 5.
    //9 7 6 2 1: Unsafe because 6 2 is a decrease of 4.
    //1 3 2 4 5: Unsafe because 1 3 is increasing but 3 2 is decreasing.
    //8 6 4 4 1: Unsafe because 4 4 is neither an increase or a decrease.
    //1 3 6 7 9: Safe because the levels are all increasing by 1, 2, or 3.
    //So, in this example, 2 reports are safe.

    //Analyze the unusual data from the engineers.How many reports are safe?

    //part 2
    //tolerate a single bad level by removing it and the rest of the array is correct
    //7 6 4 2 1: Safe without removing any level.
    //1 2 7 8 9: Unsafe regardless of which level is removed.
    //9 7 6 2 1: Unsafe regardless of which level is removed.
    //1 3 2 4 5: Safe by removing the second level, 3.
    //8 6 4 4 1: Safe by removing the third level, 4.
    //1 3 6 7 9: Safe without removing any level.

    public async Task Run()
    {
        //HttpRequestSetup
        string sessionToken = "53616c7465645f5fc8bb81cd507326a704fc03bf2f220a371e76902074c79d7d29c4cf8b1be94115098b6a9f269f3653a8e0839733f85cc423714213f52af5a6";
        string url = $"https://adventofcode.com/2024/day/2/input";

        try
        {
            //HttpRequest
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Cookie", $"session={sessionToken}");
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string input = await response.Content.ReadAsStringAsync();

                var allNumbers = input
                    .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                    .Select(group => group.Split(' ').Select(int.Parse).ToArray())
                    .ToList();

                

                //call part 1 or part 2

                Part1(allNumbers);
                //Part2(allNumbers);

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

    public void Part1(List<int[]> allNumbers)
    {
        int safeReports = 0;

        for (int i = 0; i < allNumbers.Count; i++)
        {
            if (allNumbers[i][0] > allNumbers[i][1])
            {
                bool isDescending = allNumbers[i]
                                    .Zip(allNumbers[i].Skip(1), (current, next) => current > next)
                                    .All(isDescening => isDescening);

                if (isDescending)
                {
                    bool isSafe = allNumbers[i]
                                    .Zip(allNumbers[i].Skip(1), (current, next) => Math.Abs(current - next) <= 3)
                                    .All(isValid => isValid);

                    if (isSafe)
                        safeReports++;
                }

            }
            else if (allNumbers[i][0] < allNumbers[i][1])
            {

                bool isRising = allNumbers[i]
                                    .Zip(allNumbers[i].Skip(1), (current, next) => current < next)
                                    .All(isAscending => isAscending);

                if (isRising)
                {
                    bool isSafe = allNumbers[i]
                                    .Zip(allNumbers[i].Skip(1), (current, next) => Math.Abs(current - next) <= 3)
                                    .All(isValid => isValid);
                    if (isSafe)
                        safeReports++;
                }
            }
        }
        Console.WriteLine(safeReports.ToString());
    }

    public void Part2(List<int[]> allNumbers)
    {
        int safeReports = 0;

        for (int i = 0; i < allNumbers.Count; i++)
        {
            bool isSafe = false;
            if (allNumbers[i][0] > allNumbers[i][1])
            {
                bool isDescending = allNumbers[i]
                                    .Zip(allNumbers[i].Skip(1), (current, next) => current > next)
                                    .All(isDescening => isDescening);

                if (isDescending)
                {
                    isSafe = allNumbers[i]
                                    .Zip(allNumbers[i].Skip(1), (current, next) => Math.Abs(current - next) <= 3)
                                    .All(isValid => isValid);
                }

            }
            else if (allNumbers[i][0] < allNumbers[i][1])
            {

                bool isRising = allNumbers[i]
                                    .Zip(allNumbers[i].Skip(1), (current, next) => current < next)
                                    .All(isAscending => isAscending);

                if (isRising)
                {
                    isSafe = allNumbers[i]
                                    .Zip(allNumbers[i].Skip(1), (current, next) => Math.Abs(current - next) <= 3)
                                    .All(isValid => isValid);
                }
            }

            if (isSafe)
            {
                safeReports++;
            }
            else if (isSafe is false)
            {
                for (int j = 0; j < allNumbers[i].Length; j++)
                {
                    var modified = allNumbers[i].Where((_, idx) => idx != j).ToArray();

                    bool isDescending = modified[0] > modified[1] &&
                                        modified.Zip(modified.Skip(1), (current, next) => current > next)
                                                .All(isDescending => isDescending);

                    bool isAscending = modified[0] < modified[1] &&
                                       modified.Zip(modified.Skip(1), (current, next) => current < next)
                                               .All(isAscending => isAscending);

                    bool validDiffs = modified
                                        .Zip(modified.Skip(1), (current, next) => Math.Abs(current - next) <= 3)
                                        .All(isValid => isValid);

                    if ((isDescending || isAscending) && validDiffs)
                    {
                        safeReports++;
                        break;
                    }
                }
            }
            Console.WriteLine(safeReports.ToString());
        }

    }
}
