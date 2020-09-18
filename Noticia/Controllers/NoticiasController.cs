using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Noticia.Models;
using Noticia.Data;

namespace Noticia.Controllers
{
    public class NoticiasController : Controller
    {
        private readonly NoticiaDbContext _context;

        public NoticiasController(NoticiaDbContext context)
        {
            _context = context;
        }

        // GET: Noticias
        public async Task<IActionResult> Index()
        {
            var noticiaDbContext = _context.Noticias.Include(n => n.Utilizadoresid);
            return View(await noticiaDbContext.ToListAsync());
        }

        // GET: Noticias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var noticias = await _context.Noticias
                .Include(n => n.Utilizadoresid)
                .Include(v => v.ListaNI)
                .ThenInclude(a => a.Imagens)
                .Include(v => v.ListaNT)
                .ThenInclude(a => a.Topicos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noticias == null)
            {
                return RedirectToAction("Index");
            }

            return View(noticias);
        }

        // GET: Noticias/Create
        public IActionResult Create()
        {
            ViewData["UtilizadoresidFK"] = new SelectList(_context.Utilizadores, "Id", "Email");
            return View();
        }

        // POST: Noticias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Resumo,Corpo,Data_De_Publicacao,Visivel,UtilizadoresidFK")] Noticias noticias)
        {
            if (ModelState.IsValid)
            {
                _context.Add(noticias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UtilizadoresidFK"] = new SelectList(_context.Utilizadores, "Id", "Email", noticias.UtilizadoresidFK);
            return View(noticias);
        }

        // GET: Noticias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var noticias = await _context.Noticias.FindAsync(id);
            if (noticias == null)
            {
                return RedirectToAction("Index");
            }
            ViewData["UtilizadoresidFK"] = new SelectList(_context.Utilizadores, "Id", "Email", noticias.UtilizadoresidFK);
            return View(noticias);
        }

        // POST: Noticias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Resumo,Corpo,Data_De_Publicacao,Visivel,UtilizadoresidFK")] Noticias noticias)
        {
            if (id != noticias.Id)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noticias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoticiasExists(noticias.Id))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UtilizadoresidFK"] = new SelectList(_context.Utilizadores, "Id", "Email", noticias.UtilizadoresidFK);
            return View(noticias);
        }

        // GET: Noticias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var noticias = await _context.Noticias
                .Include(n => n.Utilizadoresid)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noticias == null)
            {
                return RedirectToAction("Index");
            }

            return View(noticias);
        }

        // POST: Noticias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var noticias = await _context.Noticias.FindAsync(id);
            _context.Noticias.Remove(noticias);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoticiasExists(int id)
        {
            return _context.Noticias.Any(e => e.Id == id);
        }
    }
}
