﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.DTOS.EmailDto
{
    public class EmailDto
    {
        public string ToEmail {  get; set; }
        public string  Subject { get; set; }
        public string Body { get; set; }
    }
}
