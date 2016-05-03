using System;
using System.Threading;

namespace Filippova.Nsudotnet.NumberGuesser
{
    class Guesser
    {
        private static readonly string[] Oath = new[] { "Ну вы даёте, {0}! Не можете отгадать простенькое число, заданное компьютером??",
            "Мда... {0}, вы меня разочаровываете :(", "Официально заявляю, что {0} - полный кретин ",
            "Кругом идиоты. В том числе и вы, {0}!" };

        private static void Main()
        {
            Console.WriteLine("Приветствую! Введите свое имя ");
            var userName = Console.ReadLine();
            
            while (string.IsNullOrEmpty(userName))
            {
                Console.WriteLine("Вы не ввели имя. Введите имя");
                userName = Console.ReadLine();
            }
            var random = new Random();
            var number = random.Next(0, 101);
            var startDate = DateTime.Now;
            Console.WriteLine("{0}, угадайте загаданное мною число от 0 до 100", userName);
            var allAnswers = new int[1000];
            var count = 0;
            var isGuessed = false;
            while (!isGuessed)
            {
                var answer = Console.ReadLine();
                if (string.Equals(answer, "q"))
                {
                    Console.WriteLine("Извините.");
                    break;
                }
                int attempt;
                if (int.TryParse(answer, out attempt))
                {
                    if (attempt == number)
                    {
                        Console.WriteLine("Вы отгадали, {0}! Количество попыток: {1} ", userName, count);
                        for (var i = 0; i < count; i++)
                        {
                            Console.WriteLine(string.Concat(allAnswers[i], " ", (allAnswers[i] > number) ? ">" : "<"));
                        }
                        Console.WriteLine("На отгадывание ушло {0} минут", (DateTime.Now.Subtract(startDate).Seconds/60.0));
                        isGuessed = true;
                        Console.ReadLine();
                    }
                    else
                    {
                        allAnswers[count] = attempt;
                        Console.WriteLine(attempt > number
                            ? "Введенное число больше загаданного"
                            : "Введенное число меньше загаданного");
                        count++;
                        if (count%4 == 0)
                        {
                            Console.WriteLine(Oath[random.Next(0, Oath.Length)], userName);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Вы должны ввести число!");
                }
            }
            Console.WriteLine("До свидания, {0}!", userName);
            Thread.Sleep(2000);
        }
    }
}