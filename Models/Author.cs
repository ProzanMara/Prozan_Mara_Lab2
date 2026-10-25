namespace Prozan_Mara_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        public ICollection<Book>? Books { get; set; }
    }
}
