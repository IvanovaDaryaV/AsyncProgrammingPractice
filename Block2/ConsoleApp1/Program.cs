Console.WriteLine("Для запуска нажмите любую клавишу");
Console.ReadKey();

var task = WriteCharAsync('!');

while(!task.IsCompleted)
{
    Console.Write("$");
    Thread.Sleep(500);
}

task.Wait(); // гарантирует, что задача завершилась

Console.WriteLine("\nМетод Main закончил свою работу");

Task WriteCharAsync(object symbol)
{
    return Task.Run(() => WriteChar(symbol));
}

static void WriteChar(object symbol)
{
    var item = (char)symbol;

    for (int i = 0; i < 160; i++)
    {
        Console.Write(item);
        Thread.Sleep(500);
    }
}