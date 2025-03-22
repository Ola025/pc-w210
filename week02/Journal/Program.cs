 using System;
using System.Collections.Generic;
using System.IO;

class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    public Entry(string date, string prompt, string response)
    {
        Date = date;
        Prompt = prompt;
        Response = response;
    }

    public override string ToString()
    {
        return $"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
    }
}

class Journal
{
    private List<Entry> _entries = new List<Entry>();
    private List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    private Random _random = new Random();

    public void AddEntry()
    {
        string prompt = _prompts[_random.Next(_prompts.Count)];
        Console.WriteLine("\n" + prompt);
        Console.Write("Your response: ");
        string response = Console.ReadLine();

        string date = DateTime.Now.ToShortDateString();
        _entries.Add(new Entry(date, prompt, response));

        Console.WriteLine("Entry added.\n");
    }

    public void DisplayEntries()
    {
        Console.WriteLine();
        if (_entries.Count == 0)
            Console.WriteLine("The journal is empty.\n");
        else
            foreach (Entry entry in _entries)
                Console.WriteLine(entry.ToString());
    }

    public void SaveJournal()
    {
        Console.Write("Enter filename to save: ");
        string filename = Console.ReadLine();

        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Entry entry in _entries)
            {
                writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }

        Console.WriteLine("Journal saved!\n");
    }

    public void LoadJournal()
    {
        Console.Write("Enter filename to load: ");
        string filename = Console.ReadLine();

        if (File.Exists(filename))
        {
            _entries.Clear();
            string[] lines = File.ReadAllLines(filename);

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                _entries.Add(new Entry(parts[0], parts[1], parts[2]));
            }

            Console.WriteLine("Journal loaded!\n");
        }
        else
        {
            Console.WriteLine("File not found.\n");
        }
    }
}
      class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Journal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Save journal to file");
            Console.WriteLine("4. Load journal from file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    journal.AddEntry();
                    break;
                case "2":
                    journal.DisplayEntries();
                    break;
                case "3":
                    journal.SaveJournal();
                    break;
                case "4":
                    journal.LoadJournal();
                    break;
                case "5":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.\n");
                    break;
            }
        }
    }
}