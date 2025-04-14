using System; using System.Collections.Generic;

class Word { private string _text; private bool _isHidden;

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

class Scripture { private Reference _reference; private List<Word> _words;

public Scripture(Reference reference, string text)
{
    _reference = reference;
    _words = new List<Word>();
    foreach (string word in text.Split(' '))
    {
        _words.Add(new Word(word));
    }
}

public void HideRandomWords(int numberToHide)
{
    Random rand = new Random();
    int hiddenCount = 0;

    while (hiddenCount < numberToHide)
    {
        int index = rand.Next(_words.Count);
        if (!_words[index].IsHidden)
        {
            _words[index].Hide();
            hiddenCount++;
        }
    }
}

public string GetDisplayText()
{
    List<string> displayWords = new List<string>();
    foreach (Word word in _words)
    {
        displayWords.Add(word.GetDisplayText());
    }
    return _reference.GetDisplayText() + " " + string.Join(" ", displayWords);
}

}

class Reference { private string _book; private int _chapter; private int _verse;

public Reference(string book, int chapter, int verse)
{
    _book = book;
    _chapter = chapter;
    _verse = verse;
}

public string GetDisplayText()
{
    return $"{_book} {_chapter}:{_verse}";
}

}

class Program { static void Main(string[] args) { // Creativity: Added random hiding and clean console refresh for a better experience Reference reference = new Reference("Proverbs", 3, 5); Scripture scripture = new Scripture(reference, "Trust in the Lord with all your heart and lean not on your own understanding");

while (true)
    {
        Console.Clear();
        Console.WriteLine(scripture.GetDisplayText());
        Console.WriteLine("\nPress Enter to hide more words or type 'quit' to exit.");

        string input = Console.ReadLine();
        if (input.ToLower() == "quit")
        {
            break;
        }
        scripture.HideRandomWords(2);
    }
}

}

