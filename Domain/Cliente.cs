using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppAdo.Domain
{
    public class Cliente 
    {

        private readonly IDictionary<string, string> _validacoes;




        public Cliente(string codigo, string nome, DateTime dataCadastro)
        {
            _validacoes = new Dictionary<string, string>();


            Id = Guid.NewGuid().ToString();


            if (string.IsNullOrWhiteSpace(codigo))
                Codigo = $"X-{new Random().Next(2019, 99999)}";
            else
                Codigo = codigo;

            if (string.IsNullOrWhiteSpace(nome))
                _validacoes.Add("Cliente.Nome", "Nome deve ser informado.");
            else if (nome?.Trim().Length > 30)
                _validacoes.Add("Cliente.Nome", "Nome não deve tamanho maior que 30");
            else
                Nome = nome?.ToUpper().Trim();

            if (dataCadastro < DateTime.Today.AddYears(-10) ||
                dataCadastro > DateTime.Today.AddMonths(2))
                _validacoes.Add("Cliente.DataCadastro", $"A data deve estar entre {DateTime.Today.AddYears(-10)} e {DateTime.Today.AddMonths(2)}");
            else
                DataCadastro = dataCadastro;
        }

        public string Id { get; private set; }
        public string Codigo { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public virtual ICollection<Pedido> Pedidos { get; private set; }
        public IDictionary<string, string> Validacoes { get; private set; }


        public bool EhValido()
        {
            return Validacoes.Count == 0;
        }

    }
}
