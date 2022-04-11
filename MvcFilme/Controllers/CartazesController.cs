using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcFilme.Data;
using MvcFilme.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFilme.Controllers
{
    public class CartazesController : Controller
    {
        private readonly MvcFilmeContext _context;

        public CartazesController(MvcFilmeContext context)
        {
            _context = context; 
        }
        
        // GET /Cartazes/Details/GUID
        public async Task<IActionResult> Details([FromRoute(Name = "id")] Guid publicId)
        {
            var cartaz = await _context.Cartaz.Include(ca => ca.Filme).Include(ca => ca.Cinema).FirstOrDefaultAsync(ca => ca.PublicId == publicId);

            if (cartaz == null)
                return NotFound();

            return View(cartaz);
        }

        // GET /Cartazes/Create
        public async Task<IActionResult> Create([Bind("FilmePublicId,CinemaPublicId")]CartazViewModel cartazViewModel)
        {
            await cartazViewModel.SetSelectListItems(_context);
            ModelState.Clear();
            return View(cartazViewModel);
        }


        // POST /Cartazes/Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost([Bind("FilmePublicId,CinemaPublicId,InicioExibicao,FimExibicao,Preco")]CartazViewModel cartazViewModel)
        {
            var cartaz = new Cartaz
            {
                InicioExibicao = cartazViewModel.InicioExibicao,
                FimExibicao = cartazViewModel.FimExibicao,
                Preco = cartazViewModel.Preco
            };
            cartaz.FilmeId = await _context.Filme.Where(f => f.PublicId == cartazViewModel.FilmePublicId).Select(f => f.Id).FirstOrDefaultAsync();
            cartaz.CinemaId = await _context.Cinema.Where(c => c.PublicId == cartazViewModel.CinemaPublicId).Select(c => c.Id).FirstOrDefaultAsync();

            if (cartaz.FilmeId == 0)
                ModelState.AddModelError("FilmePublicId", "Filme não existe");

            if (cartaz.CinemaId == 0)
                ModelState.AddModelError("CinemaPublicId", "Cinema não existe");

            if (ModelState.IsValid)
            {
                _context.Add(cartaz);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Cinemas", new {id = cartazViewModel.CinemaPublicId});
            }

            await cartazViewModel.SetSelectListItems(_context);
            return View(cartazViewModel);
        }

        // GET /Cartazes/Edit/GUID
        public async Task<IActionResult> Edit([FromRoute(Name = "id")] Guid publicId)
        {
            var cartaz = await _context.Cartaz.Include(ca => ca.Filme).Include(ca => ca.Cinema).FirstOrDefaultAsync(ca => ca.PublicId == publicId);

            if (cartaz == null)
                return NotFound();

            var cartazViewModel = new CartazViewModel
            {
                Cartaz = cartaz,
                InicioExibicao = cartaz.InicioExibicao,
                FimExibicao = cartaz.FimExibicao,
                Preco = cartaz.Preco,
                FilmePublicId = cartaz.Filme.PublicId,
                CinemaPublicId = cartaz.Cinema.PublicId
            };

            await cartazViewModel.SetSelectListItems(_context);

            return View(cartazViewModel);
        }

        // POST /Cartazes/Edit/GUID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute(Name = "id")] Guid publicId, [Bind("FilmePublicId,CinemaPublicId,InicioExibicao,FimExibicao,Preco")]CartazViewModel cartazViewModel)
        {

            var cartaz = await _context.Cartaz.FirstOrDefaultAsync(ca => ca.PublicId == publicId);
            if (cartaz == null)
                return NotFound();

            cartaz.InicioExibicao = cartazViewModel.InicioExibicao;
            cartaz.FimExibicao = cartazViewModel.FimExibicao;
            cartaz.Preco = cartazViewModel.Preco;
            cartaz.FilmeId = await _context.Filme.Where(f => f.PublicId == cartazViewModel.FilmePublicId).Select(f => f.Id).FirstOrDefaultAsync();
            cartaz.CinemaId = await _context.Cinema.Where(c => c.PublicId == cartazViewModel.CinemaPublicId).Select(c => c.Id).FirstOrDefaultAsync();

            if (cartaz.FilmeId == 0)
                ModelState.AddModelError("FilmePublicId", "Filme não existe");

            if (cartaz.CinemaId == 0)
                ModelState.AddModelError("CinemaPublicId", "Cinema não existe");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartaz);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Cinemas", new { id = cartazViewModel.CinemaPublicId });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartazExists(cartaz.Id))
                        return NotFound();
                    else
                        throw;

                }
            }

            await cartazViewModel.SetSelectListItems(_context);
            return View(cartazViewModel);
        }

        public bool CartazExists(int id) =>
            _context.Cartaz.Any(ca => ca.Id == id);

        // GET /Cartazes/Delete/GUID
        public async Task<IActionResult> Delete([FromRoute(Name = "id")] Guid publicId)
        {
            var cartaz = await _context.Cartaz.Include(ca => ca.Filme).Include(ca => ca.Cinema).FirstOrDefaultAsync(ca => ca.PublicId == publicId);

            if (cartaz == null)
                return NotFound();

            return View(cartaz);
        }

        // GET /Cartazes/Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(Guid publicId)
        {
            var cartaz = await _context.Cartaz.Include(ca => ca.Cinema).FirstOrDefaultAsync(ca => ca.PublicId == publicId);
            if (cartaz == null)
                return NotFound();

            _context.Remove(cartaz);
            await _context.SaveChangesAsync();
            await CinemasViewModel.UpdateCidades(_context);

            return RedirectToAction("Details", "Cinemas", new {id = cartaz.Cinema.PublicId});
        }
    }
}
