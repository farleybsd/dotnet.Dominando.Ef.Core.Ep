﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.EF.CORE.Domain
{
    public class Documento
    {
        private string _cpf;
        public int Id { get; set; }

        public void SetCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                throw new Exception("CPF INVALIDO");
            }
            _cpf = cpf;
        }

        public string GetCPF()=> _cpf;
    }
}
