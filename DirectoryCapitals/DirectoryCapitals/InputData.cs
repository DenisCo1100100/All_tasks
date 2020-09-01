using System;

namespace DirectoryCapitals
{
    class InputData
    {
        public static string[] userSplitRequest;

        static void Main(string[] args)
        {
            ReadUserRequest();

            Console.WriteLine("\n==================>RESULT<===================");
            Directory.PrintResult();
            Console.ReadKey();
        }

        public static void ReadUserRequest()
        {
            int amountRequest = Convert.ToInt32(Console.ReadLine());
            
            for (int i = 0; i < amountRequest; i++)
            {
                userSplitRequest = Console.ReadLine().Split();
                Directory.Controller();
            }
        }
    }
}
