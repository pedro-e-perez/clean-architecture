using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Config
{
    public class AutoresHasLibrosConfiguration : IEntityTypeConfiguration<Core.Entities.AutoresHasLibros>
    {
        public void Configure(EntityTypeBuilder<AutoresHasLibros> builder)
        {
            builder.HasQueryFilter(p => !p.LogicalErasure);
        }
    }
}
