using System;
using System.Collections.Generic;
using Workshop.Extensions;
using Workshop.Models.Dto;
using Workshop.Models.Dto.Requests;

namespace Workshop.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsLibrarian { get; set; }
        
        public List<BorrowInfo> BorrowInfos { get; set; }
        public List<Book> Books { get; set; }

        public Person(RegisterRequestDto registerRequestDto)
        {
            Name = registerRequestDto.Name;
            Username = registerRequestDto.Username;
            Password = registerRequestDto.Password.HashString();
        }

        public Person()
        {
        }
    }
}