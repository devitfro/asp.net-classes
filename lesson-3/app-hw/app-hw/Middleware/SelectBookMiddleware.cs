using Microsoft.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace app_hw.Middleware
{
    public class SelectBookMiddleware
    {
        private readonly RequestDelegate next;
        private readonly string connectionString;

        public SelectBookMiddleware(RequestDelegate next, string connectionString)
        {
            this.next = next;
            this.connectionString = connectionString;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var category = context.Request.Query["category"];
            if (string.IsNullOrEmpty(category) || category != "fantasy")
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Category is invalid");
                return;
            }
          
            var booksByCategory = await FindBook(category, connectionString);
            context.Response.ContentType = "text/html; charset=utf-8";

            await context.Response.WriteAsync(HtmlHelpers.GenerateHtmlPageWithTable(HtmlHelpers.BuildTable(booksByCategory), "Books by category"));

            //await next.Invoke(context);
        }

        private static async Task<List<Book>> FindBook(string category, string connectionString)
        {
            List<Book> books = new List<Book>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, Name, Category FROM Books WHERE Category LIKE @category";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@category", $"%{category}%");

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            books.Add(new Book(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2)
                            ));
                        }
                    }
                }
            }

            return books;
        }

        record Book(int Id, string Name, string Category)
        {
            public Book(string Name, string Category) : this(0, Name, Category) { }
        }
    }     
}

