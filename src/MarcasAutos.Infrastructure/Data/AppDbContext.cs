using Microsoft.EntityFrameworkCore;
using MarcasAutos.Domain.Entities;
using MarcasAutos.Infrastructure.Configurations;

namespace MarcasAutos.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<MarcaAuto> MarcasAutos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new MarcaAutoConfiguration());
    }
}
