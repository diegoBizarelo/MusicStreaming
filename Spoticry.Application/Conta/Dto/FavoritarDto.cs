using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spoticry.Application.Conta.Dto
{
    public class FavoritarDto
    {
        public Guid IdMusica { get; set; }

        public Guid? IdPlayList { get; set;}
    }
}
