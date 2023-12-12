using Spoticry.Application.Conta.Dto;
using Spoticry.Core.Exception;
using Spoticry.Core.Validation;
using Spoticry.Domain.Conta.Ageggates;
using Spotiticry.Repository.BandaRepo;
using Spotiticry.Repository.ContaRepo;
using Spotiticry.Repository.PlanoRepo;
using System.Numerics;

namespace Spoticry.Application.Conta
{
    public class UsuarioService
    {
        private UsuarioRepository usuarioRepository = new UsuarioRepository();
        private PlanoRepository planoRepository = new PlanoRepository();
        private BandaRepository bandaRepository = new BandaRepository();

        public async Task<UsuarioDto> CriarConta(UsuarioDto conta)
        {
            Plano plano = await planoRepository.ObterPlano(conta.PlanoId);

            if (plano == null)
            {
                new BusinessException(new BusinessValidation()
                {
                    ErrorMessage = "Plano não encontrado",
                    ErrorName = nameof(CriarConta)
                }).ValidateAndThrow();
            }

            Cartao cartao = new Cartao() {
                Ativo = conta.Cartao.Ativo,
                Numero = conta.Cartao.Numero,
                Limite = conta.Cartao.Limite,
            };

            Usuario usuario = new Usuario();
            usuario.CriarUsuario(conta.Nome, conta.CPF, plano, cartao);

            usuarioRepository.SalvarUsuario(usuario);
            conta.Id = usuario.Id;

            return conta;
        }

        public async Task<bool> CriarPlayList(Guid id, string playlistNome) 
        {
            var usuario = usuarioRepository.ObterUsuario(id);
            
            if (usuario == null)
                return false;

            usuario.CriarPlayList(playlistNome);
            usuarioRepository.Update(usuario);
            return true;
        }

        private void PreencherPlayList(Usuario usuario)
        {

        }

        public UsuarioDto? ObterUsuario(Guid id)
        {
            var usuario = usuarioRepository.ObterUsuario(id);

            if (usuario == null)
                return null;

            UsuarioDto result = new UsuarioDto()
            {
                Id = usuario.Id,
                Cartao = new CartaoDto()
                {
                    Ativo = usuario.Cartoes.FirstOrDefault().Ativo,
                    Limite = usuario.Cartoes.FirstOrDefault().Limite,
                    Numero = usuario.Cartoes.FirstOrDefault().Numero,
                },
                CPF = usuario.CPF.NumeroFormatado(),
                Nome = usuario.Nome,
                Playlists = new List<PlaylistDto>(),
                PlanoId = usuario.Assinaturas.FirstOrDefault().Id
            };

            foreach (var item in usuario.Playlists)
            {
                var playList = new PlaylistDto()
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Publica = item.Publica,
                    Musicas = new List<MusicaDto>()
                };

                foreach (var musicas in item.Musicas)
                {
                    playList.Musicas.Add(new MusicaDto()
                    {
                        Duracao = musicas.Duracao,
                        Id = musicas.Id,
                        Nome = musicas.Nome
                    });
                }

                result.Playlists.Add(playList);
            }

            return result;
        }

        public async Task FavoritarMusica(Guid id, Guid idMusica, Guid? idPlayList)
        {
            var usuario = usuarioRepository.ObterUsuario(id) ?? throw new BusinessException(new BusinessValidation()
                {
                    ErrorMessage = "Usuário não encontrado",
                    ErrorName = nameof(FavoritarMusica)
                });

            var musica = await bandaRepository.ObterMusica(idMusica) ?? throw new BusinessException(new BusinessValidation()
                {
                    ErrorMessage = "Música não encontrada",
                    ErrorName = nameof(FavoritarMusica)
                });

            var playlist = usuario.Playlists.FirstOrDefault(p => p.Id == idPlayList);

            if (idPlayList == null)
                usuario.Favoritar(musica);
            else
                usuario.Favoritar(musica, playlist.Nome);

            usuarioRepository.Update(usuario);
        }

        public async Task<List<Musica>> buscarMusica(string nome)
        {
            var result = await bandaRepository.ObterMusica(nome);

            if (result == null)
            {
                return new List<Musica>();
            }

            return result;
        }

        public async Task<List<Banda>> buscarBanda(string nome)
        {
            var result = await bandaRepository.ObterBanda(nome);

            if (result == null)
            {
                return new List<Banda>();
            }

            return result;
        }
    }
}
