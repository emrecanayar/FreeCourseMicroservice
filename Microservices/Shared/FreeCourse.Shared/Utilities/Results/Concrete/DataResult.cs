using FreeCourse.Shared.Utilities.Results.Abstract;
using FreeCourse.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreeCourse.Shared.Utilities.Results.Concrete
{
    public class DataResult<T> : IDataResult<T>
    {
        public DataResult(ResultStatus resultStatus, T data, int statusCode)
        {
            ResultStatus = resultStatus;
            Data = data;
            StatusCode = statusCode;
        }
        public DataResult(ResultStatus resultStatus, T data, string message, int statusCode)
        {
            ResultStatus = resultStatus;
            Data = data;
            Message = message;
            StatusCode = statusCode;
        }

        public DataResult(ResultStatus resultStatus, T data, string message, int statusCode, Exception exception)
        {
            ResultStatus = resultStatus;
            Data = data;
            Message = message;
            StatusCode = statusCode;
            Exception = exception;
        }
        public T Data { get; }

        public ResultStatus ResultStatus { get; }

        public string Message { get; }

        public Exception Exception { get; }

        public int StatusCode { get; }
    }
}
