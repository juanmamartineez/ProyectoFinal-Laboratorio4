using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_Laboratorio4.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal_Laboratorio4.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ProveedorProducto>().HasKey(x => new { x.ProveedorId, x.ProductoId });
        }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Marca> Marcas { get; set; }

        public DbSet<Categoría> Categorías { get; set; }

        public DbSet<Proveedor> Proveedores { get; set; }

        public DbSet<ProveedorProducto> ProveedoresProductos { get; set; }
    }
}
