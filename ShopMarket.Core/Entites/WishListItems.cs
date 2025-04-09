using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopMarket.Core.Entites
{
    public class WishListItems
    {
        public int Id { get; set; }
        public int? WishlistId { get; set; }
        public int? ProductId { get; set; }

        public  WishList? Wishlist { get; set; }
         
        public  Product? Product { get; set; }  
    }
}
