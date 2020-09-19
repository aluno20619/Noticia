using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Noticia.Models;
using Noticia.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Noticia.Controllers
{
    [Authorize]
    public class ImagensController : Controller
    {
        private readonly NoticiaDbContext _context;
        private readonly IWebHostEnvironment _ambiente;

        public ImagensController(NoticiaDbContext context,IWebHostEnvironment ambiente)
        {

            _context = context;
            _ambiente = ambiente;
        }
        [Authorize(Policy = "readpolicy")]
        // GET: Imagens
        public async Task<IActionResult> Index()
        {
            return View(await _context.Imagens.ToListAsync());
        }
        [Authorize(Policy = "readpolicy")]
        // GET: Imagens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var imagens = await _context.Imagens
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (imagens == null)
            {
                return RedirectToAction("Index");
            }



            return View(imagens);
        }
        [Authorize(Policy = "writepolicy")]
        // GET: Imagens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Imagens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Policy = "writepolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Legenda")] Imagens imagens, IFormFile foto)
        {
            //_____________________________________________________________________________________________
            // vars. auxiliares
            string caminhoCompleto = "";
            bool haImagem = false;

            // será que há ficheiro?
            if (foto == null)
            {
                // não há ficheiro!
                // o que vai ser feito?
                //   - devolver o controlo para a View, informando que é necessário escolher uma fotografia
                //       ModelState.AddModelError("", "Não se esqueça de adicionar uma fotografia do Veterinário");
                //       return View(veterinario);
                //   - adicionar uma fotografia 'por defeito'
                imagens.Nome = "no_image-300x245.jpg";
            }
            else
            {
                // há ficheiro.
                // será que é uma imagem?
                if (foto.ContentType == "image/jpeg" ||
                   foto.ContentType == "image/png")
                {
                    // temos imagem. Ótimo!
                    // temos de gerar um nome para o ficheiro
                    Guid g;
                    g = Guid.NewGuid();
                    // identificar a Extensão do ficheiro
                    string extensao = Path.GetExtension(foto.FileName).ToLower();
                    // nome do ficheiro
                    string nome = g.ToString() + extensao;
                    // preparar o ficheiro para ser guardado, mas não o vamos guardar já...
                    // precisamos de identificar o caminho onde o ficheiro vai ser guardado
                    caminhoCompleto = Path.Combine(_ambiente.WebRootPath, "Imagens", nome);
                    // associar o nome da fotografia ao Veterinário 
                    imagens.Nome = nome;
                    // assinalar que existe imagem
                    haImagem = true;
                }
                else
                {
                   
                    imagens.Nome = "no_image-300x245.jpg";
                }
                //_____________________________________________________________________________________________

            }
            if (ModelState.IsValid)
            {
                _context.Add(imagens);
                await _context.SaveChangesAsync();
                //_____________________________________________________________________________________________
                if (haImagem)
                {
                    using var stream = new FileStream(caminhoCompleto, FileMode.Create);
                    await foto.CopyToAsync(stream);
                }
                // redireciona o utilizador para a View Index
                return RedirectToAction(nameof(Index));
                //_____________________________________________________________________________________________
            }
            return View(imagens);
        }
        [Authorize(Policy = "writepolicy")]
        // GET: Imagens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var imagens = await _context.Imagens.FindAsync(id);
            if (imagens == null)
            {
                return RedirectToAction("Index");
            }
            return View(imagens);
        }

        // POST: Imagens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Policy = "writepolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Legenda")] Imagens imagens)
        {
            if (id != imagens.Id)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imagens);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImagensExists(imagens.Id))
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
            return View(imagens);
        }



        // GET: Imagens/Delete/5
        [Authorize(Policy = "writepolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var imagens = await _context.Imagens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imagens == null)
            {
                return RedirectToAction("Index");
            }

            return View(imagens);
        }

        // POST: Imagens/Delete/5
        [Authorize(Policy = "writepolicy")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imagens = await _context.Imagens.FindAsync(id);
            _context.Imagens.Remove(imagens);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImagensExists(int id)
        {
            return _context.Imagens.Any(e => e.Id == id);
        }
    }
}
