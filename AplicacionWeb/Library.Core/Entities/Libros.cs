using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Core.Entities
{
    [Table("libros")]
    public class Libros
    {
        [Key]
        public int ISBN { get; set; }
        [Required]
        public Editoriales Editoriales { get; set; }
        [Required]
        [MaxLength(45)]
        public string Titulo { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Sinopsis { get; set; }
        [Required]
        [MaxLength(45)]
        [Column("n_paginas")]
        public string NPaginas { get; set; }
        public ICollection<AutoresHasLibros> AutoresHasLibros { get; set; }
        public bool LogicalErasure { get; set; }
        public bool Active { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public DateTime? EraseDate { get; set; }
    }
}
