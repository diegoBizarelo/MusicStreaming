using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Spoticry.Application.Conta.Dto
{
    public class CartaoDto
    {
        [Required]
        [Description("É necessário informar número do cartão")]
        public String Numero { get; set; }

        [Required]
        [Description("É necessário informar limite do cartão")]
        public Decimal Limite { get; set; }

        [Required]
        [Description("É necessário informar status do cartão")]
        public Boolean Ativo { get; set; }
    }
}
