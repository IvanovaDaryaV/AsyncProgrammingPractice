Console.WriteLine("Для запуска нажмите любую клавишу");
Console.ReadKey();

ThreadWrapper wrapper = new ThreadWrapper(new Action<object>(StarWriter));

wrapper.Start('*');

for (int i = 0; i < 40; i++)
{
    Console.Write('-');
    Thread.Sleep(50);
}

wrapper.Wait();

Console.WriteLine($"Программа закончила свою работу.");

void StarWriter(object arg)
{
    char item = (char)arg;

    for (int i = 0; i < 120; i++)
    {
        Console.Write(item);
        Thread.Sleep(50);
    }
}