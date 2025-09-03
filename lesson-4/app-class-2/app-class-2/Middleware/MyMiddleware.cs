namespace app_class_2.Middleware
{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;

        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync( HttpContext context,ISingletonService singleton1, IScopedService scoped1, ITransientService transient1) // Получаем через параметр метода
        {
            // Получаем через контекст
            var singleton2 = context.RequestServices.GetService<ISingletonService>();
            var scoped2 = context.RequestServices.GetService<IScopedService>();
            var transient2 = context.RequestServices.GetService<ITransientService>();

            context.Response.ContentType = "text/html;charset=utf-8";

            var html = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Title</title>
                <style>
                    body {{ margin: 20px; }}
                    .card {{ border: 2px solid #333; padding: 15px; margin: 10px; border-radius: 8px; }}
                </style>
            </head>
            <body>
                <div class='card'>
                    <h2>Singleton - Один на всё приложение</h2>
                    <p>Способ 1: {singleton1.Id}</p>
                    <p>Способ 2: {singleton2.Id}</p>
                </div>

                <div class='card'>
                    <h2>Scoped - одинаковый в одном запросе, но разные между запросами</h2>
                    <p>Способ 1: {scoped1.Id}</p>
                    <p>Способ 2: {scoped2.Id}</p>
                </div>

                <div class='card'>
                    <h2>Transient - каждый раз новый экземпляр</h2>
                    <p>Способ 1: {transient1.Id}</p>
                    <p>Способ 2: {transient2.Id}</p>
                </div>

            </body>
            </html>";

            await context.Response.WriteAsync(html);
        }
    }
}
