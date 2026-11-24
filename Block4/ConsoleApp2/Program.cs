using System.Numerics;

namespace ConsoleApp2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Для запуска нажмите любую клавишу");
            Console.ReadKey();

            Console.WriteLine("Вычисляем факториал асинхронно...");
            // await избавляет от ручного ожидания, как в задании 1
            await CalculateFactorialAsync(20);

            Console.WriteLine("\nПрограмма завершила выполнение");
            Console.ReadKey();

            async Task CalculateFactorialAsync(int target)
            {
                var result = await Task.Run(() => CalculateFactorial(target));

                // Все, что после await, C# автоматически превращает в продолжение задачи
                // аналогично "ContinueWith()"
                Console.WriteLine(result);
            }

            BigInteger CalculateFactorial(int target)
            {
                BigInteger result = 1;

                for (int i = 1; i <= target; i++)
                {
                    result *= i;
                    Thread.Sleep(100);
                }

                return result;
            }
        }
    }
}
