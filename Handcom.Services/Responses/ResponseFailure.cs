using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handcom.Services.Responses
{
    public class ResponseFailure
    {
        public bool IsSuccess { get; }
        public IEnumerable<string> Errors { get; }

        public ResponseFailure(IEnumerable<string> errors, bool isSuccess = false)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }
    }
}
