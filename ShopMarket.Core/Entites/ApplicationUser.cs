using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopMarket.Core.Entites
{
    public class ApplicationUser :IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string? Address { get; set; }
        public   ICollection<Orders>? Orders { get; set; } = new List<Orders>();
       
        public   ICollection<Cart>? Carts { get; set; } = new List<Cart>();
        public   ICollection<WishList>? Wishlists { get; set; } = new List<WishList>();
        public   ICollection<Payment>? Payments { get; set; } = new List<Payment>();
        public   ICollection<Review>? Reviews { get; set; } = new List<Review>();
        public   ICollection<RefreshToken>? RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
