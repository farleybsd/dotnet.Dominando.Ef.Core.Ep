using Api.UoW.Repository.Data.Repositories.Base;
using Api.UoW.Repository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.UoW.Repository.Data.Repositories
{
    public interface IDepartamentoRepository : IGenericRepository<Departamento>
    {
        //Task<Departamento> GetByIdAsync(int id);
        //void Add(Departamento departamento);
        //bool Save();
    }
}
