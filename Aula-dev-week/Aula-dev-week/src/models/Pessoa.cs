using System.Collections.Generic;

namespace Aula_dev_week.src.models
{
    public class Pessoa
    {
        public Pessoa()
        {
            this.Nome = "template";
            this.Idade = 0;
            this.contratos = new List<Contrato>();
            this.Ativado = true;
        }
        public Pessoa(string nome, int idade, string cpf)
        {
            Nome = nome;
            Idade = idade;
            Cpf = cpf;
            this.contratos = new List<Contrato>();
            this.Ativado = true;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string? Cpf { get; set; }
        public bool Ativado { get; set; }

        public List<Contrato> contratos { get; set; } 
    }
}
