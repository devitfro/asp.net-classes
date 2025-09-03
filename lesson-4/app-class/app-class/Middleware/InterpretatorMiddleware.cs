using Humanizer;

namespace app_class.Middleware
{
    public class InterpretatorMiddleware
    {
        readonly RequestDelegate next;
        public InterpretatorMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Items.ContainsKey("parsedNumber"))
            {
                int value = (int)context.Items["parsedNumber"]!;

                string body = $"""
                    <html>
                        <body>
                            <p style="color:gray; text-align:center; font-weight:bold">Value: {value.ToWords()}</p>
                        </body>
                    </html>
                    """;

                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync(body);
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}
