﻿using Spoticry.Core.Validation;
using Spoticry.Domain.Conta.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Spoticry.Domain.Conta.ValueObject
{
    public class CPF
    {
        private CPFException _validationError = new CPFException();
        public CPF() { }

        public CPF(string numero)
        {
            Numero = numero;

            if (IsValido() == false)
            {
                _validationError.AdicionarError(new BusinessValidation()
                {
                    ErrorMessage = "CPF Inválido",
                    ErrorName = nameof(CPFException)
                });

                _validationError.ValidateAndThrow();
            }
        }

        public String Numero { get; set; }

        public String NumeroFormatado()
        {
            return Convert.ToUInt64(Numero).ToString(@"000\.000\.000\-00");
        }

        public bool IsValido()
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            var cpf = Numero.Trim().Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}
