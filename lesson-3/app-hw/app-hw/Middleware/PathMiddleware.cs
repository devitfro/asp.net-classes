namespace app_hw.Middleware
{
    public class PathMiddleware
    {
        private readonly RequestDelegate next;

        public PathMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;
            if (path != "/allbooks")
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("path is invalid");
                return;
            }

            await next.Invoke(context);
        }
    }
}
