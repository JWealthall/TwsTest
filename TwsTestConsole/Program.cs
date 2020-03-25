using System;
using System.Text;
using TwsTest;

namespace TwsTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var parameters = "";
            if (args != null && args.Length >= 1)
            {
                try
                {
                    // Assume the first arg is a file name
                    parameters = System.IO.File.ReadAllText(args[0]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                var sb = new StringBuilder(100);
                sb.AppendLine("5 5");
                sb.AppendLine("1 2 N");
                sb.AppendLine("LMLMLMLMM");
                sb.AppendLine("3 3 E");
                sb.Append("MMRMMRMRRM");
                parameters = sb.ToString();
            }
            var mission = new Mission(parameters);
            if (mission.RunMission())
            {
                Console.WriteLine(mission.Result);
            }
            else
            {
                Console.WriteLine(mission.Message);
            }
        }
    }
}
