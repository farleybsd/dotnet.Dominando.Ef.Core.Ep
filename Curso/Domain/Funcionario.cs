namespace DominandoEfCore.Domain
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }

        public int DepartamentoId { get; set; }
        public virtual Departamento Departamento { get; set; } // virtual e para  usar o load lazy entity
    }
}