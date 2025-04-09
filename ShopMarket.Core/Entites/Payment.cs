using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Core.Entites
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Method { get; set; }
        public string Currency { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public double Amount { get; set; }

        public  ApplicationUser Customer { get; set; }
    }
}
