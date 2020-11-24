using System.Collections.Generic;
using Newtonsoft.Json;

namespace Workshop.Models
{
    public class Category
    {
        [JsonIgnore]
        public int Id { get; set; }
        
        public string Name { get; set; }
        [JsonIgnore]
        public List<Book> Books { get; set; }
    }
}