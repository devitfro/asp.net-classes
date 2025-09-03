namespace app_class.Middleware
{
    public class RoutingMiddleware
    {
        readonly RequestDelegate next;

        public RoutingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path;
            if (path == "/index")
            {
                await context.Response.WriteAsync("Home Page");
            }
            else if (path == "/about")
            {
                await context.Response.WriteAsync("About Page");
            }
            else
            {
                next.Invoke(context);
                //context.Response.StatusCode = 404;
                //await context.Response.WriteAsync("Not Found");
            }
        }
    }
}
