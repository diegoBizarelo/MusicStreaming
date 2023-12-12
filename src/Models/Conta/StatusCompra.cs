using System.ComponentModel;

namespace Models.Conta
{
    public enum StatusCompra
    {
        [Description("Recusada")]
        Recusada = 0,

        [Description("Aprovada")]
        Aprovada = 1,

        [Description("Carta Inativo")]
        CartaoInativo = 0,

        [Description("Limite Insuficiente")]
        LimiteInsuficiente = 0,

        [Description("Em Análise")]
        Processando = 2,

        /*[Description("Tentativa de Fraude")]
        TentativaFraude = 3*/
    }
}