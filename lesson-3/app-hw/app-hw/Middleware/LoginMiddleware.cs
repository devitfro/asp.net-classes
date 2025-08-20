namespace app_hw.Middleware
{
    public class LoginMiddleware
    {
        private readonly RequestDelegate next;

        public LoginMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["token"];
            if (token != "token12345")
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Token is invalid");
                return;
            }

            await next.Invoke(context);
        }
    }
}





