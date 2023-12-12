using Spoticry.Domain.Transacao.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spoticry.Domain.Transacao.Agreggates
{
    public class TransacaoAgg
    {
        public Guid Id { get; set; }
        public DateTime DataTransacao { get; set; }
        public decimal Valor { get; set; }
        public Merchant? Merchant { get; set; }
        public string? Descricao { get; set; }
    }
}
