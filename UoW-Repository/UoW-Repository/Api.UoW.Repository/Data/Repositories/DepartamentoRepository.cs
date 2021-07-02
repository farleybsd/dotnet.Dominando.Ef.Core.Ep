using Api.UoW.Repository.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.UoW.Repository.Data.Repositories
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly ApplicationContext _context;

        public DepartamentoRepository(ApplicationContext context)
        {
            _context = context;
        }
        public void Add(Departamento departamento)
        {
            _context.Departamentos.Add(departamento);
        }

        public async Task<Departamento> GetByIdAsync(int id)
        {
            return await _context
                        .Departamentos
                        .Include(p => p.Colaboradores)
                        .FirstOrDefaultAsync(p => p.Id == id);
                        
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
