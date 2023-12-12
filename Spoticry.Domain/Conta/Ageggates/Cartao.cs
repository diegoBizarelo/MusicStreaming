using Spoticry.Core.Validation;
using Spoticry.Domain.Conta.Exception;
using Spoticry.Domain.Transacao.Agreggates;
using Spoticry.Domain.Transacao.ValueObjects;

namespace Spoticry.Domain.Conta.Ageggates
{
    public class Cartao
    {

        private const int TRANSACAO_INTERVALO_TEMPO_LIMITE = -2;
        private const int TRANSACAO_MERCHANT_REPETIDA = 1;

        public Guid Id { get; set; }
        public List<TransacaoAgg> Transacoes { get; set; }
        public bool Ativo { get; set; }
        public decimal Limite { get; set; }
        public string Numero { get; set; }
        

        public Cartao()
        {
            Transacoes = new List<TransacaoAgg>();
        }

        public void CriarTransacao(string merchant, decimal valor, string descricao)
        {
            CartaoException validationErrors = new();

            CartaoEstaAivo(validationErrors);

            TransacaoAgg transacao = new()
            {
                Merchant = new Merchant() { Nome = merchant },
                Valor = valor,
                Descricao = descricao,
                DataTransacao = DateTime.Now
            };

            VerificaLimiteDisponivel(transacao, validationErrors);

            ValidarTransacao(transacao, validationErrors);

            validationErrors.ValidateAndThrow();

            transacao.Id = Guid.NewGuid();

            Limite -= transacao.Valor;

            Transacoes.Add(transacao);
        }

        private void CartaoEstaAivo(CartaoException validationErrors)
        {
            if (Ativo == false)
            {
                validationErrors.AdicionarError(new BusinessValidation()
                {
                    ErrorMessage = "Cartão não está ativo",
                    ErrorName = nameof(CartaoException)
                });

            }

        }

        private void VerificaLimiteDisponivel(TransacaoAgg transacao, CartaoException validationErrors)
        {
            if (transacao.Valor > Limite)
            {
                validationErrors.AdicionarError(new BusinessValidation()
                {
                    ErrorMessage = "Cartão não possui limite para esta transação",
                    ErrorName = nameof(CartaoException)
                });
            }
        }

        private void ValidarTransacao(TransacaoAgg transacao, CartaoException validationErrors)
        {
            var ultimasTransacoes = Transacoes.Where(x => x.DataTransacao >= DateTime.Now.AddMinutes(TRANSACAO_INTERVALO_TEMPO_LIMITE));

            if (ultimasTransacoes?.Count() >= 3)
            {
                validationErrors.AdicionarError(new BusinessValidation()
                {
                    ErrorMessage = "Cartão utilizado muitas vezes em um período curto",
                    ErrorName = nameof(CartaoException)
                });
            }

            if (ultimasTransacoes?.Where(x => x.Merchant.Nome.ToUpper() == transacao.Merchant.Nome.ToUpper()
                                         && x.Valor == transacao.Valor).Count() == TRANSACAO_MERCHANT_REPETIDA)
            {
                validationErrors.AdicionarError(new BusinessValidation()
                {
                    ErrorMessage = "Transação duplicada para o mesmo cartão e mesmo merchant",
                    ErrorName = nameof(CartaoException)
                });
            }
        }
    }
}
