using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.SharedKernel
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool LogicalErasure { get; set; }
        public bool Active { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public DateTime? EraseDate { get; set; }

    }
}
