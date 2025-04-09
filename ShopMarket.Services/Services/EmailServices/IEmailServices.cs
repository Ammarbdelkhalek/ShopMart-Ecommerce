using ShopMarket.Services.DTOS.EmailDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.Services.EmailServices
{
    public interface IEmailServices
    {
        public Task SendEmail(EmailDto emailDto);
    }
}
