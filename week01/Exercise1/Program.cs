using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("What is your fristname.");
        string fristname = Console.ReadLine();
        
        Console.WriteLine("What is your last name");
        string lastname = Console.ReadLine();
        
        Console.WriteLine($"My name is: {lastname}, {fristname} {lastname}");
        
    }
}