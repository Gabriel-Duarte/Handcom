using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Services.Responses
{
    public class ResponseSuccess
    {
        public bool IsSuccess { get; }
        public object? Data { get; }

        public ResponseSuccess(object? data, bool isSuccess = true)
        {
            IsSuccess = isSuccess;
            Data = data;
        }
    }
}