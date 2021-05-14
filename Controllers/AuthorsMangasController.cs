using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabManga;

namespace LabManga.Controllers
{
    public class AuthorsMangasController : Controller
    {
        private readonly DBLibraryContext _context;

        public AuthorsMangasController(DBLibraryContext context)
        {
            _context = context;
        }

        // GET: AuthorsMangas
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Authors", "Index");
            ViewBag.AuthorId = id;
            ViewBag.AuthorName = name;
            var mangasbyAuthor = _context.AuthorsMangas.Where(a => a.AuthorId == id).Include(a => a.Author).Include(a => a.Manga);
            return View(await mangasbyAuthor.ToListAsync());
        }

        public IActionResult Return()
        {
            return RedirectToAction("Index", "Authors");
        }

        // GET: AuthorsMangas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorsManga = await _context.AuthorsMangas
                .Include(a => a.Author)
                .Include(a => a.Manga)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorsManga == null)
            {
                return NotFound();
            }

            return View(authorsManga);
        }

        // GET: AuthorsMangas/Create
        public IActionResult Create(int authorId)
        {
            //ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name");
            ViewData["MangaId"] = new SelectList(_context.Mangas, "Id", "Name");
            ViewBag.AuthorId = authorId;
            ViewBag.AuthorName = _context.Authors.Where(c => c.Id == authorId).FirstOrDefault().Name;
            return View();
        }

        // POST: AuthorsMangas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int authorId ,[Bind("Id,MangaId,AuthorId")] AuthorsManga authorsManga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(authorsManga);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "AuthorsMangas", new { id = authorId, name = _context.Authors.Where(c => c.Id == authorId).FirstOrDefault().Name });
            }
            //ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name", authorsManga.AuthorId);
            ViewData["MangaId"] = new SelectList(_context.Mangas, "Id", "Name", authorsManga.MangaId);
            //return View(authorsManga);
            return RedirectToAction("Index", "AuthorsMangas", new { id = authorId, name = _context.Authors.Where(c => c.Id == authorId).FirstOrDefault().Name });
        }

        // GET: AuthorsMangas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorsManga = await _context.AuthorsMangas.FindAsync(id);
            if (authorsManga == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name", authorsManga.AuthorId);
            ViewData["MangaId"] = new SelectList(_context.Mangas, "Id", "Name", authorsManga.MangaId);
            return View(authorsManga);
        }

        // POST: AuthorsMangas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MangaId,AuthorId")] AuthorsManga authorsManga)
        {
            if (id != authorsManga.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorsManga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorsMangaExists(authorsManga.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name", authorsManga.AuthorId);
            ViewData["MangaId"] = new SelectList(_context.Mangas, "Id", "Name", authorsManga.MangaId);
            return View(authorsManga);
        }

        // GET: AuthorsMangas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorsManga = await _context.AuthorsMangas
                .Include(a => a.Author)
                .Include(a => a.Manga)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorsManga == null)
            {
                return NotFound();
            }

            return View(authorsManga);
        }

        // POST: AuthorsMangas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authorsManga = await _context.AuthorsMangas.FindAsync(id);
            _context.AuthorsMangas.Remove(authorsManga);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorsMangaExists(int id)
        {
            return _context.AuthorsMangas.Any(e => e.Id == id);
        }
    }
}
