using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppAdo.Domain
{
    public class Pedido
    {

        public Pedido(DateTimeOffset dataPedido, Cliente cliente, 
            ICollection<PedidoItem> itens)
        {

            Id = Guid.NewGuid().ToString();
            Numero = new Random().Next().ToString();

            DataPedido = dataPedido;
            TotalPedido = 0;
            Cliente = cliente;
            ClienteId = cliente?.Id;
            Itens = itens;
        }

        public string Id { get; private set; }
        public string Numero { get; private set; }
        public DateTimeOffset DataPedido { get; private set; }
        public decimal TotalPedido { get; private set; }

        public virtual Cliente Cliente { get; private set; }
        public string ClienteId { get; private set; }

        public virtual ICollection<PedidoItem> Itens { get; private set; }


        public void CalcularTotalPedido()
        {
            var total = Itens.Sum(a => a.CalcularPrecoTotal());
            TotalPedido = total;
        }

    }
}
