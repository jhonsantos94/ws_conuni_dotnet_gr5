using System;
using System.Collections.Concurrent;

namespace ec.edu.monster.Seguridad
{
    public static class AdministradorTokens
    {
        private static readonly ConcurrentDictionary<string, byte> TokensActivos = new ConcurrentDictionary<string, byte>();

        public static string GenerarToken()
        {
            var token = Guid.NewGuid().ToString();
            TokensActivos[token] = 0;
            return token;
        }

        public static bool ValidarToken(string token)
        {

            if (string.IsNullOrWhiteSpace(token))
                return false;

            string tokenLimpio = token.Trim();
            return TokensActivos.ContainsKey(tokenLimpio);
        }
        public static int ContarTokensEnMemoria()
        {
            return TokensActivos.Count;
        }
    }
}

