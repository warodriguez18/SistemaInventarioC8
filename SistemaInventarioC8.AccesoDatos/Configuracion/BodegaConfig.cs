using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaInventarioC8.Modelos;

namespace SistemaInventarioC8.AccesoDatos.Configuracion
{
    public class BodegaConfig : IEntityTypeConfiguration<Bodega>
    {
        public void Configure(EntityTypeBuilder<Bodega> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(b => b.Descripcion)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(b => b.Direccion)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(b => b.FechaCreacion)
                .HasDefaultValueSql("GETDATE()");
            builder.Property(b => b.FechaModificacion)
                .IsRequired(false);
            builder.Property(b => b.Activa)
                .IsRequired();
        }
    }
}
