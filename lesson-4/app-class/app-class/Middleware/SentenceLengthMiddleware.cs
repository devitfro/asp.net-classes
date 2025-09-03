namespace app_class.Middleware
{
    public class SentenceLengthMiddleware
    {
        readonly RequestDelegate next;

        public SentenceLengthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string? value = context.Request.Query["value"];
            if (!string.IsNullOrEmpty(value))
            {

                context.Items["valueLength"] = value.Length;
            }

            await next.Invoke(context);
        }
    }
}
