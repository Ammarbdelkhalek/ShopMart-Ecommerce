using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Core.Errors
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string SuccessMessage;
        public Error Error { get; }
        public Result(bool isSuccess , Error error)
        {
            if ((isSuccess && error != Error.None) || (!isSuccess && error == Error.None))
            {
                throw new InvalidOperationException("");
            }
            IsSuccess = isSuccess;
            Error = error;  
        }
        public Result(bool isSuccess , string sucessMessage)
        {
            if(!isSuccess || sucessMessage is null) throw new InvalidOperationException();
            IsSuccess = isSuccess ;
            SuccessMessage= sucessMessage;
            Error = Error.None;
        }

        public static Result Success() => new Result(true, Error.None);
        public static Result Success(string SuccessMessage)=>new Result(true, SuccessMessage);
        public static Result Failure()=>new Result(false, Error.None);

        public static Result<T> Success<T> (T Value) => new Result<T>(Value, true, Error.None);
        public static Result<T> Failure<T>(Error? Error) => new Result<T>(default, false, Error);

    }
    public class Result<T> : Result
    {
        public T Value { get; }
        public bool IsSuccess { get; }
        public Error Error { get; }
        public Result(T value , bool isSuccess , Error error):base(isSuccess , error)
        {
            Value = IsSuccess ? value! : throw new InvalidOperationException("Failure results cannot have value");
        }
    }

}

