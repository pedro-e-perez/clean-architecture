using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Config
{
    public class LibroConfiguration : IEntityTypeConfiguration<Core.Entities.Libros>
    {
        public void Configure(EntityTypeBuilder<Libros> builder)
        {
            builder.HasQueryFilter(p => !p.LogicalErasure);
        }
    }
}
