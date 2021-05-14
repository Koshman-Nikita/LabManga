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
    public class ReadersMangasController : Controller
    {
        private readonly DBLibraryContext _context;

        public ReadersMangasController(DBLibraryContext context)
        {
            _context = context;
        }

        // GET: ReadersMangas
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Readers", "Index");
            ViewBag.ReaderId = id;
            ViewBag.ReaderName = name;
            var mangasbyReader = _context.ReadersMangas.Where(a => a.ReaderId == id).Include(a => a.Reader).Include(a => a.Manga).Include(a=> a.Status);
            return View(await mangasbyReader.ToListAsync());
        }

        public IActionResult Return()
        {
            return RedirectToAction("Index", "Readers");
        }

        // GET: ReadersMangas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var readersManga = await _context.ReadersMangas
                .Include(r => r.Manga)
                .Include(r => r.Reader)
                .Include(r => r.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (readersManga == null)
            {
                return NotFound();
            }

            return View(readersManga);
        }

        // GET: ReadersMangas/Create
        public IActionResult Create(int readerId)
        {
            ViewData["MangaId"] = new SelectList(_context.Mangas, "Id", "Name");
           //ViewData["ReaderId"] = new SelectList(_context.Readers, "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name");
            ViewBag.ReaderId = readerId;
            ViewBag.ReaderName = _context.Readers.Where(c => c.Id == readerId).FirstOrDefault().Name;
            return View();
        }

        // POST: ReadersMangas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int readerId, [Bind("Id,ReaderId,MangaId,StatusId,PlanReturn,FactReturn")] ReadersManga readersManga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(readersManga);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "ReadersMangas", new { id = readerId, name = _context.Readers.Where(c => c.Id == readerId).FirstOrDefault().Name });
            }
            ViewData["MangaId"] = new SelectList(_context.Mangas, "Id", "Name", readersManga.MangaId);
            //ViewData["ReaderId"] = new SelectList(_context.Readers, "Id", "Name", readersManga.ReaderId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", readersManga.StatusId);
            //return View(readersManga);
            return RedirectToAction("Index", "ReadersMangas", new { id = readerId, name = _context.Readers.Where(c => c.Id == readerId).FirstOrDefault().Name });
        }

        // GET: ReadersMangas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var readersManga = await _context.ReadersMangas.FindAsync(id);
            if (readersManga == null)
            {
                return NotFound();
            }
            ViewData["MangaId"] = new SelectList(_context.Mangas, "Id", "Name", readersManga.MangaId);
            ViewData["ReaderId"] = new SelectList(_context.Readers, "Id", "Name", readersManga.ReaderId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", readersManga.StatusId);
            return View(readersManga);
        }

        // POST: ReadersMangas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReaderId,MangaId,StatusId,PlanReturn,FactReturn")] ReadersManga readersManga)
        {
            if (id != readersManga.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(readersManga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReadersMangaExists(readersManga.Id))
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
            ViewData["MangaId"] = new SelectList(_context.Mangas, "Id", "Name", readersManga.MangaId);
            ViewData["ReaderId"] = new SelectList(_context.Readers, "Id", "Name", readersManga.ReaderId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", readersManga.StatusId);
            return View(readersManga);
        }

        // GET: ReadersMangas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var readersManga = await _context.ReadersMangas
                .Include(r => r.Manga)
                .Include(r => r.Reader)
                .Include(r => r.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (readersManga == null)
            {
                return NotFound();
            }

            return View(readersManga);
        }

        // POST: ReadersMangas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var readersManga = await _context.ReadersMangas.FindAsync(id);
            _context.ReadersMangas.Remove(readersManga);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReadersMangaExists(int id)
        {
            return _context.ReadersMangas.Any(e => e.Id == id);
        }
    }
}
