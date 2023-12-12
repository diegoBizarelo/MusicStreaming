using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Models.Conta
{
    public class Cartao : Entity
    {
        public string Numero {  get; private set; }
        public Guid CondaId { get; private set; }
        public double? Limite { get; private set; }
        public DateTime? DataCompra { get; private set; }
        public readonly IEnumerable<Compra>? Compras;
        public StatusCartao Status { get; private set; }

        public Cartao(double limite, StatusCartao status, IEnumerable<Compra> compras)
        {
            Compras = compras;
            Status = status;
            Limite = limite;
        }

        public Cartao(StatusCartao status, double limite)
        {
            Compras = new List<Compra>();
            Status = status;
            Limite = limite;
        }

        public double? LimiteDisponivel()
        {
            var compras = Compras?.Where(c => c.Status == StatusCompra.Aprovada).Sum(c => c.Valor);
            var limiteDisponivel = Limite - compras;
            return limiteDisponivel;
        }

        private StatusCartao GetStatusCartao() => Status;

        private bool PossuiLimiteDisponivel(double valor) => LimiteDisponivel() > valor;

        /*private bool VerificaTransacoes(Compra compra)
        {
            var diffSegundos = 0; //((compra.DataCompra - ultimasCompras[0].DataCompra) + (ultimasCompras[0].DataCompra - ultimasCompras[1].DataCompra))?.TotalSeconds;
            var ultimasCompras = Compras.OrderByDescending(c => c.DataCompra).Take((int)RegrasCartao.QuantidadeCompras).ToList();
            ultimasCompras.Add(compra);
            for (int i = ultimasCompras.Count() -1; i >  , i--)
            return diffSegundos < (int)RegrasCartao.TempoLimite;
        }*/

        private bool VerficaQuantidadeTransacoesValidasNoTempoLimite(Compra compra)
        {
            var ultimasCompras = Compras.OrderByDescending(c => c.DataCompra)
                                    .Where(c => ((DateTime.Now - c.DataCompra)?.TotalSeconds < (int)RegrasCartao.TempoLimite))
                                    .Take((int)RegrasCartao.QuantidadeCompras).ToList();
            
            ultimasCompras.Add(compra);
            
            return ultimasCompras.Count() < (int)RegrasCartao.QuantidadeCompras;
        }

        public StatusCompra RealizarCompra(double valor, string comerciante) {
            var compra = new Compra(Id, valor, DateTime.Now, "ArmLock");

            if (GetStatusCartao() != StatusCartao.Ativo)
            {
                compra.AtualizarStatus(StatusCompra.CartaoInativo);
                return compra.Status;
            }
            if (!PossuiLimiteDisponivel(valor))
            {
                compra.AtualizarStatus(StatusCompra.LimiteInsuficiente);
                return compra.Status;
            }

            if (!VerficaQuantidadeTransacoesValidasNoTempoLimite(compra))
            {
                compra.AtualizarStatus(StatusCompra.Recusada);
                return compra.Status;
            }

            return StatusCompra.Aprovada;

        }

    }
}
