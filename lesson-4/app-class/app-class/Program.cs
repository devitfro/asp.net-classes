using app_class.Middleware;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<AuthenticationMiddleware>();
app.UseMiddleware<RoutingMiddleware>();
app.UseMiddleware<NumberDeterminantMiddleware>();
app.UseMiddleware<SentenceLengthMiddleware>();
app.UseMiddleware<InterpretatorMiddleware>();

//Создать приложение «Интерпретатор чисел». Диапазон чисел от 1 до 100000. Для обработки HTTP GET запроса необходимо использовать несколько middleware-компонентов.


app.MapGet("/", () => "Hello World!");

app.Run();


