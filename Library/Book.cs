namespace Library
{
    public class Book : Model
    {
        public Book(string title, string author, string iSBN, int year)
        {
            Title = title;
            Author = author;
            ISBN = iSBN;
            Year = year;
        }

        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int Year { get; set; }
        public bool Active { get; set; }
    }
}
