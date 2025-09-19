using app_class_1.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace app_class_1.Components
{
    //[ViewComponent]
    public class TimerViewComponent : ViewComponent
    {
        private readonly ITimeService _ts;

        public TimerViewComponent(ITimeService ts)
        {
            _ts = ts;
        }

        public IViewComponentResult Invoke()
        {
            string html = $"<h1>{_ts.GetTime()}</h1>";
            return new HtmlContentViewComponentResult(new HtmlString(html));


        }

    }
}
