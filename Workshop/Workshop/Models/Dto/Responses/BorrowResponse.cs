using System;

namespace Workshop.Models.Dto.Responses
{
    public class BorrowResponse
    {
        public DateTime BorrowedTime { get; set; }
        public DateTime ReturnTime { get; set; }
        
        public BorrowResponse(BorrowInfo borrow)
        {
            BorrowedTime = borrow.BorrowedTime;
            ReturnTime = borrow.ReturnTime;
        }
    }
}