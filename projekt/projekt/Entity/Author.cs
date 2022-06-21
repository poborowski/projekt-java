using System.Text.Json.Serialization;

namespace projekt.Entity
{
    public class Author
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
       [JsonIgnore]
        public ICollection<Book> Books{ get; set; }
    }
}
