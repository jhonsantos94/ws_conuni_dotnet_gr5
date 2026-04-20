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
            return !string.IsNullOrWhiteSpace(token) && TokensActivos.ContainsKey(token);
        }
    }
}

