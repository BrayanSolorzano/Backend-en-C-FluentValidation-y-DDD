using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MarcasAutos.Domain.Entities;

namespace MarcasAutos.Infrastructure.Configurations;

public class MarcaAutoConfiguration : IEntityTypeConfiguration<MarcaAuto>
{
    public void Configure(EntityTypeBuilder<MarcaAuto> builder)
    {
        builder.ToTable("MarcasAutos");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.PaisOrigen)
            .IsRequired()
            .HasMaxLength(100);

        // Data Seed
        builder.HasData(
            new { Id = 1, Nombre = "Toyota", PaisOrigen = "Japón" },
            new { Id = 2, Nombre = "Ford", PaisOrigen = "Estados Unidos" },
            new { Id = 3, Nombre = "Volkswagen", PaisOrigen = "Alemania" }
        );
    }
}
