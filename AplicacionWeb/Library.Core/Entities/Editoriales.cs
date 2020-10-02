using Library.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Core.Entities
{
    [Table("editoriales")]
    public class Editoriales : BaseEntity
    {
       
        [Required]
        [MaxLength(45)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(45)]
        public string Sede { get; set; }

        public ICollection<Libros> Libros { get; set; }
    }
}
