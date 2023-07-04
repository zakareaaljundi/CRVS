using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CRVS.Core.Models
{
    public static class SecretKeyGenerator
    {
        public static string GenerateSecretKey()
        {
            byte[] keyBytes = new byte[32]; // 32 bytes = 256 bits
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(keyBytes);
            }
            return Convert.ToBase64String(keyBytes);
        }
    }
}
