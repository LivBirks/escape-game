using System;

namespace EscapeGame
{
    public class Book
    {
        public string Title { get; private set; }
        public bool IsKey { get; private set; }

        public Book(string title, bool isKey)
        {
            Title = title;
            IsKey = isKey;
        }
    }
}