using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRVS.Core.Models
{
    public class JwtSettings
    {
        public string SecretKey { get; }

        public JwtSettings(IConfiguration configuration)
        {
            SecretKey = configuration["Jwt:Secret"];
        }
    }
}
