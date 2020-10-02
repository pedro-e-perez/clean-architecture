using Library.Core.Interfaces;
using Library.Core.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Infrastructure.Data
{
    public class EfRepository : IRepository
    {
        private readonly LibraryDbContext _dbContext;
        /// <summary>
        /// se inyecta el dbcontext
        /// </summary>
        /// <param name="dbContext"></param>
        public EfRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// obtener el objeto por id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById<T>(int id) where T : BaseEntity
        {
            return _dbContext.Set<T>().SingleOrDefault(e => e.Id == id);
        }
        /// <summary>
        /// traer unalista de la entidad
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> List<T>() where T : BaseEntity
        {
            return _dbContext.Set<T>().ToList();
        }
        /// <summary>
        /// agregar un registro
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Add<T>(T entity) where T : BaseEntity
        {

            entity.CreateDate = DateTime.Now;
            entity.Active = true;
            entity.LogicalErasure = false;
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }
        /// <summary>
        /// eliminar registro
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void Delete<T>(T entity) where T : BaseEntity
        {


            entity.EraseDate = DateTime.Now;
            entity.Active = false;
            entity.LogicalErasure = true;

            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();


        }
        /// <summary>
        /// actualizar registro
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void Update<T>(T entity) where T : BaseEntity
        {
            entity.ModificationDate = DateTime.Now;

            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
