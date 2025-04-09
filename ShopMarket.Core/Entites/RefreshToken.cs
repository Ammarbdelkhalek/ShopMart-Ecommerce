using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Core.Entites
{
    [Owned]
    public class RefreshToken
    {

        public string Token { get; set; }
        public DateTime ExpriesOn { get; set; }
        public bool IsExpired { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime RevokedOn { get; set; }
        public bool IsActive => RevokedOn == null && !IsExpired;
    }
}
