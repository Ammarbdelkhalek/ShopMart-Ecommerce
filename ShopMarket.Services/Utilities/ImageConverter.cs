using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.Utilities
{
    public static class ImageConverters
    {
        public static async Task<byte[]> ToByteArray(this IFormFile image)
        {
            using (var stream = new MemoryStream())
            {
                await image.CopyToAsync(stream);
                return stream.ToArray();
            }
        }
    }
}
