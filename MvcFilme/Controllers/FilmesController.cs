using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcFilme.Data;
using MvcFilme.Models;
using MvcFilme.Utils;
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
        public async Task<IActionResult> Index([Bind("Genero,Classificacao,BuscaTitulo,AnoLancamento,Ordem,OrdenaPor,PaginaAtual,QuantidadeDeItemPorPagina")] FilmesViewModel filmesViewModel)
        {
            var query = from f in _context.Filme select f;

            if (!String.IsNullOrEmpty(filmesViewModel.BuscaTitulo))
                query = query .Where(f => f.Titulo.Contains(filmesViewModel.BuscaTitulo));
            if(filmesViewModel.Genero != null)
                query = query .Where(f => f.Genero.Equals(filmesViewModel.Genero));
            if(filmesViewModel.Classificacao != null)
                query = query.Where(f => f.Classificacao.Equals(filmesViewModel.Classificacao));
            if (filmesViewModel.AnoLancamento != 0)
                query = query.Where(f => f.Lancamento.Year == filmesViewModel.AnoLancamento);

            switch(filmesViewModel.OrdenaPor)
            {
                case "insercao":
                    if (filmesViewModel.Ordem)
                        query = query.OrderByDescending(f => f.Inserted);
                    else
                        query = query.OrderBy(f => f.Inserted);
                    break;
                case "titulo":
                    if (filmesViewModel.Ordem)
                        query = query.OrderByDescending(f => f.Titulo);
                    else
                        query = query.OrderBy(f => f.Titulo);
                    break;
                case "lancamento":
                    if (filmesViewModel.Ordem)
                        query = query.OrderByDescending(f => f.Lancamento);
                    else
                        query = query.OrderBy(f => f.Lancamento);
                    break;
            }


            await filmesViewModel.SetSelectListItems(_context);
            filmesViewModel.Filmes = await PaginatedList<Filme>.CreateAsync(query, filmesViewModel.PaginaAtual, filmesViewModel.QuantidadeDeItemPorPagina);
            return View(filmesViewModel);
        }

        // GET: Filmes/Details/GUID
        public async Task<IActionResult> Details([FromRoute(Name = "id")]Guid? publicId, [Bind("ApenasEmCartaz,CinemaNome,CinemaCidade,CinemaEstado,InicioExibicao,FimExibicao")] FilmeViewModel filmeViewModel)
        {
            if (publicId == null)
                return PublicIdRequired();

            var filme = await _context.Filme.FirstOrDefaultAsync(m => m.PublicId == publicId);

            if (filme == null)
                return FilmeNotFound();
            

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
                SetMessage(filme.Titulo + " foi criado com sucesso!", "success");
                return RedirectToAction(nameof(Index));
            }
            return View(filme);
        }

        // GET: Filmes/Edit/GUID
        public async Task<IActionResult> Edit([FromRoute(Name = "id")]Guid? publicId)
        {
            if (publicId == null)
                return PublicIdRequired();
            

            var filme = await _context.Filme.FirstOrDefaultAsync(f => f.PublicId == publicId);
            if (filme == null)
                return FilmeNotFound();
            
            return View(filme);
        }

        // POST: Filmes/Edit/GUID
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute(Name = "id")]Guid? publicId, [Bind("PublicId,Titulo,Lancamento,Genero,Capa,Classificacao,Sinopse")] Filme updatedFilme)
        {
            if (publicId == null)
                return PublicIdRequired(); 

            var filme = await _context.Filme.AsNoTracking().Where(f => f.PublicId.Equals(publicId)).Select(f => f).FirstOrDefaultAsync();

            if (filme == null)
                return FilmeNotFound();

            updatedFilme.Id = filme.Id;
            updatedFilme.Inserted = filme.Inserted;
            updatedFilme.LastUpdated = filme.LastUpdated;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Filme.Update(updatedFilme);
                    SetMessage(updatedFilme.Titulo + " foi atualizado criado com sucesso!", "success");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmeExists(filme.Id))
                    {
                        SetMessage("Filme não existe", "danger");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                        throw;
                }
                return RedirectToAction(nameof(Details), new {Id = updatedFilme.PublicId});
            }
            return View(updatedFilme);
        }

        // GET: Filmes/Delete/GUID
        public async Task<IActionResult> Delete([FromRoute(Name = "id")]Guid? publicId)
        {
            if (publicId == null)
                return PublicIdRequired(); 

            var filme = await _context.Filme
                .FirstOrDefaultAsync(m => m.PublicId.Equals(publicId));

            if (filme == null)
                return FilmeNotFound();

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

            if(filme.Cartazes.Count > 0)
            {
                SetMessage("Não é possível remover um filme que está em algum cartaz", "warning");
                return RedirectToAction("Details", "Filmes", new {id = filme.PublicId}, "cartazes");
            }    

            _context.Filme.Remove(filme);
            await _context.SaveChangesAsync();
            SetMessage(filme.Titulo + " foi removido com successo", "success");
            return RedirectToAction(nameof(Index));
        }

        private bool FilmeExists(int id) =>
            _context.Filme.Any(e => e.Id == id);

        private void SetMessage(string message, string type)
        {
            TempData["Message"] = message;
            TempData["MessageType"] = type;
        }
        private IActionResult PublicIdRequired()
        {
            SetMessage("Um id deve ser passado", "danger");
            return RedirectToActionPermanent(nameof(Index));
        }
        private IActionResult FilmeNotFound()
        {
            SetMessage("Filme não existe", "danger");
            return RedirectToActionPermanent(nameof(Index));
        }
    }
}
