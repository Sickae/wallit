using Microsoft.AspNetCore.Mvc;

namespace WallIT.Web.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected void SetTitle(string title)
        {
            if (string.IsNullOrEmpty(title) == false)
                ViewBag.Title = title;
        }

        protected virtual void FillViewBags()
        { }
    }
}
