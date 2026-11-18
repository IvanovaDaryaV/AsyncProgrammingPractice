var number = 40;

Console.WriteLine("Для запуска нажмите любую клавишу");
Console.ReadKey();

var task = FindLastFibonacciNumberAsync(number).ContinueWith((t) => Console.WriteLine($"Результат: {t.Result}"));
await task;

static Task<double> FindLastFibonacciNumberAsync(int number)
{
    return Task.Factory.StartNew(() => FindLastFibonacciNumber(number), TaskCreationOptions.LongRunning);
}

static double FindLastFibonacciNumber(int number)
{
    Func<int, double> fib = null;
    fib = (x) => x > 1 ? fib(x - 1) + fib(x - 2) : x;
    return fib.Invoke(number);
}