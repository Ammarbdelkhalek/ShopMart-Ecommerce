using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Core.Errors
{
    public class Error
    {
        public static readonly Error None = new Error(string.Empty,200);
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public Error(string message , int statuscode)
        {
            Message = message ?? GetDefaultMessageForStatusCode(StatusCode);
            StatusCode = statuscode;
        }       

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A badRequest , You Have made!",
                401 => "Authorized , you are not !",
                404 => "Resource was not found !",
                500 => "Server Error",
                _ => "Invalid Requrest"
            };
        }
    }
}
