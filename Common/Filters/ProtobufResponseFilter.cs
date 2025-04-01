using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Google.Protobuf;
using Shared.Protos; // PResponse

namespace Common.Filters
{

    public class ProtobufResponseFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ObjectResult objectResult &&
                objectResult.Value is IMessage protoMessage)
            {
                var pResponse = new PResponse
                {
                    ErrorCode = 0,
                    ErrMsg = "",
                    Data = protoMessage.ToByteString()
                };

                context.Result = new FileContentResult(pResponse.ToByteArray(), "application/x-protobuf");
            }

            await next();
        }
    }

}
