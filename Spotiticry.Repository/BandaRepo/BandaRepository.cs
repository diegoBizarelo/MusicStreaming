using Spoticry.Domain.Conta.Ageggates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Spotiticry.Repository.BandaRepo
{
    public class BandaRepository
    {
        private HttpClient HttpClient { get; set; }
        private static List<Musica> _musicas = new List<Musica>();
        private static List<Banda> _bandas = new List<Banda>();

        public BandaRepository()
        {
            HttpClient = new HttpClient();

            #region AddBandas
            if (!_bandas.Any())
            {
                _bandas.Add(new Banda()
                {
                    Id = Guid.Parse("83f8b3ec-ae8a-4122-8bac-ea643bdc69cb"),
                    Nome = "Pagode é o novo metal",
                });
                _bandas.Add(new Banda()
                {
                    Id = Guid.Parse("6717f0a7-b77e-4027-87d0-b1662648f05f"),
                    Nome = "Queen",
                });
                _bandas.Add(new Banda()
                {
                    Id = Guid.Parse("b8c1b5df-8dc1-4ece-86f0-9e091adec90b"),
                    Nome = "Eagles",
                });
                _bandas.Add(new Banda()
                {
                    Id = Guid.Parse("3222c32d-c7d0-4d11-8379-c1b2fa4ec1c9"),
                    Nome = "Beatles",
                });
                _bandas.Add(new Banda()
                {
                    Id = Guid.Parse("5b427083-767c-4656-a5c5-c0ecac3c9107"),
                    Nome = "Guns N' Roses",
                });
            }
            #endregion AddBandas

            #region AddMusicas
            if (!_musicas.Any())
            {
                _musicas.Add(new Musica
                {
                    Id = Guid.Parse("f7218b7d-f4b9-46bc-ada4-fd087b5fc876"),
                    Nome = "Welcome to the microsserices",
                    Duracao = 350,
                    BandaId = Guid.Parse("83f8b3ec-ae8a-4122-8bac-ea643bdc69cb")
                });
                _musicas.Add(new Musica
                {
                    Id = Guid.Parse("b32f8558-3138-4a84-98ff-e8a1143b22f6"),
                    Nome = "Bohemian Rhapsody",
                    Duracao = 354,
                    BandaId = Guid.Parse("6717f0a7-b77e-4027-87d0-b1662648f05f")
                });
                _musicas.Add(new Musica
                {
                    Id = Guid.Parse("873df2cc-4162-4667-990b-f87ffd1e2f3f"),
                    Nome = "Hotel California",
                    Duracao = 391,
                    BandaId = Guid.Parse("b8c1b5df-8dc1-4ece-86f0-9e091adec90b")
                });
                _musicas.Add(new Musica
                {
                    Id = Guid.Parse("505a33b9-95ad-4816-a8a8-4dd5a732db59"),
                    Nome = "Hey Jude",
                    Duracao = 461,
                    BandaId = Guid.Parse("3222c32d-c7d0-4d11-8379-c1b2fa4ec1c9")
                });
                _musicas.Add(new Musica
                {
                    Id = Guid.Parse("131e8637-0e5d-48c5-9f98-ac9ebd75dee0"),
                    Nome = "November Rain",
                    Duracao = 536,
                    BandaId = Guid.Parse("5b427083-767c-4656-a5c5-c0ecac3c9107")
                });
            }
            #endregion AddMusicas
        }

        public async Task<Musica?> ObterMusica(Guid id)
        {
            var result = await HttpClient.GetAsync($"{EnderecoHttp.Musica}/{id}");

            if (result.IsSuccessStatusCode == false)
                return null;

            var content = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Musica>(content);
        }

        public async Task<List<Musica>> ObterMusica(string nome)
        {
            var result = await HttpClient.GetAsync($"{EnderecoHttp.Musica}/buscarMusica/{nome}");

            if (result.IsSuccessStatusCode == false)
                return null;

            var content = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Musica>>(content);
        }

        public Musica RetornarMusica(Guid id)
        {
            return _musicas.FirstOrDefault(m => m.Id == id);
        }

        public List<Banda> BuscarBanda(string nome)
        {
            var result = _bandas.Where(b => b.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();
            return result;
        }

        public List<Musica> RetornarMusicaPorNome(string nome)
        {
            var result = _musicas.Where(m => m.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();
            return result;
        }

        public List<Banda> RetornarBandaPorNome(string nome)
        {
            var result = _bandas.Where(b => b.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();
            return result;
        }

        public async Task<List<Banda>> ObterBanda(string nome)
        {
            var result = await HttpClient.GetAsync($"{EnderecoHttp.Musica}/buscarBanda/{nome}");

            if (result.IsSuccessStatusCode == false)
                return null;

            var content = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Banda>>(content);
        }
    }
}
