using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Conta
{
    public enum RegrasCartao : int
    {
        [Description("Quantidade máxima compras")]
        QuantidadeCompras = 3,

        [Description("Tempo máximo em segudos")]
        TempoLimite = 120,
    }
}
