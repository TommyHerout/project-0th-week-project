using System;

namespace Workshop.Models.Dto.Responses
{
    public class BorrowResponse
    {

        public DateTime BorrowedTime { get; set; } = DateTime.Now;
        public DateTime ReturnTime { get; set; } = DateTime.Now.AddDays(10);
        
        public BorrowResponse(BorrowInfo borrow)
        {
            BorrowedTime = borrow.BorrowedTime;
            ReturnTime = borrow.ReturnTime;
        }
    }
}