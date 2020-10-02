using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
namespace Library.Infrastructure.Config
{
    public class EditorialesConfiguration : IEntityTypeConfiguration<Core.Entities.Editoriales>
    {
        public void Configure(EntityTypeBuilder<Editoriales> builder)
        {
            builder.HasQueryFilter(p => !p.LogicalErasure);
        }
    }
}
