using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Interfaces
{
    public interface IRepositoryLibros
    {
        Libros GetById(int id);
        List<Libros> List() ;
        Libros Add(Libros entity) ;
        void Update(Libros entity);
        void Delete(Libros entity);
    }
}
