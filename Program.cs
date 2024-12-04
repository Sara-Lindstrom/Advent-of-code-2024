using AdventCalender2024.Days;

class Program
{
    static async Task Main()
    {
        string sessionToken = "";
        var test = new Day4();
        await test.Run(sessionToken);
    }
}