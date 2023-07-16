List<int> list = InitializeList(10);
Console.Write("Our List: ");
PrintAllListElements(list);

try
{
    Console.Write("Enter values to duplicate (separated by spaces): ");
    List<int> listOfDuplicateValues = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
    list = DuplicateSomeListValues(list, listOfDuplicateValues);
}
catch
{
    Console.WriteLine("Invalid value for duplicating!");
}
finally
{
    Console.Write("Our Result List: ");
    PrintAllListElements(list);
}


List<int> InitializeList(int length)
{
    List<int> list = new List<int>() { };
    Random random = new Random();

    for (int i = 0; i < length; i++)
        list.Add(random.Next(-25, 25));

    return list;
}
void PrintAllListElements(List<int> list)
{
    foreach (var item in list)
        Console.Write(item + " ");

    Console.WriteLine();
}

List<int> DuplicateSomeListValues(List<int> list, List<int> listOfDuplicateValues)
{
    List<int> resultList = new List<int>() { };
    for (int i = 0, counter = 0; i < list.Count; i++)
    {
        resultList.Add(list[i]); 
        if (listOfDuplicateValues.Contains(list[i]))
        {
            int index = i + (counter++) + 1;
            resultList.Insert(index, list[i]); 
        }
    }
    return resultList;
}