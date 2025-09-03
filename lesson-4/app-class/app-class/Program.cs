using app_class.Middleware;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<AuthenticationMiddleware>();
app.UseMiddleware<RoutingMiddleware>();
app.UseMiddleware<NumberDeterminantMiddleware>();
app.UseMiddleware<SentenceLengthMiddleware>();
app.UseMiddleware<InterpretatorMiddleware>();

//������� ���������� �������������� �����. �������� ����� �� 1 �� 100000. ��� ��������� HTTP GET ������� ���������� ������������ ��������� middleware-�����������.


app.MapGet("/", () => "Hello World!");

app.Run();


