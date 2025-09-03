using Humanizer;

namespace app_class.Middleware
{
    public class NumberDeterminantMiddleware
    {
        readonly RequestDelegate next;

        public NumberDeterminantMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
 
            if (int.TryParse(context.Request.Query["value"], out int result))
            {
                if (result >= 1 && result <= 100000)
                {
                    context.Items["parsedNumber"] = result;
                }
                else
                {
                    context.Response.StatusCode = 400;
                    return;
                }
            }

            await next.Invoke(context);
        }
    }
}
