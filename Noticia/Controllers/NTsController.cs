using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Noticia.Models;
using Noticia.Data;
using Microsoft.AspNetCore.Authorization;

namespace Noticia.Controllers
{
    public class NTsController : Controller
    {
        private readonly NoticiaDbContext db;

        public NTsController(NoticiaDbContext context)
        {
            db = context;
        }

        // GET: NTs
        [Authorize(Policy = "writepolicy")]
        public async Task<IActionResult> Index()
        {
            var noticiaDbContext = db.NT.Include(n => n.Noticias).Include(n => n.Topicos);
            return View(await noticiaDbContext.ToListAsync());
        }

        // GET: NTs/Details/5
        [Authorize(Policy = "writepolicy")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var nT = await db.NT
                .Include(n => n.Noticias)
                .Include(n => n.Topicos)
                .FirstOrDefaultAsync(m => m.Topicosid == id);
            if (nT == null)
            {
                return RedirectToAction("Index");
            }

            return View(nT);
        }

        // GET: NTs/Create
        [Authorize(Policy = "writepolicy")]
        public IActionResult Create()
        {
            ViewData["Noticiasid"] = new SelectList(db.Noticias, "Id", "Id");
            ViewData["Topicosid"] = new SelectList(db.Topicos, "Id", "Id");
            
            return View();
        }

        // POST: NTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Policy = "writepolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Noticiasid,Topicosid")] NT nT)
        {
            
            if (ModelState.IsValid)
            {
                db.Add(nT);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Noticiasid"] = new SelectList(db.Noticias, "Id", "Id", nT.Noticiasid);
            ViewData["Topicosid"] = new SelectList(db.Topicos, "Id", "Id", nT.Topicosid);
            return View(nT);
        }

        // GET: NTs/Edit/5
        [Authorize(Policy = "writepolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var nT = await db.NT.FindAsync(id);
            if (nT == null)
            {
                return RedirectToAction("Index");
            }
            ViewData["Noticiasid"] = new SelectList(db.Noticias, "Id", "Id", nT.Noticiasid);
            ViewData["Topicosid"] = new SelectList(db.Topicos, "Id", "Id", nT.Topicosid);
            return View(nT);
        }

        // POST: NTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Policy = "writepolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Noticiasid,Topicosid")] NT nT)
        {
            if (id != nT.Topicosid)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(nT);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NTExists(nT.Topicosid))
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
            ViewData["Noticiasid"] = new SelectList(db.Noticias, "Id", "Id", nT.Noticiasid);
            ViewData["Topicosid"] = new SelectList(db.Topicos, "Id", "Id", nT.Topicosid);
            return View(nT);
        }

        // GET: NTs/Delete/5
        [Authorize(Policy = "writepolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var nT = await db.NT
                .Include(n => n.Noticias)
                .Include(n => n.Topicos)
                .FirstOrDefaultAsync(m => m.Topicosid == id);
            if (nT == null)
            {
                return RedirectToAction("Index");
            }

            return View(nT);
        }

        // POST: NTs/Delete/5
        [Authorize(Policy = "writepolicy")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nT = await db.NT.FindAsync(id);
            db.NT.Remove(nT);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NTExists(int id)
        {
            return db.NT.Any(e => e.Topicosid == id);
        }
    }
}
