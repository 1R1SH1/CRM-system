using Microsoft.AspNetCore.Mvc;

namespace IT_Consulting_CRM_Web.ViewComponents
{
    public class LoginView : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
