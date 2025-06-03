using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterTimeServer.Shared.Helpers
{
    public static class UsuarioHelper
    {
        public static int GetIdade(DateTime dataNascimento)
        {
            TimeSpan idade = DateTime.UtcNow - dataNascimento;
            return (int)(idade.TotalDays / 365);
        }

        public static int GetMlPorDia(int idade, int peso)
        {
            int fator = idade switch
            {
                <= 17 => 40,
                <= 55 => 35,
                <= 65 => 30,
                _ => 25
            };
            return peso * fator;
        }
    }
}
