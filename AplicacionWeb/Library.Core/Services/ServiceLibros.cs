using Library.Core.Entities;
using Library.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Core.Services
{
    public class ServiceLibros : IServiceLibro
    {
        private readonly IRepository _repository;
        private readonly IRepositoryLibros _repositoryLibros;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceLibros(IRepository repository, IRepositoryLibros repositoryLibros, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _repositoryLibros = repositoryLibros;
            _unitOfWork = unitOfWork;
        }

        public Libros ActualizarLibro(int id, Libros libro)
        {
            using (var transaction = _unitOfWork.Context.Database.BeginTransaction())
            {
                try
                {
                    var listaAutoresHasLibros = libro.AutoresHasLibros;
                    libro.AutoresHasLibros = null;
                    //insertar el libro
                    _repositoryLibros.Update(libro);
                    //eliminar autores que no estan
                    var listAutoresNotExist = _repository.List<AutoresHasLibros>()
                        .Where(x => !listaAutoresHasLibros.Contains(x))
                        .ToList();
                    foreach (var autor in listAutoresNotExist) {
                        
                        _unitOfWork.Context.Set<AutoresHasLibros>().Remove(autor);
                        _unitOfWork.Context.SaveChanges();
                    }
                                                         

                    //insertar los autores
                    foreach (var autorLibro in listaAutoresHasLibros)
                    {
                        var existsAutor = _repository.List<AutoresHasLibros>().Any(
                            x => x.AutoresId.Equals(autorLibro.AutoresId) && 
                            x.LibrosISBN.Equals(autorLibro.LibrosISBN) && (x.LogicalErasure.Equals(false)||x.LogicalErasure.Equals(true)));
                        if (!existsAutor)
                        {
                            var autor = _repository.GetById<Autores>(autorLibro.AutoresId);
                            autorLibro.Autores = autor;
                            autorLibro.Libros = libro;
                            _repository.Add(autorLibro);
                        }
                        else {
                            autorLibro.LogicalErasure = false;
                            _repository.Update(autorLibro);
                        }


                    }

                    transaction.Commit();
                    return libro;
                }
                catch (Exception ex)
                {
                    // TODO: Handle failure
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
       

        /// <summary>
        /// metodo que permite el registro del libro
        /// </summary>
        /// <param name="libro"></param>
        /// <returns></returns>
        public Libros GuardarLibro(Libros libro)
        {
            using (var transaction = _unitOfWork.Context.Database.BeginTransaction())
            {
                try
                {
                    var listaAutoresHasLibros = libro.AutoresHasLibros;
                    libro.AutoresHasLibros = null;
                    //insertar el libro
                    _repositoryLibros.Add(libro);
                    //insertar los autores
                    foreach (var autorLibro in listaAutoresHasLibros) {
                        if (autorLibro.Id == 0) {
                            var autor = _repository.GetById<Autores>(autorLibro.AutoresId);
                            autorLibro.Autores = autor;
                            autorLibro.Libros = libro;
                            _repository.Add(autorLibro);
                        }
                       
                    
                    }
                   
                    transaction.Commit();
                    return libro;
                }
                catch (Exception ex)
                {
                    // TODO: Handle failure
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
