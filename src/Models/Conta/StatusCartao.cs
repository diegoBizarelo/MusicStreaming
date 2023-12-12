using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Conta
{
    public enum StatusCartao
    {
        [Description("Catão Ativo")]
        Ativo = 1,

        [Description("Cartão Inativo")]
        Inativo = 0,

        [Description("Bloqueado")]
        Bloqueado = 3
    }
}
