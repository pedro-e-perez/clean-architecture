using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }


    }
}
