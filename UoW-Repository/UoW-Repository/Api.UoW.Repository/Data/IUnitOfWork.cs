using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.UoW.Repository.Data
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
