using Library.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Core.Entities
{
    [Table("autores")]
    public class Autores : BaseEntity
    {
        
        [Required]
        [MaxLength(45)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(45)]
        public string Apellidos { get; set; }

        public ICollection<AutoresHasLibros> AutoresHasLibros { get; set; }
    }
}
