using System;
using Workshop.Models.Dto.Requests;

namespace Workshop.Models
{
    public class BorrowInfo
    {
        
        public int Id { get; set; }
        public DateTime BorrowedTime { get; set; } = DateTime.Now;
        public DateTime ReturnTime { get; set; } = DateTime.Now.AddDays(10);
        public Person Person { get; set; }
        public Book Book { get; set; }
        
        public BorrowInfo(Person person, Book book)
        {
            Person = person;
            Book = book;
        }

        public BorrowInfo()
        {
        }
    }
}