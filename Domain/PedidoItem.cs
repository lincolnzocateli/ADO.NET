namespace ConsoleAppAdo.Domain
{
    public class PedidoItem
    {


        public string Id { get; set; }
        public string Mercadoria { get; set; }
        public double Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal PrecoTotal { get; private set; }

        public virtual Pedido Pedido { get; set; }
        public string PedidoId { get; set; }



        public decimal CalcularPrecoTotal()
        {
            PrecoTotal = (decimal)Quantidade * PrecoUnitario;
            return PrecoTotal;
        }
    }
}
