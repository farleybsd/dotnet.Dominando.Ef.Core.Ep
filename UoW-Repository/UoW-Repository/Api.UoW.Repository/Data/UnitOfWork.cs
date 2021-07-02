using Api.UoW.Repository.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.UoW.Repository.Data
{
    /*
    É um dos padrões de design mais comuns nas empresas de 
    desenvolvimento de software.De acordo com Martin Fowler,
    o padrão de unidade de trabalho "mantém uma lista de 
    objetos afetados por uma transação e coordena a escrita
    de mudanças e trata possíveis problemas de concorrência".
    */
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        private IDepartamentoRepository _departamentoRepository;
        public IDepartamentoRepository DepartamentoRepository
        {
            get => _departamentoRepository ?? (_departamentoRepository = new DepartamentoRepository(_context));
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}


