using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProfileTask.Models;
using System.IO;

namespace ProfileTask.Controllers
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
            var result = await _context.backgrounds.Include(b => b.Employee).ToListAsync();
            return View( result);
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "employeeName");
            return View();
        }

        // POST: Backgrounds/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,Title,Description,OrgnizeName,Picture,BackgroundPicture,dateFrom,dateTo")] Background background)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    if (background.Picture != null)
                    {
                        var backgroundPicturePath = Path.Combine("wwwroot/uploads", background.Picture.FileName);
                        using (var stream = new FileStream(backgroundPicturePath, FileMode.Create))
                        {
                            await background.Picture.CopyToAsync(stream);
                        }
                        // Save the relative path to the database
                        background.BackgroundPicture = backgroundPicturePath.Replace("wwwroot", "");
                    }

                    await _context.AddAsync(background);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "employeeName", background.EmployeeId);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BackgroundExists(background.Id))
                {
                    return NotFound();
                }
                else
                {
                    return View(background);
                }
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "employeeName", background.EmployeeId);
            return View(background);
        }

        // POST: Backgrounds/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,Title,Description,OrgnizeName,Picture,BackgroundPicture,dateFrom,dateTo")] Background background)
        {
            if (id != background.Id)
            {
                return NotFound();
            }
                try
                {
                    var existingBackGround = await _context.backgrounds.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
                    if (existingBackGround == null)
                    {
                        return NotFound();
                    }
                    if (background.Picture != null)
                    {
                        var backgroundPicturePath = Path.Combine("wwwroot/uploads", background.Picture.FileName);
                        using (var stream = new FileStream(backgroundPicturePath, FileMode.Create))
                        {
                            await background.Picture.CopyToAsync(stream);
                        }
                        // Save the relative path to the database
                        background.BackgroundPicture = backgroundPicturePath.Replace("wwwroot", "");
                    }
                    else
                    {
                        // Retain the old background picture path if no new file is provided
                        background.BackgroundPicture = existingBackGround.BackgroundPicture;
                    }

                    _context.Update(background);
                    await _context.SaveChangesAsync();
                    ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "employeeName", background.EmployeeId);
                    return RedirectToAction(nameof(Index));

                 }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BackgroundExists(background.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                    return View(background);
                    }
                }
        }

        // GET: Backgrounds/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
