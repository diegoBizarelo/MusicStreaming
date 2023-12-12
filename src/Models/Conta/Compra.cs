namespace Models.Conta
{
    public class Compra
    {
        public Guid Id { get; private set; }
        public Guid CartaoId { get; private set; }
        public double Valor { get; private set; }
        public DateTime? DataCompra { get; private set; }
        public string? Comerciante { get; private set; }
        public StatusCompra Status { get; private set; }

        public Compra(Guid cartaoId, double valor, DateTime? dataCompra, string comerciante, StatusCompra status = StatusCompra.Processando)
        {
            CartaoId = cartaoId;
            Valor = valor;
            DataCompra = dataCompra;
            Comerciante = comerciante;
            Status = status;
        }

        public void AtualizarStatus(StatusCompra status)
        {
            Status = status;
        }
    }
}