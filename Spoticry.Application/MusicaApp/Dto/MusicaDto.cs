using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spoticry.Application.MusicaApp.Dto
{
    public class MusicaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Duracao { get; set; }
        public Guid BandaId { get; internal set; }
    }
}
