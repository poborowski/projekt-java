namespace projekt.Entity
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public  List<Category> Category{ get; set; }
      
     
        public  ICollection<Author> Authors { get; set; }
       
    }
}
