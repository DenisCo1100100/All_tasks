using System;         

namespace RefactoringCode
{
    class Character
    {
        private int age;
        private int strength;
        private int agility;
        private int intelligence;
        private int points;
        private string operation;
        private string subject;

        private string strengthVisual;
        private string agilityVisual;
        private string intelligenceVisual;

        private string operandPointsRaw;
        private int operandPoints;

        public Character()
        {
            age = 0;
            strength = 0;
            agility = 0;
            intelligence = 0;
            points = 25;
        }

        public void ExpensPoimts()
        {
            PrintInfoCharacter();

            while (points > 0)
            {
                ChangeCharacter();

                switch (subject.ToLower())
                {
                    case "сила":
                        CharacterOperations(ref operandPoints, ref strength);
                        break;

                    case "ловкость":
                        CharacterOperations(ref operandPoints, ref agility);
                        break;

                    case "интелект":
                        CharacterOperations(ref operandPoints, ref intelligence);
                        break;
                }

                PrintInfoCharacter();
            }

            EndCreateCharacter();
        }

        public void StartCreateCharacter()
        {
            Console.WriteLine("Добро пожаловать в меню выбора создания персонажа!\n" +
                "У вас есть 25 очков, которые вы можете распределить по умениям\n" +
                "Нажмите любую клавишу чтобы продолжить...");

            Console.ReadKey();
        }

        public void PrintInfoCharacter()
        {
            Console.Clear();

            strengthVisual = string.Empty;
            agilityVisual = string.Empty;
            intelligenceVisual = string.Empty;

            strengthVisual = strengthVisual.PadLeft(strength, '#').PadRight(10, '_');
            agilityVisual = agilityVisual.PadLeft(agility, '#').PadRight(10, '_');
            intelligenceVisual = intelligenceVisual.PadLeft(intelligence, '#').PadRight(10, '_');

            Console.WriteLine("Поинтов - {0}\nВозраст - {1}\nСила - [{2}]\nЛовкость - [{3}]\n" +
                "Интелект - [{4}]", points, age, strengthVisual, agilityVisual, intelligenceVisual);
        }

        private void ChangeCharacter()
        {
            Console.WriteLine("Какую характеристику вы хотите изменить?");
            subject = Console.ReadLine();

            Console.WriteLine(@"Что вы хотите сделать? +\-");
            operation = Console.ReadLine();

            Console.WriteLine(@"Колличество поинтов которые следует {0}", operation == "+" ? "прибавить " : "отнять");

            do
            {
                operandPointsRaw = Console.ReadLine();
            } while (!int.TryParse(operandPointsRaw, out operandPoints));
        }

        public void CharacterOperations(ref int operandPoints, ref int specifications)
        {
            if (operation == "+")
            {
                int overhead = operandPoints - (10 - specifications);
                overhead = overhead < 0 ? 0 : overhead;
                operandPoints -= overhead;
            }
            else
            {
                int overhead = specifications - operandPoints;
                overhead = overhead < 0 ? overhead : 0;
                operandPoints += overhead;
            }

            specifications = operation == "+" ? specifications + operandPoints : specifications - operandPoints;
            points = operation == "+" ? points - operandPoints : points + operandPoints;
        }

        public void EndCreateCharacter()
        {
            Console.WriteLine("Вы распределили все очки. Введите возраст персонажа:");

            string ageRaw;
            int operandAge;

            do
            {
                ageRaw = Console.ReadLine();
            } while (int.TryParse(ageRaw, out operandAge));
            age = operandAge;

            PrintInfoCharacter();
        }
    }
}