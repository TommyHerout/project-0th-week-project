using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Workshop.Models.Dto;
using Workshop.Models.Dto.Responses;

namespace Workshop.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public Category Category { get; set; }
        public Person Person { get; set; }
        
        public Book(string name, bool isAvailable, Category category, Person person)
        {
            Name = name;
            IsAvailable = isAvailable;
            Person = person;
        }

        public Book(Person person)
        {
            Person = person;
        }

        public Book()
        {
            
        }
    }
}