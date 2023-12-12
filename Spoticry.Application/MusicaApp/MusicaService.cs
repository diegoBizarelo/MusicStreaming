using Spoticry.Application.MusicaApp.Dto;
using Spotiticry.Repository.BandaRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spoticry.Application.MusicaApp
{
    public class MusicaService
    {
        private readonly BandaRepository _bandaRepository = new BandaRepository();

        public List<BandaDto> BuscarBanda(string nome)
        {
            var bandas = _bandaRepository.RetornarBandaPorNome(nome);
            var bandasDto = new List<BandaDto>();

            foreach (var banda in bandas)
            {
                bandasDto.Add(
                    new BandaDto()
                    {
                        Id = banda.Id,
                        Nome = banda.Nome,
                    });
            }


            return bandasDto;
        }

        public List<MusicaDto> BuscarMusica(string nome)
        {
            var musicas = _bandaRepository.RetornarMusicaPorNome(nome);
            var musicasDto = new List<MusicaDto>();

            foreach (var musica in musicas)
            {
                musicasDto.Add(
                    new MusicaDto()
                    {
                        Id = musica.Id,
                        Nome = musica.Nome,
                        Duracao = musica.Duracao,
                    });
            } 
            

            return musicasDto;
        }

        public MusicaDto ObterMusica(Guid id)
        {
            var musica = _bandaRepository.RetornarMusica(id);
            var musicaDto = new MusicaDto()
            {
                Id = musica.Id,
                Nome = musica.Nome,
                Duracao = musica.Duracao,
                BandaId = musica.BandaId
            };
            
            return musicaDto;
        }
    }
}
