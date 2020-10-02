using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Config
{
    public class AutoresConfiguration : IEntityTypeConfiguration<Core.Entities.Autores>
    {
        public void Configure(EntityTypeBuilder<Autores> builder)
        {
            builder.HasQueryFilter(p => !p.LogicalErasure);
        }
    }
}
