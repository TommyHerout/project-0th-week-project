namespace Workshop.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public Category Category { get; set; }
        public Person Person { get; set; }
    }
}