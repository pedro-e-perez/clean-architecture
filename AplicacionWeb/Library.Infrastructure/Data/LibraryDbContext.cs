using System;
using System.Collections.Generic;
using System.Text;
using Library.Core.Entities;
using Library.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;
namespace Library.Infrastructure.Data
{
    public class LibraryDbContext:DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //cargar ls configruaciones
            modelBuilder.ApplyConfiguration(new EditorialesConfiguration());
            modelBuilder.ApplyConfiguration(new LibroConfiguration());
            modelBuilder.ApplyConfiguration(new AutoresConfiguration());
            modelBuilder.ApplyConfiguration(new AutoresHasLibrosConfiguration());
            //crear la relacion many ot many autores has libros
            modelBuilder.Entity<AutoresHasLibros>().HasKey(sc => new { sc.AutoresId, sc.LibrosISBN });
            //el isbn no debe ser autoincement
            modelBuilder.Entity<Libros>()
            .Property(c => c.ISBN)
            .ValueGeneratedNever();
        }
        #region Entidades
        //tabla autores
        public DbSet<Autores> Autores { get; set; }
        //tabla editoriales
        public DbSet<Editoriales> Editoriales { get; set; }
        //tabla libros
        public DbSet<Libros> Libros { get; set; }
        //tabla has libros
        public DbSet<AutoresHasLibros> AutoresHasLibros { get; set; }

        #endregion

    }
}
