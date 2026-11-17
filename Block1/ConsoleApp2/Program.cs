Console.WriteLine("Для запуска нажмите любую клавишу");
Console.ReadKey();

ThreadWrapper<int> wrapper = new ThreadWrapper<int>(Calculate);

wrapper.Start(1000);

while(!wrapper.IsCompleted)
{
    Console.Write('!');
    Thread.Sleep(100);
}

Console.WriteLine($"\nРезультат асинхронной операции: {wrapper.Result:N}");
Console.WriteLine($"Программа закончила свою работу.");

int Calculate(object sleepTime)
{
    var sleepTimeInt = (int)sleepTime;
    var sum = 0;

    for (int i = 0; i < 10; i++)
    {
        sum += i;
        Thread.Sleep(sleepTimeInt);
    }

    return sum;
}