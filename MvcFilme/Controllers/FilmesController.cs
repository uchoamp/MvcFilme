using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcFilme.Data;
using MvcFilme.Models;

namespace MvcFilme.Controllers
{
    public class FilmesController : Controller
    {
        private readonly MvcFilmeContext _context;

        public FilmesController(MvcFilmeContext context)
        {
            _context = context;
        }

        // GET: Filmes
        public async Task<IActionResult> Index(string genero, string busca, string classificacao)
        {
            IQueryable<string> consultaGenero = from m in _context.Filme orderby m.Genero select m.Genero;
            var filmes = from m in _context.Filme select m;

            if (!String.IsNullOrEmpty(busca))
                filmes = filmes.Where(f => f.Titulo.Contains(busca));
            if(!String.IsNullOrEmpty(genero))
                filmes = filmes.Where(x => x.Genero.Equals(genero));

            var filmeGeneroVM = new FilmeViewModel();

            filmeGeneroVM.filmes = await filmes.ToListAsync(); 
            return View(filmeGeneroVM);
        }

        // GET: Filmes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme
                .FirstOrDefaultAsync(m => m.PublicId == id);
            if (filme == null)
            {
                return NotFound();
            }

            return View(filme);
        }

        // GET: Filmes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Filmes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titulo,Lancamento,Genero,Capa,Classificacao,Sinopse")] Filme filme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filme);
        }

        // GET: Filmes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme.FirstOrDefaultAsync(f => f.PublicId == id);
            if (filme == null)
            {
                return NotFound();
            }
            return View(filme);
        }

        // POST: Filmes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Titulo,Lancamento,Genero,Capa,Classificacao,Sinopse")] Filme updatedFilme)
        {
            var filme = await _context.Filme.AsNoTracking().FirstOrDefaultAsync(f => f.PublicId.Equals(id));

            if (filme is null)
                return NotFound();

            updatedFilme.Id = filme.Id;
            updatedFilme.PublicId = id;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Filme.Update(updatedFilme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmeExists(filme.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(filme);
        }

        // GET: Filmes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme
                .FirstOrDefaultAsync(m => m.PublicId.Equals(id));
            if (filme == null)
            {
                return NotFound();
            }

            return View(filme);
        }

        // POST: Filmes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid publicId)
        {
            var filme = await _context.Filme.FirstOrDefaultAsync(f => f.PublicId.Equals(publicId));
            if (filme == null)
            {
                return NotFound();
            }
            _context.Filme.Remove(filme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmeExists(int id)
        {
            return _context.Filme.Any(e => e.Id == id);
        }
    }
}
