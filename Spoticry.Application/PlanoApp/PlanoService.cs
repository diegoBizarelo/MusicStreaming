using Spoticry.Application.PlanoApp.Dto;
using Spoticry.Domain.Conta.Ageggates;
using Spotiticry.Repository.PlanoRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spoticry.Application.PlanoApp
{
    public class PlanoService
    {
        private PlanoRepository planoRepository = new PlanoRepository();
        public PlanoService() { }
        public PlanoDto ObterPlano(Guid id)
        {
            var plano = planoRepository.RetornarPlano(id);

            if (plano == null)
                return null;

            var planoDto = new PlanoDto()
            {
                Id = plano.Id,
                Nome = plano.Nome,
                Valor = plano.Valor,
                Descricao = plano.Descricao,
            };

            return planoDto;
        }
    }
}
