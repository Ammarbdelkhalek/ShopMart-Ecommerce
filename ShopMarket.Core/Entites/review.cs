using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopMarket.Core.Entites
{
    public class Review
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public  ApplicationUser? Customer { get; set; }
        public  Product? Product { get; set; }
    }
}
