using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcFilme.Data;
using MvcFilme.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index([Bind("Genero,Classificacao,BuscaTitulo,AnoLancamento")] FilmesViewModel filmesViewModel)
        {
            var filmes = from f in _context.Filme select f;

            if (!String.IsNullOrEmpty(filmesViewModel.BuscaTitulo))
                filmes = filmes.Where(f => f.Titulo.Contains(filmesViewModel.BuscaTitulo));
            if(!String.IsNullOrEmpty(filmesViewModel.Genero))
                filmes = filmes.Where(f => f.Genero.Equals(filmesViewModel.Genero));
            if(filmesViewModel.Classificacao != null)
                filmes = filmes.Where(f => f.Classificacao.Equals(filmesViewModel.Classificacao));
            if (filmesViewModel.AnoLancamento != 0)
                filmes = filmes.Where(f => f.Lancamento.Year == filmesViewModel.AnoLancamento);

            await filmesViewModel.SetSelectListItems(_context);
            filmesViewModel.Filmes = await filmes.ToListAsync(); 
            return View(filmesViewModel);
        }

        // GET: Filmes/Details/GUID
        public async Task<IActionResult> Details([FromRoute(Name = "id")]Guid? publicId, [Bind("ApenasEmCartaz,CinemaNome,CinemaCidade,CinemaEstado,InicioExibicao,FimExibicao")] FilmeViewModel filmeViewModel)
        {
            if (publicId == null)
                return NotFound();
            

            var filme = await _context.Filme.FirstOrDefaultAsync(m => m.PublicId == publicId);

            if (filme == null)
                return NotFound();
            

            var hoje = DateTime.Now;

            filmeViewModel.Filme = filme;
            var query = _context.Cartaz.Where(ca => ca.Filme.Id == filme.Id);

            if (filmeViewModel.ApenasEmCartaz)
                query = query.Where(ca =>  ca.FimExibicao >= hoje);

            if (filmeViewModel.InicioExibicao != null)
                query = query.Where(ca => ca.InicioExibicao >= filmeViewModel.InicioExibicao);

            if (filmeViewModel.FimExibicao != null)
                query = query.Where(ca => ca.FimExibicao <= filmeViewModel.FimExibicao);

            if (!String.IsNullOrWhiteSpace(filmeViewModel.CinemaNome))
                query = query.Where(ca => ca.Cinema.Nome == filmeViewModel.CinemaNome);

            if (!String.IsNullOrWhiteSpace(filmeViewModel.CinemaCidade))
                query = query.Where(ca => ca.Cinema.Cidade == filmeViewModel.CinemaCidade);

            if (filmeViewModel.CinemaEstado != null)
                query = query.Where(ca => ca.Cinema.UnidadeFederativa == filmeViewModel.CinemaEstado);

            filmeViewModel.Cartazes = await query.Include(ca => ca.Cinema).ToListAsync();

            await filmeViewModel.SetSelectListItems(_context);

            return View(filmeViewModel);
        }

        // GET: Filmes/Create
        public IActionResult Create() => View();
       

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

        // GET: Filmes/Edit/GUID
        public async Task<IActionResult> Edit([FromRoute(Name = "id")]Guid? publicId)
        {
            if (publicId== null)
                return NotFound();
            

            var filme = await _context.Filme.FirstOrDefaultAsync(f => f.PublicId == publicId);
            if (filme == null)
                return NotFound();
            
            return View(filme);
        }

        // POST: Filmes/Edit/GUID
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute(Name = "id")]Guid publicId, [Bind("PublicId,Titulo,Lancamento,Genero,Capa,Classificacao,Sinopse")] Filme updatedFilme)
        {
            var filmeId = await _context.Filme.Where(f => f.PublicId.Equals(publicId)).Select(f => f.Id).FirstOrDefaultAsync();

            if (filmeId == 0)
                return NotFound();

            updatedFilme.Id = filmeId;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Filme.Update(updatedFilme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmeExists(filmeId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(updatedFilme);
        }

        // GET: Filmes/Delete/GUID
        public async Task<IActionResult> Delete([FromRoute(Name = "id")]Guid publicId)
        {
            var filme = await _context.Filme
                .FirstOrDefaultAsync(m => m.PublicId.Equals(publicId));

            if (filme == null)
            {
                return NotFound();
            }

            return View(filme);
        }

        // POST: Filmes/Delete/GUID
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

        private bool FilmeExists(int id) =>
            _context.Filme.Any(e => e.Id == id);
        
    }
}
