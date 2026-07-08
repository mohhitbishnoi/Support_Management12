using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared;

public class Result<T> : IResult<T>
{
    public string Message { get ; set ; }
    public bool IsSuccess { get ; set ; }
    public int Code { get; set ; }
    public string? Token { get ; set; }
    public T? Data { get ; set ; }
    public Exception? Exception { get ; set ; }

    public static Result<T> Success(string massage)
    {
        return new Result<T>
        {
            Message = massage,
            IsSuccess = true,
            Code = 200
        };
    }
    public static Result<T> Success(T data, string massage)
    {
        return new Result<T>
        {
            Data = data,
            Message = massage,
            IsSuccess = true,
            Code = 200
        };
    }
    public static Result<T> Success(T data, string Token, string massage)
    {
        return new Result<T>
        {
            Token = Token,
            Data = data,
            Message = massage,
            IsSuccess = true,
            Code = 200
        };
    }
    public static Result<T> BadRequest(string massage)
    {
        return new Result<T>
        {
            Message = massage,
            IsSuccess = false,
            Code = 400
        };
    }
}
 
