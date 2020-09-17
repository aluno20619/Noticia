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
    public class NIsController : Controller
    {
        private readonly NoticiaDbContext _context;

        public NIsController(NoticiaDbContext context)
        {
            _context = context;
        }

        // GET: NIs
        public async Task<IActionResult> Index()
        {
            var noticiaDbContext = _context.NI.Include(n => n.Imagens).Include(n => n.Noticias);
            return View(await noticiaDbContext.ToListAsync());
        }

        // GET: NIs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nI = await _context.NI
                .Include(n => n.Imagens)
                .Include(n => n.Noticias)
                .FirstOrDefaultAsync(m => m.Imagensid == id);
            if (nI == null)
            {
                return NotFound();
            }

            return View(nI);
        }

        // GET: NIs/Create
        public IActionResult Create()
        {
            ViewData["Imagensid"] = new SelectList(_context.Imagens, "Id", "Id");
            ViewData["Noticiasid"] = new SelectList(_context.Noticias, "Id", "Corpo");
            return View();
        }

        // POST: NIs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Noticiasid,Imagensid")] NI nI)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nI);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Imagensid"] = new SelectList(_context.Imagens, "Id", "Id", nI.Imagensid);
            ViewData["Noticiasid"] = new SelectList(_context.Noticias, "Id", "Corpo", nI.Noticiasid);
            return View(nI);
        }

        // GET: NIs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nI = await _context.NI.FindAsync(id);
            if (nI == null)
            {
                return NotFound();
            }
            ViewData["Imagensid"] = new SelectList(_context.Imagens, "Id", "Id", nI.Imagensid);
            ViewData["Noticiasid"] = new SelectList(_context.Noticias, "Id", "Corpo", nI.Noticiasid);
            return View(nI);
        }

        // POST: NIs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Noticiasid,Imagensid")] NI nI)
        {
            if (id != nI.Imagensid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nI);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NIExists(nI.Imagensid))
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
            ViewData["Imagensid"] = new SelectList(_context.Imagens, "Id", "Id", nI.Imagensid);
            ViewData["Noticiasid"] = new SelectList(_context.Noticias, "Id", "Corpo", nI.Noticiasid);
            return View(nI);
        }

        // GET: NIs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nI = await _context.NI
                .Include(n => n.Imagens)
                .Include(n => n.Noticias)
                .FirstOrDefaultAsync(m => m.Imagensid == id);
            if (nI == null)
            {
                return NotFound();
            }

            return View(nI);
        }

        // POST: NIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nI = await _context.NI.FindAsync(id);
            _context.NI.Remove(nI);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NIExists(int id)
        {
            return _context.NI.Any(e => e.Imagensid == id);
        }
    }
}
