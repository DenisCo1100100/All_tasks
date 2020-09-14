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
            try
            {
                ReadUserRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ReadUserRequest()
        {
            Console.Write("==>");
            UserRequest = Console.ReadLine();
            splitRequest = UserRequest.Split();

            while (splitRequest[(int)WordsRequest.Request] != "End")
            {
                MyDataBase.ControllerRequest();

                Console.Write("==>");
                UserRequest = Console.ReadLine();
                splitRequest = UserRequest.Split();
            }
        }
    }

    class MyDataBase
    {
        private static Dictionary<string, List<string>> DataBase = new Dictionary<string, List<string>>();

        public static void ControllerRequest()
        {
            string command = InputData.splitRequest[(int)WordsRequest.Request];

            switch (command)
            {
                case "Add":
                    Add();
                    break;
                case "Del":
                    DeletEventOrDate();
                    break;
                case "Find":
                    Find();
                    break;
                case "Print":
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
                throw new Exception($"Unknown command: {command}");
        }

        private static void Add()
        {
            if (InputData.splitRequest.Length == (int)WordsRequest.Date)
            {
                throw new Exception("Wrong date format");
            }
            else if(InputData.splitRequest.Length == (int)WordsRequest.Event)
            {
                DateFormatCheck();
                return;
            }

            DateFormatCheck();

            string key = AddZeroInDate(InputData.splitRequest[(int)WordsRequest.Date]);
            string stringEvent = InputData.splitRequest[(int)WordsRequest.Event];

            if (stringEvent == null)
                return;

            if (DataBase.ContainsKey(key))
            {
                if (!FindEvent(key, stringEvent))
                    DataBase[key].Add(stringEvent);
            }
            else
            {
                DataBase.Add(key, new List<string> { stringEvent });
            }
        }

        private static bool FindEvent(string key, string searchedEvent)
        {
            foreach (var item in DataBase[key])
            {
                if (item == searchedEvent)
                    return true;
            }

            return false;
        }

        private static void DeletEventOrDate()
        {
            if (InputData.splitRequest.Length == (int)WordsRequest.Date)
            {
                throw new Exception("Wrong date format");
            }

            DateFormatCheck();

            if (InputData.splitRequest.Length >= 3)
                DeleteEvent();
            else
                DeleteDate();
        }

        private static void DeleteEvent()
        {
            string date = AddZeroInDate(InputData.splitRequest[(int)WordsRequest.Date]);

            if (DataBase.ContainsKey(date))
            {
                foreach (var searchEvent in DataBase[date])
                {
                    if (searchEvent == InputData.splitRequest[(int)WordsRequest.Event])
                    {
                        DataBase[date].Remove(searchEvent);
                        Console.WriteLine("Delete successfully");

                        if (DataBase[date].Count == 0)
                        {
                            DataBase.Remove(date);
                        }

                        return;
                    }
                }
            }

            Console.WriteLine("Event not found");
        }

        private static void DeleteDate()
        {
            string date = AddZeroInDate(InputData.splitRequest[(int)WordsRequest.Date]);

            if (DataBase.ContainsKey(date))
            {
                Console.WriteLine("Delete " + DataBase[date].Count + " events");
                DataBase.Remove(date);
            }
            else
            {
                Console.WriteLine("Delete 0 events");
            }
        }

        private static void Find()
        {
            if (InputData.splitRequest.Length == (int)WordsRequest.Date)
            {
                throw new Exception("Wrong date format");
            }

            DateFormatCheck();

            foreach (var date in DataBase)
            {
                string keyInDict = AddZeroInDate(date.Key);
                string keyInRequest = AddZeroInDate(InputData.splitRequest[(int)WordsRequest.Date]);

                if (keyInDict == keyInRequest)
                {
                    date.Value.Sort();

                    foreach (var item in date.Value)
                    {
                        Console.WriteLine(item);
                    }

                    return;
                }
            }
        }

        private static void Print()
        {
            var sortDict = new SortedDictionary<string, List<string>>(DataBase);

            foreach (var date in sortDict)
            {
                string outputDate = AddZeroInDate(date.Key);

                date.Value.Sort();
                foreach (var item in date.Value)
                    Console.WriteLine(outputDate + " " + item);
            }
        }

        private static void DateFormatCheck()
        {
            string date = InputData.splitRequest[(int)WordsRequest.Date];
            Regex regex = new Regex(@"^[-\+]?(\d{1,10})[-]([-\+]?[0-9]?[0-9]?)[-]([+\-]?[0-9]?[0-9]?)$");

            if (!regex.IsMatch(date))
                throw new Exception($"Wrong date format: {date}");

            int month = Convert.ToInt32(regex.Match(date).Groups[2].ToString());
            int day = Convert.ToInt32(regex.Match(date).Groups[3].ToString());

            FindErrorDate(month, day);
            DeletPlusInDate(ref InputData.splitRequest[(int)WordsRequest.Date]);
        }

        private static void FindErrorDate(int month, int day)
        {
            if (month > 12 || month < 1)
                throw new Exception($"Month value is invalid: {month}");

            else if (day > 31 || day < 1)
                throw new Exception($"Day value is invalid: {day}");
        }

        private static void DeletPlusInDate(ref string date)
        {
            string outputDate = "";

            for (int i = 0; i < date.Length; i++)
            {
                if (date[i] != '+')
                    outputDate += date[i];
            }

            date = outputDate;
        }

        private static string AddZeroInDate(string date)
        {
            int cnt = 0;
            MatchEvaluator ev = new MatchEvaluator(m => m.Groups[0].ToString().PadLeft(cnt++ > 0 ? 2 : 4, '0'));
            string outputDate = Regex.Replace(date, @"\d{1,4}", ev);

            return outputDate;
        }
    }
}