using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileTask.Models;

namespace ProfileTask.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var result = await _context.Employees.ToListAsync();
            return View(result);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(x=>x.Licenses)
                .Include(x=>x.notes)
                .Include(x=>x.backgrounds)
                .Include(x=>x.contacts)
                .Include(x=>x.otherExperience)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,employeeName,employeeJop,backgroundPicture,employeePicture,about")] Employee employee)
        {
            try
            {
                if (employee.backgroundPicture != null)
                {
                    var backgroundPicturePath = Path.Combine("wwwroot/uploads", employee.backgroundPicture.FileName);
                    using (var stream = new FileStream(backgroundPicturePath, FileMode.Create))
                    {
                        await employee.backgroundPicture.CopyToAsync(stream);
                    }
                    // Save the relative path to the database
                    employee.backgroundPicturePath = backgroundPicturePath.Replace("wwwroot", "");
                }

                if (employee.employeePicture != null)
                {
                    var employeePicturePath = Path.Combine("wwwroot/uploads", employee.employeePicture.FileName);
                    using (var stream = new FileStream(employeePicturePath, FileMode.Create))
                    {
                        await employee.employeePicture.CopyToAsync(stream);
                    }
                    // Save the relative path to the database
                    employee.employeePicturePath = employeePicturePath.Replace("wwwroot", "");
                }

                await _context.AddAsync(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.Id))
                {
                    return NotFound();
                }
                else
                {
                    return View(employee);
                }
            }

        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Edit(int id, [Bind("Id,employeeName,employeeJop,backgroundPicture,employeePicture,about")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }
                try
                {
                    var existingEmployee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
                    if (existingEmployee == null)
                    {
                        return NotFound();
                    }

                    if (employee.backgroundPicture != null)
                    {
                        var backgroundPicturePath = Path.Combine("wwwroot/uploads", employee.backgroundPicture.FileName);
                        using (var stream = new FileStream(backgroundPicturePath, FileMode.Create))
                        {
                            await employee.backgroundPicture.CopyToAsync(stream);
                        }
                        // Save the relative path to the database
                        employee.backgroundPicturePath = backgroundPicturePath.Replace("wwwroot", "");
                    }
                    else
                    {
                        // Retain the old background picture path if no new file is provided
                        employee.backgroundPicturePath = existingEmployee.backgroundPicturePath;
                    }

                    if (employee.employeePicture != null)
                    {
                        var employeePicturePath = Path.Combine("wwwroot/uploads", employee.employeePicture.FileName);
                        using (var stream = new FileStream(employeePicturePath, FileMode.Create))
                        {
                            await employee.employeePicture.CopyToAsync(stream);
                        }
                        // Save the relative path to the database
                        employee.employeePicturePath = employeePicturePath.Replace("wwwroot", "");
                    }
                    else
                    {
                        // Retain the old employee picture path if no new file is provided
                        employee.employeePicturePath = existingEmployee.employeePicturePath;
                    }

                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                    return View(employee);
                    }
                }
            
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }

}
