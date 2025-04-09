using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopMarket.Core.Entites
{
    public class WishList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CustomerId { get; set; }
        public  ApplicationUser Customer { get; set; }
        
        public  ICollection<WishListItems>? wishlistItems { get; set; } = new List<WishListItems>();

    }
}
