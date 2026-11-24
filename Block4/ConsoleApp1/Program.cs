using System.Numerics;

Console.WriteLine("Для запуска нажмите любую клавишу");
Console.ReadKey();

var task = CalculateFactorialAsync(33);

while (!task.IsCompleted)
{
    Console.Write("* "); 
    Thread.Sleep(100);
}

task.Wait(); // гарантирует, что задача завершилась

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