using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopMarket.Core.Entites
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int StockAmount { get; set; }
        public double? DiscountPercentage { get; set; }
        public string? Image { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }

        [JsonIgnore]
        public double? AfterDiscount => Price - (Price * DiscountPercentage / 100);
        public  Category? Category { get; set; }
        public   Brand? Brand { get; set; }
        public  ICollection<OrderItems>? orderItems { get; set; } = new List<OrderItems>();
        public  ICollection<WishListItems>? wishlistItems { get; set; } = new List<WishListItems>();
        public  ICollection<CartItems>? cartItems { get; set; } = new List<CartItems>();
        public  ICollection<Review>? Reviews { get; set; } = new List<Review>();
    }
}
