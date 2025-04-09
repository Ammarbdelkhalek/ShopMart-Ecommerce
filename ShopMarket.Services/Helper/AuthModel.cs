using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopMarket.Services.Helper
{
    public class AuthModel
    {
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; } = int.MaxValue;
        public string Token { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsSuccess { get; set; } = false;
        public DateTime? ExpiresOn { get; set; }
        
        public string RefreshToken { get; set; } = string.Empty;
        public List<string> Roles { get; set; }  =new List<string>();
        public DateTime RefreshTokenExpiration { get; set; }


    }
}
