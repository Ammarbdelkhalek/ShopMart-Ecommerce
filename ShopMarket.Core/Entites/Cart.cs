using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopMarket.Core.Entites
{
    public class Cart
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }

        public  ApplicationUser Customer { get; set; }
         
        public  ICollection<CartItems>? cartItems { get; set; } = new List<CartItems>();
    }
}
