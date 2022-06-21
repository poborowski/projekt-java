using projekt.Entity;

namespace projekt
{
    public class LibrarySeeder
    {
        private readonly LibraryDbContext _context;
        public LibrarySeeder(LibraryDbContext libraryDbCo)
        {
            _context = libraryDbCo;
        }
        public void Seed()
        {
            if (_context.Database.CanConnect())
            {

                if (!_context.Books.Any())
                {
                    var Books = GetBooks();
                    _context.Books.AddRange(Books);
                    _context.SaveChanges();
                }
                if (!_context.Authors.Any())
                {
                    var author = Author();
                    _context.Authors.Add(author);
                    _context.SaveChanges();
                }
                if (!_context.Categories.Any())
                {
                    var category = Category();
                    _context.Categories.Add(category);
                    _context.SaveChanges();
                }

            }
        }
        private Category Category()
        {
            var category = new Category()
            {
                Name = "Przygoda",
                Description = "Przygoda",
            };
            return category;
        }
        private Author Author()
        {
            var author = new Author()
            {
                Name = "Bartek",
                LastName = "Bartek",
                DateOfBirth = DateTime.Now,

            };
            return author;
        }


        private IEnumerable<Book> GetBooks()
        {
            var books = new List<Book>()
          {

          new Book()
          {
              Name = "W pustyni i w puszczy",
              Description = "Przygoda w afryce",


              Category= new List<Category>()
              {
                  new Category()
                  {
                      Name="Akcja",
                      Description="Akcja"
                  }

              },

              Authors = new List<Author>()
              {
                new Author()
                {
                                    Name = "Bartek",
                LastName = "Bartek",
                DateOfBirth = DateTime.Now,

                 }

              },


          } };



            return books;
        }


    }
}