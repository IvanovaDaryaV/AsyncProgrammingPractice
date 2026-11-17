Console.WriteLine("Для запуска нажмите любую клавишу");
Console.ReadKey();

var result = WriteCharAsync('!');

for (int i = 0; i < 160; i++)
{
    Console.WriteLine("$");
    Thread.Sleep(500);
}

Task WriteCharAsync(object symbol)
{
    return Task.Run(() => WriteChar(symbol));
}

static void WriteChar(object symbol)
{
    var item = (char)symbol;

    for (int i = 0; i < 160; i++)
    {
        Console.WriteLine(item);
        Thread.Sleep(500);
    }
}