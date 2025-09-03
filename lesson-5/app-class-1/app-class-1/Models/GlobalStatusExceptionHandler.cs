namespace app_class_1.Models
{
    public class GlobalStatusExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalStatusExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            switch (context.Response.StatusCode)
            {
                case 400: { context.Response.Redirect("/400"); break; }
                case 401: { context.Response.Redirect("/401"); break; }
                case 404: { context.Response.Redirect("/404"); break; }
                default:
                    {
                        context.Request.Path = "/";
                        break;
                    }
            }
        }
    }
}
