using Microsoft.AspNetCore.Mvc;

namespace SemanticSearch.Controllers
{
    public class PreviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
