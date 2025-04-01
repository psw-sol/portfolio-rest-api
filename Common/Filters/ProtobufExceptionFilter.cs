using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Protos;

namespace Common.Filters
{

    public class ProtobufExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = new PResponse
            {
                ErrorCode = 500,
                ErrMsg = context.Exception.Message
            };

            context.Result = new FileContentResult(response.ToByteArray(), "application/x-protobuf");
            context.ExceptionHandled = true;
        }
    }

}
