using System; using System.Collections.Generic; using System.Linq;

namespace ScriptureMemorizer { class Word { private string _text; private bool _isHidden;

public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public bool IsHidden => _isHidden;

    public void Hide()
    {
        _isHidden = true;
    }

    public string GetDisplayText()
    {
        return IsHidden ? new string('_', _text.Length) : _text;
    }
}

class Scripture
{
    private string _reference;
    private List<Word> _words;

    public Scripture(string reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public void HideRandomWords(int numberToHide)
    {
        Random random = new Random();
        List<Word> visibleWords = _words.Where(w => !w.IsHidden).ToList();

        for (int i = 0; i < numberToHide && visibleWords.Any(); i++)
        {
            int index = random.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
        }
    }

    public string GetDisplayText()
    {
        string text = string.Join(" ", _words.Select(word => word.GetDisplayText()));
        return $"{_reference}\n{text}";
    }

    public bool AllWordsHidden()
    {
        return _words.All(word => word.IsHidden);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Welcome to Scripture Memorizer!");

        // Reference and text of the scripture
        string reference = "Proverbs 3:5-6";
        string text = "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths.";

        Scripture scripture = new Scripture(reference, text);

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to end.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }

            if (!scripture.AllWordsHidden())
            {
                scripture.HideRandomWords(3);
            }
            else
            {
                Console.WriteLine("All words are hidden. Press Enter to quit.");
                Console.ReadLine();
                break;
            }
        }
    }
}

}
