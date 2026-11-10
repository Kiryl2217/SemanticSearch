using Microsoft.AspNetCore.Mvc;

namespace SemanticSearch.Controllers
{
    public class WebApplicationInfo : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
