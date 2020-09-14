using System;
using System.Text;

namespace PaswordGenerator
{
    class Program
    {
        private const int PASSWORD_LENGTH = 10;
        private static string answerUser;
        private static string generatedPassword = "";

        private delegate char MyDelegate();
        private static MyDelegate[] generateSembolFunction;

        static void Main()
        {
            Console.Write("Password generation with repeats(Y/N):");
            answerUser = Console.ReadLine();

            generateSembolFunction = new MyDelegate[] { 
                GeneratingLowerСharacters, 
                GeneratingUpperCharacters,
                GenerationSpecialСharacters,
                GenerationNumberСharacters
            };

            try
            {
                GeneratRandomSymbols(RepitSymbol(answerUser));
                generatedPassword = AddHyphen();
                Console.WriteLine("Generated password: " + generatedPassword);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Press key to close");
            Console.ReadKey();
        }

        private static void GeneratRandomSymbols(bool repitingSybol)
        {
            Random random = new Random();
            int typeSymbol;

            while (generatedPassword.Length != PASSWORD_LENGTH)
            {
                typeSymbol = random.Next(0, 4);
                
                if (repitingSybol)
                {
                    generatedPassword += generateSembolFunction[typeSymbol]();
                }
                else
                {
                    CheckRepitSymbols(typeSymbol);
                }
            }
        }

        private static void CheckRepitSymbols(int typeSymbol)
        {
            char receivedChar = generateSembolFunction[typeSymbol]();

            for (int i = 0; i < generatedPassword.Length; i++)
            {
                if (generatedPassword[i] == receivedChar)
                {
                    return;
                }
            }

            generatedPassword += receivedChar;
        }

        private static bool RepitSymbol(string answerUser)
        {
            return answerUser == "Y" ? true : answerUser == "N" ? false : throw new Exception("Wrong command");
        }

        private static char GeneratingLowerСharacters()
        {
            Random random = new Random();
            int symbolCode = random.Next(97, 123);

            return Convert.ToChar(symbolCode);
        }

        private static char GeneratingUpperCharacters()
        {
            Random random = new Random();
            int symbolCode = random.Next(65, 91);

            return Convert.ToChar(symbolCode);
        }

        private static char GenerationSpecialСharacters()
        {
            Random random = new Random();
            int symbolCode = random.Next(33, 39);

            while (symbolCode == 34)
            {
                symbolCode = random.Next(33, 39);
            }

            return Convert.ToChar(symbolCode);
        }

        private static char GenerationNumberСharacters()
        {
            Random random = new Random();
            int symbolCode = random.Next(48, 58);

            return Convert.ToChar(symbolCode);
        }

        private static string AddHyphen()
        {
            StringBuilder resultPassword = new StringBuilder(generatedPassword);

            resultPassword.Insert(2, "-");
            resultPassword.Insert(5, "-");
            resultPassword.Insert(9, "-");

            return resultPassword.ToString();
        }
    }
}