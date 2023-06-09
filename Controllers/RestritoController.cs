using Microsoft.AspNetCore.Mvc;
using WebListaMercado.Filters;

namespace WebListaMercado.Controllers
{
    [PaginaParaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
