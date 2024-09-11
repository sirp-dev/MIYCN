using Microsoft.AspNetCore.Mvc;

namespace RazorWebUI.Pages.Shared.ViewComponents
{
    public class UserNavigationViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
        }
}
