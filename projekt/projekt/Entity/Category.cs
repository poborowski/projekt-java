using System.Text.Json.Serialization;

namespace projekt.Entity
{
    public class Category
    {
        public int Id { get; set; }
   
        public string Description { get; set; }
       public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Book> Books { get; set; }
    }
}
