using System.Text;

string initialTextPath = "C:\\Projects\\other projects\\AsyncProgrammingCource\\Block4\\ConsoleApp3\\text.txt";
string resultTextPath = "C:\\Projects\\other projects\\AsyncProgrammingCource\\Block4\\ConsoleApp3\\result.txt";

var text = string.Empty;
using (StreamReader reader = new(initialTextPath, Encoding.UTF8))
{
    text = await reader.ReadToEndAsync();
}
var task = ParseAsync(text);

Console.WriteLine("Введите свое имя:");
var name = Console.ReadLine();

using var fileStream = new FileStream(resultTextPath, FileMode.Create, FileAccess.Write);
using var writer = new StreamWriter(fileStream, Encoding.UTF8);

var parseResult = await task;

writer.Write($"{name} нашёл {parseResult.Count} уникальных слов. Перечисление слов: ");
writer.Write(string.Join(", ", parseResult) + ".");

Console.WriteLine("Программа завершила работу.");
Console.ReadKey();

Task<IList<string>> ParseAsync(string inputData)
{
    return Task.Run(() => Parse(inputData));
}

IList<string> Parse(string inputData)
{
    // Разбиение текста по любому из трех символов: пробел, запятая, точка
    var arr = inputData.Split(" ,.".ToCharArray(), StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    var uniqueCharsList = new HashSet<string>(arr).ToList();

    return uniqueCharsList;
}