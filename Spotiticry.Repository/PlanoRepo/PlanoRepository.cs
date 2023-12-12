using Spoticry.Domain.Conta.Ageggates;
using System.Text.Json;

namespace Spotiticry.Repository.PlanoRepo
{
    public class PlanoRepository
    {
        private HttpClient HttpClient { get; set; }
        private static List<Plano> Planos = new List<Plano>();

        public PlanoRepository()
        {
            HttpClient = new HttpClient();

            #region AddPlanos
            Planos.Add(new Plano()
            {
                Id = Guid.Parse("7a6815b1-c8b0-4388-9665-1d5644dbb7f9"),
                Nome = "Super",
                Descricao = "Super plano",
                Valor = (decimal) 20.00
            });
            Planos.Add(new Plano()
            {
                Id = Guid.Parse("c541ab3d-fb05-4672-8690-dec522d098fb"),
                Nome = "Basico",
                Descricao = "Basicão",
                Valor = (decimal) 10.00
            });
            Planos.Add(new Plano()
            {
                Id = Guid.Parse("bd0cd6cf-89a9-4495-ab7f-e9d0a7c6d2d1"),
                Nome = "Intermediario",
                Descricao = "Nem pouco nem muito",
                Valor = (decimal) 15.00
            });
            #endregion AddPlanos
        }

        public async Task<Plano> ObterPlano(Guid id)
        {
            var result = await HttpClient.GetAsync($"{EnderecoHttp.Plano}/{id}");

            if (result.IsSuccessStatusCode == false)
                return null;

            var content = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Plano>(content);
        }

        public Plano RetornarPlano(Guid id)
        {
            return Planos.FirstOrDefault(p => p.Id == id);
        }
    }
}
