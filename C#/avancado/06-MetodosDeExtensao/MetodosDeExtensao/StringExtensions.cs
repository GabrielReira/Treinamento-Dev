using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodosDeExtensao
{
    public static class StringExtensions
    {
        public static string UpperLowerCase(this string frase, bool caixaInicial)
        {
            bool isUpperCase = caixaInicial;
            string resultado = "";
            for (int i=0; i<frase.Length; i++)
            {
                resultado += isUpperCase ? (frase[i].ToString().ToUpper()) : (frase[i].ToString().ToLower());
                isUpperCase = !isUpperCase;
            }
            return resultado;
        }
    }
}
