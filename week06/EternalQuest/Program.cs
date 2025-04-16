// Program.cs using System; using System.Collections.Generic;

class Program { static void Main(string[] args) { GoalManager manager = new GoalManager(); manager.Start(); } }

// Goal.cs public abstract class Goal { protected string _name; protected string _description; protected int _points;

public Goal(string name, string description, int points)
{
    _name = name;
    _description = description;
    _points = points;
}

public abstract int RecordEvent();
public abstract bool IsComplete();
public abstract string GetDetails();
public abstract string GetSaveString();

}

// SimpleGoal.cs public class SimpleGoal : Goal { private bool _isComplete = false;

public SimpleGoal(string name, string description, int points) : base(name, description, points) { }

public override int RecordEvent()
{
    if (!_isComplete)
    {
        _isComplete = true;
        return _points;
    }
    return 0;
}

public override bool IsComplete() => _isComplete;

public override string GetDetails() => $"[{(_isComplete ? "X" : " ")}] {_name} ({_description})";

public override string GetSaveString() => $"SimpleGoal:{_name}|{_description}|{_points}|{_isComplete}";

}

// EternalGoal.cs public class EternalGoal : Goal { public EternalGoal(string name, string description, int points) : base(name, description, points) { }

public override int RecordEvent() => _points;

public override bool IsComplete() => false;

public override string GetDetails() => $"[∞] {_name} ({_description})";

public override string GetSaveString() => $"EternalGoal:{_name}|{_description}|{_points}";

}

// ChecklistGoal.cs public class ChecklistGoal : Goal { private int _targetCount; private int _completedCount; private int _bonus;

public ChecklistGoal(string name, string description, int points, int targetCount, int bonus) : base(name, description, points)
{
    _targetCount = targetCount;
    _bonus = bonus;
    _completedCount = 0;
}

public override int RecordEvent()
{
    _completedCount++;
    return _points + (_completedCount == _targetCount ? _bonus : 0);
}

public override bool IsComplete() => _completedCount >= _targetCount;

public override string GetDetails() => $"[{(IsComplete() ? "X" : " ")}] {_name} ({_description}) -- Completed: {_completedCount}/{_targetCount}";

public override string GetSaveString() => $"ChecklistGoal:{_name}|{_description}|{_points}|{_targetCount}|{_bonus}|{_completedCount}";

}

// GoalManager.cs using System.IO;

public class GoalManager { private List<Goal> _goals = new List<Goal>(); private int _score = 0;

public void Start()
{
    string input;
    do
    {
        Console.WriteLine("\nEternal Quest Menu:");
        Console.WriteLine("1. Create New Goal");
        Console.WriteLine("2. List Goals");
        Console.WriteLine("3. Save Goals");
        Console.WriteLine("4. Load Goals");
        Console.WriteLine("5. Record Event");
        Console.WriteLine("6. Show Score");
        Console.WriteLine("0. Exit");
        Console.Write("Choose an option: ");
        input = Console.ReadLine();

        switch (input)
        {
            case "1": CreateGoal(); break;
            case "2": ListGoals(); break;
            case "3": SaveGoals(); break;
            case "4": LoadGoals(); break;
            case "5": RecordEvent(); break;
            case "6": Console.WriteLine($"Your score: {_score}"); break;
        }

    } while (input != "0");
}

private void CreateGoal()
{
    Console.WriteLine("Select Goal Type:");
    Console.WriteLine("1. Simple");
    Console.WriteLine("2. Eternal");
    Console.WriteLine("3. Checklist");
    string choice = Console.ReadLine();

    Console.Write("Enter name: ");
    string name = Console.ReadLine();
    Console.Write("Enter description: ");
    string description = Console.ReadLine();
    Console.Write("Enter points: ");
    int points = int.Parse(Console.ReadLine());

    switch (choice)
    {
        case "1":
            _goals.Add(new SimpleGoal(name, description, points));
            break;
        case "2":
            _goals.Add(new EternalGoal(name, description, points));
            break;
        case "3":
            Console.Write("Target count: ");
            int target = int.Parse(Console.ReadLine());
            Console.Write("Bonus points: ");
            int bonus = int.Parse(Console.ReadLine());
            _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
            break;
    }
}

private void ListGoals()
{
    Console.WriteLine("Goals:");
    for (int i = 0; i < _goals.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {_goals[i].GetDetails()}");
    }
}

private void SaveGoals()
{
    Console.Write("Filename to save: ");
    string filename = Console.ReadLine();
    using (StreamWriter file = new StreamWriter(filename))
    {
        file.WriteLine(_score);
        foreach (Goal goal in _goals)
        {
            file.WriteLine(goal.GetSaveString());
        }
    }
}

private void LoadGoals()
{
    Console.Write("Filename to load: ");
    string filename = Console.ReadLine();
    string[] lines = File.ReadAllLines(filename);

    _goals.Clear();
    _score = int.Parse(lines[0]);

    for (int i = 1; i < lines.Length; i++)
    {
        string[] parts = lines[i].Split(":");
        string type = parts[0];
        string[] data = parts[1].Split("|");

        switch (type)
        {
            case "SimpleGoal":
                _goals.Add(new SimpleGoal(data[0], data[1], int.Parse(data[2]))
                {
                    // manually restoring completion
                    _isComplete = bool.Parse(data[3])
                });
                break;
            case "EternalGoal":
                _goals.Add(new EternalGoal(data[0], data[1], int.Parse(data[2])));
                break;
            case "ChecklistGoal":
                ChecklistGoal goal = new ChecklistGoal(data[0], data[1], int.Parse(data[2]), int.Parse(data[3]), int.Parse(data[4]));
                for (int j = 0; j < int.Parse(data[5]); j++) goal.RecordEvent();
                _goals.Add(goal);
                break;
        }
    }
}

private void RecordEvent()
{
    ListGoals();
    Console.Write("Choose goal number to record: ");
    int index = int.Parse(Console.ReadLine()) - 1;
    if (index >= 0 && index < _goals.Count)
    {
        int pointsEarned = _goals[index].RecordEvent();
        _score += pointsEarned;
        Console.WriteLine($"You earned {pointsEarned} points!");

        if (_score % 100 == 0)
        {
            Console.WriteLine("Level up! You're doing amazing!");
        }
    }
}

}

<<<<<<< HEAD
// Extra Feature: Level up notification every 100 points. You can also categorize goals for more creativity.
=======
// Extra Feature: Level up notification every 100 points. You can also categorize goals for more creativity.
>>>>>>> 1b00875570d8d582fad28326fa4871a1d758f00a
