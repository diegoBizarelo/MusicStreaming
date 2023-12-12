using Spoticry.Domain.Conta.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spoticry.Domain.Conta.Ageggates
{
    public class Usuario
    {
        private const string NOME_PADRAO_PLAYLIST = "Favoritas";

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public CPF CPF { get; set; }
        public List<Cartao> Cartoes { get; set; }
        public List<Playlist> Playlists { get; set; }
        public List<Assinatura> Assinaturas { get; set; }

        public Usuario()
        {
            Playlists = new List<Playlist>();
            Assinaturas = new List<Assinatura>();
            Cartoes = new List<Cartao>();
        }

        public void CriarUsuario(string nome, string cpf, Plano plano, Cartao cartao)
        {
            CPF = new CPF(cpf);
            Nome = nome;

            AssinarPlano(plano, cartao);
            AdicionarCartao(cartao);
            CriarPlayList();

        }

        private void AdicionarCartao(Cartao cartao)
        {
            Cartoes.Add(cartao);
        }

        public void Favoritar(Musica musica)
        {
            Playlists.FirstOrDefault(x => x.Nome.Equals(NOME_PADRAO_PLAYLIST, StringComparison.OrdinalIgnoreCase))?.Musicas.Add(musica);
        }

        public void Favoritar(Musica musica, string playlist)
        {
            Playlists.FirstOrDefault(x => x.Nome.Equals(playlist, StringComparison.OrdinalIgnoreCase))?.Musicas.Add(musica);
        }

        public void CriarPlayList(string nome = "Favoritas")
        {
            Playlists.Add(new Playlist()
            {
                Id = Guid.NewGuid(),
                Nome = nome,
                Publica = false,
                Usuario = this
            });
        }

        public void AssinarPlano(Plano plano, Cartao cartao)
        {

            cartao.CriarTransacao(plano.Nome, plano.Valor, plano.Descricao);

            if (Assinaturas.Count > 0 && Assinaturas.Any(x => x.Ativo))
            {
                var planoAtivo = Assinaturas.FirstOrDefault(x => x.Ativo);
                planoAtivo.Ativo = false;
            }

            Assinaturas.Add(new Assinatura()
            {
                Ativo = true,
                DataAssinatura = DateTime.Now,
                Plano = plano,
                Id = Guid.NewGuid()
            });
        }
    }
}
