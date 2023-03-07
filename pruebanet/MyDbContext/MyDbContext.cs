
using Microsoft.EntityFrameworkCore;
using pruebanet.Entities;


public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
       : base(options)
    {
    }
    public DbSet<TipoCambio> TipoCambio { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "MyDatabase");
    }
}

