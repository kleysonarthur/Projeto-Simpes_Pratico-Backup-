using Microsoft.AspNetCore.Mvc;
using SimplesPratico.Filters;

namespace SimplesPratico.Controllers {
    [FiltroLogado]
    public class RestritoController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
