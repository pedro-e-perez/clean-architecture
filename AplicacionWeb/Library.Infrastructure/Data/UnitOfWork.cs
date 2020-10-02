using Library.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(LibraryDbContext context)
        {
            Context = context;
        }
        public DbContext Context { get; }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
