using System;
using System.Collections.Generic;
using System.Threading;

abstract class MindfulnessActivity
{
    protected int Duration;
    
    public void Start()
    {
        Console.WriteLine("Starting the activity...");
        Console.Write("Enter duration in seconds: ");
        Duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Get ready...");
        Thread.Sleep(2000);
        Run();
        End();
    }
    
    protected abstract void Run();
    
    private void End()
    {
        Console.WriteLine($"Good job! You completed the activity for {Duration} seconds.");
        Thread.Sleep(2000);
    }
}

class BreathingActivity : MindfulnessActivity
{
    protected override void Run()
    {
        Console.WriteLine("Breathe in...");
        Thread.Sleep(Duration * 500);
        Console.WriteLine("Breathe out...");
        Thread.Sleep(Duration * 500);
    }
}

class ReflectionActivity : MindfulnessActivity
{
    private static List<string> Prompts = new List<string>
    {
        "Think of a time you overcame a challenge.",
        "Think of a time you helped someone.",
        "Think of a time you achieved something great."
    };

    protected override void Run()
    {
        Random rand = new Random();
        string prompt = Prompts[rand.Next(Prompts.Count)];
        Console.WriteLine(prompt);
        Thread.Sleep(Duration * 1000);
    }
}

class ListingActivity : MindfulnessActivity
{
    protected override void Run()
    {
        Console.WriteLine("List as many positive things as you can:");
        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        List<string> items = new List<string>();

        while (DateTime.Now < endTime)
        {
            Console.Write("Enter an item: ");
            items.Add(Console.ReadLine());
        }

        Console.WriteLine($"You listed {items.Count} items!");
    }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing");
            Console.WriteLine("2. Reflection");
            Console.WriteLine("3. Listing");
            Console.WriteLine("4. Exit");

            switch (Console.ReadLine())
            {
                case "1": new BreathingActivity().Start(); break;
                case "2": new ReflectionActivity().Start(); break;
                case "3": new ListingActivity().Start(); break;
                case "4": return;
                default: Console.WriteLine("Invalid choice!"); break;
            }
        }
    }
}
