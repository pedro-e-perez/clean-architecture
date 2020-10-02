using Library.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Core.Entities
{
    [Table("autores_has_libros")]
    public class AutoresHasLibros : BaseEntity  
    {
        public int AutoresId { get; set; }
        public Autores Autores { get; set; }
        public int LibrosISBN { get; set; }
        public Libros Libros { get; set; }
    }
}
