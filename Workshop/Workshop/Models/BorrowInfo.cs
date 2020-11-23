using System;

namespace Workshop.Models
{
    public class BorrowInfo
    {
        public int Id { get; set; }
        public DateTime BorrowedTime { get; set; }
        public DateTime ReturnedTime { get; set; }
        public Person Person { get; set; }
    }
}