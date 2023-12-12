using Models.Conta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamMusicTest.Conta
{
    public class ContaTestes
    {
        [Fact]
        public void Conta_RealizarCompra_CartaoInativoRetornaInativo()
        {
            //Arrange
            var cartao = new Cartao(limite: 2000, status: StatusCartao.Inativo);

            //Act
            var resultado = cartao.RealizarCompra(valor: 500, "Armlock");

            //Assert
            Assert.Equal(StatusCompra.CartaoInativo, resultado);
        }

        [Fact]
        public void Conta_RealizarCompra_CartaoSemLimiteDisponivel()
        {
            var cartao = new Cartao(limite: 300, status: StatusCartao.Ativo);

            var resultado = cartao.RealizarCompra(valor: 2000, "Armlock");

            Assert.Equal(StatusCompra.LimiteInsuficiente, resultado);
        }

        [Fact]
        public void Conta_RealizarCompra_TransacoesNoTempoLimiteAcimaPermitido()
        {
            var cartaoId = Guid.NewGuid();
            var date = DateTime.Now; //DateTime.ParseExact("2023-10-26 11:28:50", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            var compras = new List<Compra>()
            {
                new Compra(
                    cartaoId,
                    200d,
                    date.AddSeconds(-300),
                    "Lavandas Magics",
                    StatusCompra.Aprovada
                ),
                new Compra(
                    cartaoId,
                    200d,
                    date.AddSeconds(-40),
                    "Lavandas Mitycs",
                    StatusCompra.Aprovada
                ),
                new Compra(
                    cartaoId,
                    200d,
                    date.AddSeconds(-30),
                    "Lavandas Epics",
                    StatusCompra.Aprovada
                ),
            };
            var cartao = new Cartao(limite: 2000, status: StatusCartao.Ativo, compras);

            var resultado = cartao.RealizarCompra(200, "KeepTalking");

            Assert.Equal(StatusCompra.Recusada, resultado);
        }

        [Fact]
        public void Conta_RealizarCompra_TransacoesNoTempoLimiteAbaixoPermitido()
        {
            var cartaoId = Guid.NewGuid();
            var date = DateTime.Now; //DateTime.ParseExact("2023-10-26 11:28:50", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            var compras = new List<Compra>()
            {
                new Compra(
                    cartaoId,
                    200d,
                    date.AddSeconds(-300),
                    "Lavandas Magics",
                    StatusCompra.Aprovada
                ),
                new Compra(
                    cartaoId,
                    200d,
                    date.AddSeconds(-360),
                    "Lavandas Mitycs",
                    StatusCompra.Aprovada
                ),
                new Compra(
                    cartaoId,
                    200d,
                    date.AddSeconds(-30),
                    "Lavandas Epics",
                    StatusCompra.Aprovada
                ),
            };
            var cartao = new Cartao(limite: 2000, status: StatusCartao.Ativo, compras);

            var resultado = cartao.RealizarCompra(200, "KeepTalking");

            Assert.Equal(StatusCompra.Aprovada, resultado);
        }
    }
}
