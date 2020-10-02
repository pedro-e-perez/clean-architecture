using Library.Core.Entities;
using Library.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Models
{
    public class LibroViewModel
    {
        private IRepository _repository;

        [Key]
        public int ISBN { get; set; }
        [Required]
        public int EditorialesId { get; set; }
        [Required]
        [MaxLength(45)]
        public string Titulo { get; set; }
        [Required]
        public string Sinopsis { get; set; }
        [Required]
        [MaxLength(45)]
        public string NPaginas { get; set; }

        [Required]
        public bool Active { get; set; }
        [Required]
        public string [] AutoresIds { get; set; }

        public static LibroViewModel ToMap(Libros libros) {
            var autoresIds = libros.AutoresHasLibros.Select(x=>x.AutoresId);
            var libroViewModel = new LibroViewModel
            {
                ISBN=libros.ISBN,
                Active=libros.Active,
                EditorialesId=libros.Editoriales.Id,
                NPaginas=libros.NPaginas,
                Sinopsis=libros.Sinopsis,
                Titulo=libros.Titulo,


            };
            return libroViewModel;
        }
        public Libros MapData(IRepository context)
        {
            _repository = context;

            var editorial = _repository.GetById<Editoriales>(this.EditorialesId);
            var autoreshaslibros = CreateAutoresHasLibros();
            var libros = new Libros
            {

                ISBN = ISBN,
                Titulo = Titulo,
                Sinopsis = Sinopsis,
                NPaginas = NPaginas,
                Active = Active,
                Editoriales = editorial,
                AutoresHasLibros= autoreshaslibros

            };
            return libros;

        }
        private List<AutoresHasLibros> CreateAutoresHasLibros()
        {
            var listAutores = new List<AutoresHasLibros>();
            foreach (var autor in AutoresIds)
            {
                if (!string.IsNullOrEmpty(autor))
                {
                    var autorId = int.Parse(autor);
                    var autoreshaslibros = _repository.List<AutoresHasLibros>().Where(x => x.AutoresId.Equals(autorId) && x.LibrosISBN.Equals(ISBN))
                        .FirstOrDefault();
                    if (autoreshaslibros != null)
                    {
                        listAutores.Add(autoreshaslibros);
                    }
                    else
                    {
                        listAutores.Add(new AutoresHasLibros
                        {
                            LibrosISBN = ISBN,
                            AutoresId = autorId
                        });
                    }
                }



            }
            return listAutores;

        }

    }
}
