Console.Write("Enter first string: ");
string str1 = Console.ReadLine();

Console.Write("Enter second string: ");
string str2 = Console.ReadLine();

string? resultString = sd(str1, str2);
Console.WriteLine("Our result: " + (resultString ?? "is null"));

static string? sd(string str1, string str2)
{
    if (str1.Length > str2.Length)
    {
        return string.Concat(str1, str2);
    }
    else  if (str2.Length > str1.Length && !string.IsNullOrEmpty(str1))
    {
        int index = str2.IndexOf(str1[0]);
        return str2.Substring(0, index) + " " + str2.Substring(index);
    }
    return null;
}
