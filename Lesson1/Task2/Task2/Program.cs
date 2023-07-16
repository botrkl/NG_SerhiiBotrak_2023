Console.Write("Enter your number: ");
if (int.TryParse(Console.ReadLine(), out int number))
    Console.WriteLine("Our number plussed with 10: {0}", number += 10);
else
    Console.WriteLine("Incorrect value!");