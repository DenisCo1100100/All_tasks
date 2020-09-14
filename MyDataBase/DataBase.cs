using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MyDataBase
{
    enum WordsRequest
    {
        Request,
        Date, 
        Event
    }

    class InputData
    {
        public static string UserRequest { get; set; }
        public static string[] splitRequest;

        static void Main()
        {
            ReadUserRequest();

            Console.WriteLine("Press key to close");
            Console.ReadKey();
        }

        private static void ReadUserRequest()
        {
            Console.Write("==>");
            UserRequest = Console.ReadLine().ToLower();
            splitRequest = UserRequest.Split();

            while (splitRequest[(int)WordsRequest.Request] != "end")
            {
                DataBase.ControllerRequest();

                Console.Write("==>");
                UserRequest = Console.ReadLine().ToLower();
                splitRequest = UserRequest.Split();
            }
        }
    }

    class DataBase
    {
        private static Dictionary<string, List<string>> dataBase = new Dictionary<string, List<string>>();

        public static void ControllerRequest()
        {
            string command = InputData.splitRequest[(int)WordsRequest.Request];

            switch (command)
            {
                case "add":
                    Add();
                    break;
                case "del":
                    DeletEventOrDate();
                    break;
                case "find":
                    Find();
                    break;
                case "print":
                    Print();
                    break;
                default:
                    ProcessingUnknownCommand(command);
                    break;
            }
        }

        private static void ProcessingUnknownCommand(string command)
        {
            if (command != "")
            {
                Console.WriteLine($"Unknown command: {command}");
            }
        }


        private static void Add()
        {
            DateFormatCheck();

            string key = InputData.splitRequest[(int)WordsRequest.Date];
            string stringEvent = null;

            for (int i = (int)WordsRequest.Event; i < InputData.splitRequest.Length; i++)
            {
                stringEvent += InputData.splitRequest[i];
            }

            if (dataBase.ContainsKey(key))
            {
                dataBase[key].Add(stringEvent);
            }
            else
            {
                dataBase.Add(key, new List<string> { stringEvent });
            }
        }

        private static void DeletEventOrDate()
        {
            DateFormatCheck();

            if (InputData.splitRequest.Length == 3)
            {
                DeleteEvent();
            }
            else
            {
                DeleteDate();
            }
        }

        private static void DeleteEvent()
        {
            string date = InputData.splitRequest[(int)WordsRequest.Date];

            if (dataBase.ContainsKey(date))
            {
                foreach (var searchEvent in dataBase[date])
                {
                    if (searchEvent == InputData.splitRequest[(int)WordsRequest.Event])
                    {
                        dataBase[date].Remove(searchEvent);
                        Console.WriteLine("Delete successfully");

                        if (dataBase[date].Count == 0)
                        {
                            dataBase.Remove(date);
                        }
                        
                        return;
                    }
                }
            }

            Console.WriteLine("Event not found");
        }

        private static void DeleteDate()
        {
            string date = InputData.splitRequest[(int)WordsRequest.Date];

            if (dataBase.ContainsKey(date))
            {
                Console.WriteLine("Delete " + dataBase[date].Count + " events");
                dataBase.Remove(date);
            }
            else
            {
                Console.WriteLine("Delete 0 events");
            }
        }

        private static void Find()
        {
            DateFormatCheck();

            foreach (var date in dataBase)
            {
                if (date.Key == InputData.splitRequest[(int)WordsRequest.Date])
                {
                    date.Value.Sort();

                    foreach (var item in date.Value)
                    {
                        Console.WriteLine(item);
                    }

                    return;
                }
            }

            Console.WriteLine("Event not found");
        }
        
        private static void Print()
        {
            var sortDict = new SortedDictionary<string, List<string>>(dataBase);

            foreach (var date in sortDict)
            {
                int cnt = 0;
                MatchEvaluator ev = new MatchEvaluator(m => m.Groups[0].ToString().PadLeft(cnt++ > 0 ? 2 : 4, '0'));
                string outputDate = Regex.Replace(date.Key, @"\d{1,4}", ev);
                Console.Write(outputDate + " ");


                date.Value.Sort();
                foreach (var item in date.Value) 
                { 
                    Console.Write(item + " ");
                }

                Console.WriteLine();
            }
        }

        private static void DateFormatCheck()
        {
            string date = InputData.splitRequest[(int)WordsRequest.Date];
            Regex regex = new Regex(@"^[-\+]?(\d{1,10})[-]([-\+]?(0?[1-9]|[-\+]?1[0-2]))[-]([-\+]?([0-2]?[1-9]|[+\-]?[1-3][0-1]))$");

            if (!regex.IsMatch(date))
            {
                throw new Exception($"Wrong date format: {date}");
            }

            int month = Convert.ToInt32(regex.Match(date).Groups[2].ToString());
            int day = Convert.ToInt32(regex.Match(date).Groups[4].ToString());

            FindErrorDate(month, day);
            DeletPlusInDate(ref InputData.splitRequest[(int)WordsRequest.Date]);
        }

        private static void FindErrorDate(int month, int day)
        {
            if (month > 12 || month < 1)
            {
                throw new Exception($"Month value is invalid: {month}");
            }
            else if (day > 31 || day < 1)
            {
                throw new Exception($"Day value is invalid: {day}");
            }
        }

        private static void DeletPlusInDate(ref string date)
        {
            string outputDate = "";

            for (int i = 0; i < date.Length; i++)
            {
                if (date[i] != '+')
                {
                    outputDate += date[i];
                }
            }

            date = outputDate;
        }
    }
}