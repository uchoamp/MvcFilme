using Microsoft.AspNetCore.Mvc;
using MvcFilme.Data;
using System.Threading.Tasks;

namespace MvcFilme.Controllers
{
    public class CinemasController: Controller
    {
        private readonly MvcFilmeContext _context;

        public CinemasController(MvcFilmeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
