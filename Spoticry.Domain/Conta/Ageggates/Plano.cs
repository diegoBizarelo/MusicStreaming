﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spoticry.Domain.Conta.Ageggates
{
    public class Plano
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }
        
        [JsonPropertyName("descricao")]
        public string Descricao { get; set; }

        [JsonPropertyName("valor")]
        public decimal Valor { get; set; }
    }
}
