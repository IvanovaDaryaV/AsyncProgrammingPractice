Console.WriteLine("Для запуска нажмите любую клавишу");
Console.ReadKey();

TaskScheduler scheduler = new DelayTaskScheduler();

var task = new Task(() =>
{
    Console.WriteLine($"Идентификатор потока: {Thread.CurrentThread.ManagedThreadId}, " +
        $"из пула потоков: {Thread.CurrentThread.IsThreadPoolThread}");
});
task.Start(scheduler);

while (!task.IsCompleted)
{
    Console.Write("* ");
    Thread.Sleep(100);
}

Console.WriteLine("Программа завершила выполнение");

Console.ReadKey();