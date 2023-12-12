using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spoticry.Application.Conta.Dto
{
    public class UsuarioDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "É necessário informar um nome")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "É necessário informar cpf")]
        public String CPF { get; set; }

        [Required(ErrorMessage = "É necessário assinar um plano")]
        public Guid PlanoId { get; set; }

        public CartaoDto Cartao { get; set; }

        public List<PlaylistDto>? Playlists { get; set; }
    }
}
