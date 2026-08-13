using System;
using System.Threading.Tasks;
using DotNetEnv;

namespace Mezon_sdk.Test
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Env.Load();

            string choice = args.Length > 0 ? args[0].ToLower() : "";

            if (string.IsNullOrEmpty(choice))
            {
                Console.WriteLine("========================================");
                Console.WriteLine("        MEZON SDK TEST RUNNER           ");
                Console.WriteLine("========================================");
                Console.WriteLine("Chon test case muon chay:");
                Console.WriteLine("1. login (LoginTest)");
                Console.WriteLine("2. chan  (ChanTest)");
                Console.WriteLine("3. reply (ReplyTest)");
                Console.WriteLine("4. all   (Chay tat ca)");
                Console.Write("\nNhap lua chon (1-4 hoac ten test): ");
                choice = (Console.ReadLine() ?? "").Trim().ToLower();
            }

            switch (choice)
            {
                case "1":
                case "login":
                case "logintest":
                    Console.WriteLine("\n--- Running LoginTest ---");
                    await LoginTest.RunAsync();
                    break;

                case "2":
                case "chan":
                case "chantest":
                    Console.WriteLine("\n--- Running ChanTest ---");
                    await ChanTest.RunAsync();
                    break;

                case "3":
                case "reply":
                case "replytest":
                    Console.WriteLine("\n--- Running ReplyTest ---");
                    await ReplyTest.RunAsync();
                    break;

                case "4":
                case "all":
                    Console.WriteLine("\n--- Running All Tests ---");
                    Console.WriteLine("\n[1/3] Running LoginTest...");
                    await LoginTest.RunAsync();
                    Console.WriteLine("\n[2/3] Running ChanTest...");
                    await ChanTest.RunAsync();
                    Console.WriteLine("\n[3/3] Running ReplyTest...");
                    await ReplyTest.RunAsync();
                    break;

                default:
                    Console.WriteLine($"\nLua chon khong hop le: '{choice}'");
                    Console.WriteLine("Huong dan dung: dotnet run -- <logintest|chantest|replytest|all>");
                    break;
            }
        }
    }
}
