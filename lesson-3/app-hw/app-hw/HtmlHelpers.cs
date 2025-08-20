using System.Reflection;
using System.Text;

namespace app_hw
{
    public static class HtmlHelpers
    {
        public static string BuildTable<T>(IEnumerable<T> collection)
        {
            StringBuilder table = new StringBuilder();
            PropertyInfo[] properties = typeof(T).GetProperties();

            table.Append("<table class='table table-bordered'>");

            foreach (T item in collection)
            {
                table.Append("<tr>");
                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(item);
                    table.Append($"<td>{value}</td>");
                }
                table.Append("</tr>");
            }
            table.Append("</table>");

            return table.ToString();
        }

        public static string GenerateHtmlPageWithTable(string body, string header)
        {
            return $"""
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset="utf-8" />
                <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet" 
                integrity="sha384-KK94CHFLLe+nY2dmCWGMq91rCGa5gtU4mk92HdvYe+M/SXH301p5ILy+dN9+nJOZ" crossorigin="anonymous">
                <title>{header}</title>
            </head>
            <body>
            <div class="container">
            <h2 class="d-flex justify-content-center">{header}</h2>
            <div class="mt-5"></div>
            {body}
            <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/js/bootstrap.bundle.min.js"
            integrity="sha384-ENjdO4Dr2bkBIFxQpeoTz1HIcje39Wm4jDKdf19U8gI4ddQ3GYNS7NTKfAdVQSZe" crossorigin="anonymous"></script>
            </div>
            </body>
            </html>
            """;
        }
    }
}
