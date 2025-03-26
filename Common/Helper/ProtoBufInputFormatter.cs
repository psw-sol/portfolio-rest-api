namespace Common.Helpers
{
    using Google.Protobuf;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;

    public class ProtoBufInputFormatter : InputFormatter
    {
        public ProtoBufInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/x-protobuf"));
        }

        protected override bool CanReadType(Type type) => typeof(IMessage).IsAssignableFrom(type);

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var message = (IMessage)Activator.CreateInstance(context.ModelType)!;
            var result = message.Descriptor.Parser.ParseFrom(context.HttpContext.Request.Body);
            return await InputFormatterResult.SuccessAsync(result);
        }
    }

}
