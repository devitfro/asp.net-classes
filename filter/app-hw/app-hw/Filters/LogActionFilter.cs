using Microsoft.AspNetCore.Mvc.Filters;

namespace app_hw.Filters
{
    public class LogActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"[{DateTime.Now}] Вызвано действие: {context.ActionDescriptor.DisplayName}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"[{DateTime.Now}] Завершено действие: {context.ActionDescriptor.DisplayName}");
        }
    }
}
