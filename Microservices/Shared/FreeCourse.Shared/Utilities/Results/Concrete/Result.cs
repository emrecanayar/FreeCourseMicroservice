using FreeCourse.Shared.Utilities.Results.Abstract;
using FreeCourse.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreeCourse.Shared.Utilities.Results.Concrete
{
    public class Result : IResult
    {
        public Result(ResultStatus resultStatus, int statusCode)
        {
            ResultStatus = resultStatus;
            StatusCode = statusCode;
        }
        public Result(ResultStatus resultStatus, string message, int statusCode)
        {
            ResultStatus = resultStatus;
            Message = message;
            StatusCode = statusCode;
        }

        public Result(ResultStatus resultStatus, string message, int statusCode,Exception exception)
        {
            ResultStatus = resultStatus;
            Message = message;
            Exception = exception;
            StatusCode = statusCode;
        }


        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        public Exception Exception { get; }
        public int StatusCode { get; }
    }
}
