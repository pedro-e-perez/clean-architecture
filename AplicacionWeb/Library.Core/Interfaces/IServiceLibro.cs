using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Interfaces
{
    public interface IServiceLibro
    {
        public Libros GuardarLibro(Libros libro);
        public Libros ActualizarLibro(int id,Libros libro);
    }
}
