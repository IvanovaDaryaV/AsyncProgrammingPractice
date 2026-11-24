Console.WriteLine("Для запуска нажмите любую клавишу");
Console.ReadKey();

TaskScheduler scheduler = new StackTaskScheduler();
Task[] tasks = new Task[40];

for (int i = 0; i < 40; i++)
{
    var num = i + 1;
    tasks[i] = new Task(() =>
    {
        Thread.Sleep(3000);
        Console.WriteLine($"Выполнена задача #{num}");
    });
    tasks[i].Start(scheduler); // задача добавляется в стек и сразу кладется в очередь на запуск
}

Console.WriteLine("Задачи добавлены в стек");

// Не даем программе завершиться, пока все 40 фоновых задач
// из пула потоков не закончат свою работу
Task.WaitAll(tasks);

Thread.Sleep(2000);

Console.WriteLine("Программа завершила выполнение");

Console.ReadKey();