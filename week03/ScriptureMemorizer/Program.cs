using System;

using System;
using System.Collections.Generic;

namespace ScriptureMemorizer
{
    // My Scripture Memorizer Program
    // 
    // This program displays a scripture (reference + text),
    // then hides a few random words each time you press Enter.
    // Type "quit" to stop, or keep going until all words are hidden.
    //
    // Classes:
    // 1) Program    - Entry point, handles user interaction.
    // 2) Reference  - Stores and formats the scripture reference.
    // 3) Word       - Represents each individual word (hidden or not).
    // 4) Scripture  - Combines reference + words, with methods to display/hide words.
    //
    // Exceeding Requirements (Example Ideas):
    // - Could load multiple scriptures from a file
    // - Could show hints if the user wants them
    // - Could only hide words that aren't already hidden (a bit trickier)
    //

    class Program
    {
        static void Main(string[] args)
        {
            // A little greeting for my program
            Console.WriteLine("Hello! Welcome to my Scripture Memorizer Program.");
            Console.WriteLine("Press Enter to start hiding words, or type 'quit' at any time to exit.");
            Console.WriteLine();

            // Here I'm setting up a single verse for demonstration: John 3:16
            Reference reference = new Reference("John", "3", "16");

            // This is the text for the scripture.
            string scriptureText = "For God so loved the world that he gave his one and only Son, " +
                                   "that whoever believes in him shall not perish but have eternal life.";

            // Create the Scripture object.
            Scripture scripture = new Scripture(reference, scriptureText);

            // Main loop - keep going until user quits or everything is hidden.
            while (true)
            {
                // Clear the console so it looks neat every time we display the scripture.
                Console.Clear();

                // Display the current state of the scripture (some words may be hidden).
                scripture.DisplayScripture();

                // If all words are hidden, let's congratulate and end the loop.
                if (scripture.AllWordsHidden())
                {
                    Console.WriteLine("\nAwesome! You've hidden all the words.");
                    break;
                }

                Console.WriteLine("\nPress Enter to hide more words, or type 'quit' to exit:");
                string userInput = Console.ReadLine().Trim().ToLower();

                // If the user typed "quit," let's end.
                if (userInput == "quit")
                {
                    break;
                }

                // Otherwise, hide a few random words. I'll choose 3 here, but you can change it.
                scripture.HideRandomWords(3);
            }

            // Just a nice goodbye message
            Console.WriteLine("\nThank you for using my Scripture Memorizer!");
            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }
    }

    // The Reference class: holds the book, chapter, and verse info.
    class Reference
    {
        private string _book;
        private string _chapter;
        private string _verseStart;
        private string _verseEnd;

        // Single verse constructor
        public Reference(string book, string chapter, string verse)
        {
            _book = book;
            _chapter = chapter;
            _verseStart = verse;
            _verseEnd = "";  // Not used for single verses
        }

        // Multiple-verse constructor
        public Reference(string book, string chapter, string verseStart, string verseEnd)
        {
            _book = book;
            _chapter = chapter;
            _verseStart = verseStart;
            _verseEnd = verseEnd;
        }

        // Returns a nicely formatted reference (e.g., "John 3:16" or "Proverbs 3:5-6").
        public string GetDisplayText()
        {
            if (string.IsNullOrEmpty(_verseEnd))
            {
                // Single verse
                return $"{_book} {_chapter}:{_verseStart}";
            }
            else
            {
                // Verse range
                return $"{_book} {_chapter}:{_verseStart}-{_verseEnd}";
            }
        }
    }

    // The Word class: each word can be shown or hidden with underscores.
    class Word
    {
        private string _text;
        private bool _isHidden;

        public Word(string text)
        {
            _text = text;
            _isHidden = false;  // By default, the word is visible.
        }

        public bool IsHidden()
        {
            return _isHidden;
        }

        // Hide the word by setting this flag to true.
        public void Hide()
        {
            _isHidden = true;
        }

        // If hidden, return underscores. Otherwise, return the original text.
        public string GetDisplayText()
        {
            if (_isHidden)
            {
                return new string('_', _text.Length);
            }
            else
            {
                return _text;
            }
        }
    }

    // The Scripture class: holds the reference and the list of Word objects.
    class Scripture
    {
        private Reference _reference;
        private List<Word> _words;
        private Random _random;

        // We pass in a Reference object and the text for the scripture.
        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            _random = new Random();
            _words = new List<Word>();

            // Split the text by spaces, then create Word objects.
            string[] splitText = text.Split(' ');
            foreach (string wordStr in splitText)
            {
                // Trim extra whitespace or punctuation if needed.
                string trimmed = wordStr.Trim();
                if (!string.IsNullOrEmpty(trimmed))
                {
                    _words.Add(new Word(trimmed));
                }
            }
        }

        // Display the reference and the words in the console.
        public void DisplayScripture()
        {
            Console.WriteLine(_reference.GetDisplayText());
            Console.WriteLine();

            foreach (Word w in _words)
            {
                Console.Write(w.GetDisplayText() + " ");
            }
            Console.WriteLine();
        }

        // Hide a certain number of words at random.
        // For the basic requirement, we don't worry if some are already hidden.
        public void HideRandomWords(int numberToHide)
        {
            for (int i = 0; i < numberToHide; i++)
            {
                int index = _random.Next(_words.Count);
                _words[index].Hide();
            }
        }

        // Check if every word is hidden.
        public bool AllWordsHidden()
        {
            foreach (Word w in _words)
            {
                if (!w.IsHidden())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
