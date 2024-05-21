using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileTask.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    public class BackgroundController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BackgroundController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Backgrounds
        public async Task<IActionResult> Index()
        {
            var backgrounds = await _context.backgrounds.Include(b => b.Employee).ToListAsync();
            return View(backgrounds);
        }

        // GET: Backgrounds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var background = await _context.backgrounds
                .Include(b => b.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (background == null)
            {
                return NotFound();
            }

            return View(background);
        }

        // GET: Backgrounds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Backgrounds/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,Title,Description,OrgnizeName,Picture,dateFrom,dateTo")] Background background)
        {
            if (ModelState.IsValid)
            {
                if (background.Picture != null)
                {
                    // Handle file upload
                    var filePath = Path.Combine("wwwroot/uploads", background.Picture.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await background.Picture.CopyToAsync(stream);
                    }

                    background.BackgroundPicture = "/uploads/" + background.Picture.FileName;
                }

                _context.Add(background);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(background);
        }

        // GET: Backgrounds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var background = await _context.backgrounds.FindAsync(id);
            if (background == null)
            {
                return NotFound();
            }
            return View(background);
        }

        // POST: Backgrounds/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,Title,Description,OrgnizeName,Picture,dateFrom,dateTo")] Background background)
        {
            if (id != background.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (background.Picture != null)
                    {
                        // Handle file upload
                        var filePath = Path.Combine("wwwroot/uploads", background.Picture.FileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await background.Picture.CopyToAsync(stream);
                        }

                        background.BackgroundPicture = "/uploads/" + background.Picture.FileName;
                    }

                    _context.Update(background);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BackgroundExists(background.Id))
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
            return View(background);
        }

        // GET: Backgrounds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var background = await _context.backgrounds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (background == null)
            {
                return NotFound();
            }

            return View(background);
        }

        // POST: Backgrounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var background = await _context.backgrounds.FindAsync(id);
            _context.backgrounds.Remove(background);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BackgroundExists(int id)
        {
            return _context.backgrounds.Any(e => e.Id == id);
        }
    }
}
