namespace LoginServer.Helpers
{
    using Google.Protobuf;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;

    public class ProtoBufOutputFormatter : OutputFormatter
    {
        public ProtoBufOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/x-protobuf"));
        }

        protected override bool CanWriteType(Type? type) => typeof(IMessage).IsAssignableFrom(type);

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var message = (IMessage)context.Object!;
            message.WriteTo(context.HttpContext.Response.Body);
            await Task.CompletedTask;
        }
    }

}
