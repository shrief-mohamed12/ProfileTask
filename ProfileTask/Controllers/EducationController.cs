
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Mvc.Rendering;
using ProfileTask.Models;

namespace ProfileTask.Controllers
{
    public class EducationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EducationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Educations
        public async Task<IActionResult> Index()
        {
            var result = await _context.educations.Include(e => e.Employee).ToListAsync();
            return View( result);
        }

        // GET: Educations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var education = await _context.educations
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (education == null)
            {
                return NotFound();
            }

            return View(education);
        }

        // GET: Educations/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "employeeName");
            return View();
        }

        // POST: Educations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,Title,Description,Picture,EducPicturePath,dateFrom,dateTo")] Education education)
        {
            try
            {
                if (education.Picture != null)
                {
                    var backgroundPicturePath = Path.Combine("wwwroot/uploads", education.Picture.FileName);
                    using (var stream = new FileStream(backgroundPicturePath, FileMode.Create))
                    {
                        await education.Picture.CopyToAsync(stream);
                    }
                    // Save the relative path to the database
                    education.EducPicturePath = backgroundPicturePath.Replace("wwwroot", "");
                }

                await _context.AddAsync(education);
                await _context.SaveChangesAsync();
                ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "employeeName", education.EmployeeId);
                return RedirectToAction(nameof(Index));

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EducationExists(education.Id))
                {
                    return NotFound();
                }
                else
                {
                    return View(education);
                }
            }
        }

        // GET: Educations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var education = await _context.educations.FindAsync(id);
            if (education == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "employeeName", education.EmployeeId);
            return View(education);
        }

        // POST: Educations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,Title,Description,Picture,EducPicturePath,dateFrom,dateTo")] Education education)
        {
            if (id != education.Id)
            {
                return NotFound();
            }
                try
                {
                    var existingEdicataion = await _context.educations.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
                    if (existingEdicataion == null)
                    {
                        return NotFound();
                    }
                    if (education.Picture != null)
                    {
                        var backgroundPicturePath = Path.Combine("wwwroot/uploads", education.Picture.FileName);
                        using (var stream = new FileStream(backgroundPicturePath, FileMode.Create))
                        {
                            await education.Picture.CopyToAsync(stream);
                        }
                        // Save the relative path to the database
                        education.EducPicturePath = backgroundPicturePath.Replace("wwwroot", "");
                    }
                    else
                    {
                        // Retain the old background picture path if no new file is provided
                        education.EducPicturePath = existingEdicataion.EducPicturePath;
                    }

                     _context.Update(education);
                     ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "employeeName", education.EmployeeId);
                    await _context.SaveChangesAsync();
                     return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationExists(education.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                    return View(education);
                    }
                 }
        }

        // GET: Educations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var education = await _context.educations
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (education == null)
            {
                return NotFound();
            }

            return View(education);
        }

        // POST: Educations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var education = await _context.educations.FindAsync(id);
            _context.educations.Remove(education);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationExists(int id)
        {
            return _context.educations.Any(e => e.Id == id);
        }
    }
}
