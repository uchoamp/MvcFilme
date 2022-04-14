using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MvcFilme.Data;
using MvcFilme.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MvcFilme.Controllers
{
    public class CinemasController : Controller
    {
        private readonly MvcFilmeContext _context;

        public CinemasController(MvcFilmeContext context)
        {
            _context = context;
        }

        // GET: /Cinemas 
        public async Task<IActionResult> Index(string cidade, UnidadesFederativas? estado)
        {
            var hoje = DateTime.Now;
            var query = _context.Cinema
                .Select(c => new Cinema
                {
                    PublicId = c.PublicId,
                    Nome = c.Nome,
                    Cidade = c.Cidade,
                    UnidadeFederativa = c.UnidadeFederativa,
                    Telefone = c.Telefone,
                    QuantidadeFilmesEmCartaz = c.Cartazes.Where(ca => ca.FimExibicao >= hoje).Count()
                });

            if (!String.IsNullOrWhiteSpace(cidade))
                query = query.Where(c => c.Cidade == cidade);
            if (estado != null)
                query = query.Where(c => c.UnidadeFederativa == estado);

            var cinemaViewModel = new CinemasViewModel();
            cinemaViewModel.Cinemas = await query.ToListAsync();
            return View(cinemaViewModel);
        }

        // GET /Cinemas/Details/GUID
        public async Task<IActionResult> Details([FromRoute(Name = "id")]Guid? publicId, [Bind("ApenasEmCartaz,FilmeTitulo,FilmeGenero,FilmeClassificacao,FilmeAnoLancamento,InicioExibicao,FimExibicao")] CinemaViewModel cinemaViewModel)
        {
            if (publicId == null)
                return PublicIdRequired();

            var hoje = DateTime.Now;
            var cinema = await _context.Cinema.Where(c => c.PublicId == publicId).Select(c => new Cinema
            {
                Id = c.Id,
                PublicId = c.PublicId,
                Nome = c.Nome,
                Cidade = c.Cidade,
                UnidadeFederativa = c.UnidadeFederativa,
                Telefone = c.Telefone,
                QuantidadeCartazes = c.Cartazes.Count,
                QuantidadeFilmesEmCartaz = c.Cartazes.Where(ca => ca.FimExibicao >= hoje).Count()
            }).FirstOrDefaultAsync();

            if (cinema == null)
                return CinemaNotFound();

            cinemaViewModel.Cinema = cinema;

            var query = _context.Cartaz.Where(ca => ca.CinemaId == cinema.Id);

            if (cinemaViewModel.ApenasEmCartaz)
                query = query.Where(ca => ca.FimExibicao >= hoje);

            if (cinemaViewModel.InicioExibicao != null)
                query = query.Where(ca => ca.InicioExibicao >= cinemaViewModel.InicioExibicao);

            if (cinemaViewModel.FimExibicao != null)
                query = query.Where(ca => ca.FimExibicao <= cinemaViewModel.FimExibicao);

            if (!String.IsNullOrWhiteSpace(cinemaViewModel.FilmeTitulo))
                query = query.Where(ca => ca.Filme.Titulo.Contains(cinemaViewModel.FilmeTitulo));

            if (cinemaViewModel.FilmeGenero != null)
                query = query.Where(ca => ca.Filme.Genero.Equals(cinemaViewModel.FilmeGenero));

            if (cinemaViewModel.FilmeClassificacao != null)
                query = query.Where(ca => ca.Filme.Classificacao == cinemaViewModel.FilmeClassificacao);
            
            if (cinemaViewModel.FilmeAnoLancamento != 0)
                query = query.Where(ca => ca.Filme.Lancamento.Year == cinemaViewModel.FilmeAnoLancamento);

            await cinemaViewModel.SetSelectListItems(_context);
            cinemaViewModel.Cartazes = await query.Include(ca => ca.Filme).ToListAsync();

            return View(cinemaViewModel);  
        }

        // GET /Cinemas/Create 
        public IActionResult Create() => View();

        // POST /Cinemas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Cidade,UnidadeFederativa,Telefone")] Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cinema);
                await _context.SaveChangesAsync();

                await CinemasViewModel.UpdateCidades(_context);
                SetMessage("Cinema " + cinema.Nome + " adicionado com sucesso", "success");
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
        }

        // GET /Cinemas/Edit/GUID
        public async Task<IActionResult> Edit([FromRoute(Name = "id")]Guid? publicId)
        {
            if (publicId == null)
                return PublicIdRequired();

            var cinema = await _context.Cinema.FirstOrDefaultAsync(f => f.PublicId == publicId);

            if (cinema == null)
                return CinemaNotFound();

            return View(cinema);
        }

        // POST /Cinemas/Edit/GUID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute(Name = "id")]Guid? publicId, [Bind("PublicId,Nome,Cidade,UnidadeFederativa,Telefone")] Cinema updatedCinema)
        {
            if (publicId == null)
                return PublicIdRequired();

            // Sem AsNoTracking ocorre um conflito de Id
            var cinema = await _context.Cinema.AsNoTracking().Where(c => c.PublicId == publicId).Select(c => c).FirstOrDefaultAsync();

            if (cinema == null)
                return CinemaNotFound();

            updatedCinema.Id = cinema.Id;
            updatedCinema.Inserted = cinema.Inserted;
            updatedCinema.LastUpdated = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(updatedCinema);
                    await _context.SaveChangesAsync();
                    await CinemasViewModel.UpdateCidades(_context);
                    SetMessage("Cinema " + updatedCinema.Nome + " atualizado com sucesso", "success");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CinemaExists(cinema.Id))
                        return CinemaNotFound();
                    else
                        throw;
                }
            }

            return View(cinema);
        }

        public bool CinemaExists(int id) =>
            _context.Cinema.Any(c => c.Id == id);

        // GET /Cinemas/Delete/GUID
        public async Task<IActionResult> Delete([FromRoute(Name = "id")]Guid? publicId)
        {
            if (publicId == null)
                return PublicIdRequired();

            var cinema = await _context.Cinema.Where(c => c.PublicId == publicId).Select(c => new Cinema
            {
                PublicId = c.PublicId,
                Nome = c.Nome,
                Cidade = c.Cidade,
                UnidadeFederativa = c.UnidadeFederativa,
                Telefone = c.Telefone,
                QuantidadeCartazes = c.Cartazes.Count(),
            }).FirstOrDefaultAsync();

            if (cinema == null)
                return CinemaNotFound();

            return View(cinema);
        }

        // POST /Cinemas/Delete/GUID
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(Guid publicId)
        {
            var cinema = await _context.Cinema.FirstOrDefaultAsync(c => c.PublicId == publicId);

            if (cinema == null)
                return NotFound();

            if(cinema.Cartazes.Count > 0)
            {
                SetMessage("Não é possível remover um cinema que possui algum cartaz", "warning");
                return RedirectToAction("Details", "Cinemas", new {id = cinema.PublicId});
            }    


            _context.Remove(cinema);
            await _context.SaveChangesAsync();
            await CinemasViewModel.UpdateCidades(_context);
            SetMessage("Cinema " + cinema.Nome + " removido com sucesso", "success");

            return RedirectToAction(nameof(Index));
        }

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
        private IActionResult CinemaNotFound()
        {
            SetMessage("Cinema não existe", "danger");
            return RedirectToActionPermanent(nameof(Index));
        }
    }
}
