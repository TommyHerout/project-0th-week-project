using System.Collections.Generic;
using Newtonsoft.Json;

namespace Workshop.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}