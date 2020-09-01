using System;
using System.Collections.Generic;


namespace DirectoryCapitals
{
    class Directory
    {
        #region privateConstant
        public const int REQUEST_STRING = 0;
        public const int REQUEST_COUNTRY = 1;
        public const int REQUEST_CAPITAL = 2;
        public const int LIST_COUNTRY = 0;
        public const int LIST_CAPITAL = 1;
        #endregion

        private static List<List<string>> listСities = new List<List<string>>();
        private static string outputData;

        public static void Controller()
        {
            switch (InputData.userSplitRequest[REQUEST_STRING])
            {
                case "CHANGE_CAPITAL":
                    Change();
                    break;
                case "RENAME":
                    Rename();
                    break;
                case "ABOUT":
                    About();
                    break;
                case "DUMP":
                    Dump();
                    break;
            }
        }

        private static void Change()
        {
            if (listСities.Count <= 0)
            {
                AddNewCountry();
            }
            else
            {
                if (!FindCountryInList())
                {
                    AddNewCountry();
                }
            }
        }

        private static void Rename()
        {
            if (CheckСoincidenceData())
            {
                outputData += "\nIncorrect rename, skip";
            }
            else
            {
                RenameCountry();
            }
        }

        private static void About()
        {
            FindCountryInList(InputData.userSplitRequest[REQUEST_COUNTRY]);
        }

        private static void Dump()
        {
            if (listСities.Count <= 0)
            {
                outputData += "\nThere are no countries in the world";
            }

            PrintDirectory();
        }


        private static void AddNewCountry()
        {
            listСities.Add(new List<string>() { InputData.userSplitRequest[REQUEST_COUNTRY], InputData.userSplitRequest[REQUEST_CAPITAL]});

            outputData += $"\nIntroduce new country {InputData.userSplitRequest[REQUEST_COUNTRY]} " +
                                      $"with capital {InputData.userSplitRequest[REQUEST_CAPITAL]}";
        }

        private static bool FindCountryInList()
        {
            foreach (var country in listСities)
            {
                if (country[LIST_COUNTRY] == InputData.userSplitRequest[REQUEST_COUNTRY])
                {
                    if (country[LIST_CAPITAL] != InputData.userSplitRequest[REQUEST_CAPITAL])
                    {
                        outputData += $"\nCountry {InputData.userSplitRequest[REQUEST_COUNTRY]} has changed its capital from {country[LIST_CAPITAL]} " +
                            $"to {InputData.userSplitRequest[REQUEST_CAPITAL]}";
                        country[LIST_CAPITAL] = InputData.userSplitRequest[REQUEST_CAPITAL];

                        return true;
                    }

                    return true;
                }
            }

            return false;
        }

        private static bool CheckСoincidenceData()
        {
            if (InputData.userSplitRequest[REQUEST_COUNTRY] == InputData.userSplitRequest[REQUEST_CAPITAL])
            {
                return true;
            }

            foreach (var country in listСities)
            {
                if (country[LIST_COUNTRY] == InputData.userSplitRequest[REQUEST_CAPITAL])
                {
                    return true;
                }
            }

            return false;
        }

        private static void RenameCountry()
        {
            foreach (var country in listСities)
            {
                if (country[0] == InputData.userSplitRequest[REQUEST_COUNTRY])
                {
                    outputData += $"\nCountry {country[LIST_COUNTRY]} with capital {country[LIST_CAPITAL]} has been renamed to {InputData.userSplitRequest[2]}";
                    country[LIST_COUNTRY] = InputData.userSplitRequest[2];

                    return;
                }
            }

            outputData += "\nIncorrect rename, skip";
        }

        private static void FindCountryInList(string soughtCountry) 
        {
            foreach (var country in listСities)
            {
                if (country[LIST_COUNTRY] == soughtCountry)
                {
                    outputData += $"\nCountry {country[LIST_COUNTRY]} has capital {country[LIST_CAPITAL]}";

                    return;
                }
            }

            outputData += "\nCountry country doesn't exis";
        }

        private static void PrintDirectory()
        {
            outputData += "\n";
            foreach (var country in listСities)
            {
                outputData += (country[LIST_COUNTRY] + "/" + country[LIST_CAPITAL] + " ");
            }
        }

        public static void PrintResult()
        {
            Console.WriteLine(outputData);
        }
    }
}