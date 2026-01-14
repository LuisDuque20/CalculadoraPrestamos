using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Prestamo> Prestamos { get; set; }
    public DbSet<DetallePrestamo> DetallesPrestamo { get; set; }
}
