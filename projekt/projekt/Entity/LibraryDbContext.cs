using Microsoft.EntityFrameworkCore;

namespace projekt.Entity;

public class LibraryDbContext:DbContext
{
    private string Connection= @"Server=(localdb)\MSSQLLocalDB;Database=LibraryDB;Trusted_Connection=True;";
    public DbSet<Book>Books{ get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Book>().Property(P => P.Name).IsRequired();
        modelBuilder.Entity<User>().Property(P => P.Email).IsRequired();
        modelBuilder.Entity<Author>().Property(P => P.Name).IsRequired();
        modelBuilder.Entity<Category>().Property(P => P.Name).IsRequired();

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(Connection);
    }
    

}
