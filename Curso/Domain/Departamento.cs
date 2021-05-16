using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;

namespace DominandoEfCore.Domain
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        public Departamento()
        {

        }

        private Action<object,string> _lazyLoader { get; set; }
        private Departamento(Action<object, string> lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }
        private List<Funcionario> _funcionarios;
       
        public List<Funcionario> Funcionarios
        {
            get 
            {
                _lazyLoader.Invoke(this, nameof(Funcionarios));
                return _funcionarios;
            }
            set => _funcionarios = value;
        }

        //private ILazyLoader _lazyLoader { get; set; }
        //private Departamento(ILazyLoader lazyLoader)
        //{
        //    _lazyLoader = lazyLoader;
        //}
        //private List<Funcionario> _funcionarios;
        //public virtual List<Funcionario> Funcionarios { get; set; } // virtual e para  usar o load lazy entity
        //public  List<Funcionario> Funcionarios 
        //{
        //    get => _lazyLoader.Load(this, ref _funcionarios);
        //    set => _funcionarios = value;
        //}
    }
}