using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProfileTask.Models;
using System.IO;
using ProfileTask.ViewModel;

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

        public async Task<IActionResult> ListByID(int Id )
        {
            var result = await _context.backgrounds.Include(b => b.Employee).Where(x=>x.EmployeeId==Id).ToListAsync();
            return View(result);
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
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,Title,Description,OrgnizeName,Picture,dateFrom,dateTo")] Background background)
        {
            try
            {
                    if (background.Picture != null)
                    {
                        var backgroundPicturePath = Path.Combine("wwwroot/uploads", background.Picture.FileName);
                        using (var stream = new FileStream(backgroundPicturePath, FileMode.Create))
                        {
                            await background.Picture.CopyToAsync(stream);
                        }
                        background.BackgroundPicture = backgroundPicturePath.Replace("wwwroot", "");
                    }

                    await _context.AddAsync(background);
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

        // GET: Backgrounds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {


            var background = await _context.backgrounds.Where(x => x.EmployeeId == id || x.Id == id).FirstOrDefaultAsync(); 
            if (background == null)
            {
                return NotFound();
            }
            var viewModel = new EditBackgroundViewModel
            {
                Id= background.Id,
                EmployeeId= background.EmployeeId ,
                Title = background.Title,
                Description= background.Description,
                Picture = background.Picture,
                OrgnizeName= background.OrgnizeName,    
                BackgroundPicture=background.BackgroundPicture,
                dateFrom=background.dateFrom,
                dateTo=background.dateTo,
            };
            return View(viewModel);

        }

        // POST: Backgrounds/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditBackgroundViewModel viewModel)
        {

            var background = await _context.backgrounds
                .FirstOrDefaultAsync(b => b.EmployeeId == viewModel.EmployeeId && b.Id==viewModel.Id );
            if (background == null)
            {
                return NotFound();
            }
            background.Title = viewModel.Title;
            background.Description = viewModel.Description;
            background.OrgnizeName = viewModel.OrgnizeName;
            try
            {
                
                // Handle file upload
                if (viewModel.Picture != null)
                {
                var filePath = Path.Combine("wwwroot/uploads", viewModel.Picture.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await viewModel.Picture.CopyToAsync(stream);
                }
                background.BackgroundPicture = "/uploads/" + viewModel.Picture.FileName;
                }
                else
                {
                background.BackgroundPicture = background.BackgroundPicture;
                }
                 _context.Update(background);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), "Employee", new { id = viewModel.EmployeeId });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.backgrounds.Any(b => b.EmployeeId == viewModel.EmployeeId))
                {
                    return NotFound();
                }
                else
                {
                    return View(background);
                }
            }

        }
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
