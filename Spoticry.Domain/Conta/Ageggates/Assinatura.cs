using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spoticry.Domain.Conta.Ageggates
{
    public class Assinatura
    {
        public Guid Id { get; set; }
        public Plano Plano { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataAssinatura { get; set; }
    }
}
