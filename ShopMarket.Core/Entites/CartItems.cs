using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopMarket.Core.Entites
{
    public class CartItems
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int? CartId { get; set; }
        public int? ProductId { get; set; }
        public  Cart? Cart { get; set; }
        public  Product? Product { get; set; }
    }
}
