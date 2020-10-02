using Library.Core.Entities;
using Library.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Infrastructure.Data
{
    public class RepositoryLibros : IRepositoryLibros
    {
        private readonly LibraryDbContext _dbContext;
        /// <summary>
        /// se inyecta el dbcontext
        /// </summary>
        /// <param name="dbContext"></param>
        public RepositoryLibros(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Libros Add(Libros entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.Active = true;
            entity.LogicalErasure = false;
            _dbContext.Set<Libros>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public void Delete(Libros entity)
        {
            entity.EraseDate = DateTime.Now;
            entity.Active = false;
            entity.LogicalErasure = true;

            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public Libros GetById(int ISBN)
        {
            return _dbContext.Set<Libros>()
                .Include(x=>x.AutoresHasLibros)
                .Include(x => x.Editoriales)
                .SingleOrDefault(e => e.ISBN == ISBN);
        }

        public List<Libros> List()
        {
            return _dbContext.Set<Libros>()
                .Include(x=>x.Editoriales).ToList();
        }

        public void Update(Libros entity)
        {
            entity.ModificationDate = DateTime.Now;

            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
