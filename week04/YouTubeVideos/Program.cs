using System;
using System.Collections.Generic;

class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; } // in seconds
    private List<Comment> comments;

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        comments = new List<Comment>();
    }

    public void AddComment(string name, string text)
    {
        comments.Add(new Comment(name, text));
    }

    public int GetCommentCount()
    {
        return comments.Count;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"\nVideo: {Title}\nBy: {Author}\nDuration: {Length} seconds\nTotal Comments: {GetCommentCount()}\n");
        foreach (var comment in comments)
        {
            Console.WriteLine($"- {comment.Name}: {comment.Text}");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("YouTube Video Tracker\n");

        List<Video> videos = new List<Video>();

        // Creating video objects
        Video vid1 = new Video("Intro to C#", "John Doe", 300);
        Video vid2 = new Video("OOP Basics", "Jane Smith", 600);
        Video vid3 = new Video("Advanced C#", "Mike Johnson", 900);

        // Adding comments to each video
        vid1.AddComment("Alice", "Great intro!");
        vid1.AddComment("Bob", "Very helpful.");
        vid1.AddComment("Charlie", "Loved it!");

        vid2.AddComment("David", "OOP is awesome!");
        vid2.AddComment("Eve", "Very clear explanation.");

        vid3.AddComment("Frank", "This was advanced but useful.");
        vid3.AddComment("Grace", "Good job!");

        // Adding videos to list
        videos.Add(vid1);
        videos.Add(vid2);
        videos.Add(vid3);

        // Displaying video details
        foreach (var video in videos)
        {
            video.DisplayInfo();
        }
    }
}