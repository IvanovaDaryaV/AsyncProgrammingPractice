using System.Text;

Console.WriteLine("Для запуска нажмите любую клавишу");
Console.ReadKey();

var arr = new int[1_000_000];
for (int i = 0; i < arr.Length; i++)
{
    arr[i] = i; 
}

var task = SortArrayAsync(true, arr).ContinueWith((t) =>
{
    var sorted = t.Result;

    var sb = new StringBuilder();
    for (int i = 0; i < sorted.Length; i++)
    {
        sb.Append(sorted[i].ToString());

        if (i < sorted.Length - 1)
            sb.Append(", ");
    }
    Console.WriteLine(sb);
});

await task;

static Task<int[]> SortArrayAsync(bool isAscending, params int[] array)
{
    return Task.Run(() => SortArray(isAscending, array));
}

static int[] SortArray(bool isAscending, params int[] array)
{
    Array.Sort(array);

    if(!isAscending)
        Array.Reverse(array);

    return array;
}