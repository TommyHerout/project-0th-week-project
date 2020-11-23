using System;
using System.Collections.Generic;
using Workshop.Models.Dto;

namespace Workshop.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsLibrarian { get; set; }
        public List<BorrowInfo> BorrowInfos { get; set; }
        public List<Book> Books { get; set; }

        public Person(PersonRequestDto personRequestDto)
        {
            Name = personRequestDto.Name;
            Password = personRequestDto.Password;
        }
    }
}