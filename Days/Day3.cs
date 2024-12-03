using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

                    //Part1(allNumbers);
                    //Part2(allNumbers);
                }
            }
            catch (Exception ex) { }
        }

        public void Part1()
        {

        }
        public void Part2()
        {

        }
    }
}
