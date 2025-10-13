using ApiLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary;

public class LibraryDbContext : DbContext //création de la classe "LibraryDbContext", l'héritage de DbContext, une classe d'Ef
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Login> Logins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)//Configuration de la connexion à la BDD 
    {
        string connectionString =
            "Server=romaric-thibault.fr;" +
            "Database=timothe_EfCoreLibrary;" +
            "User Id=timothe;" +
            "Password=Onto9-Cage-Afflicted;" +
            "TrustServerCertificate=true;";
        
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Vide pour le moment
    }
}