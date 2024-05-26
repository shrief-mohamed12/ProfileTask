using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileTask.Models;
using ProfileTask.ViewModel;

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
                .Include(x=>x.educations)
                .Include(x=>x.otherExperience)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            List<OtherTable> OtherTable = await _context.otherTables.ToListAsync();
            ViewBag.OtherTable = OtherTable;
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
        public async Task<IActionResult> EditAbout(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeProfile = await _context.Employees.FindAsync(id);
            if (employeeProfile == null)
            {
                return NotFound();
            }

            return View(employeeProfile);
        }

        // POST: EmployeeProfiles/EditAbout/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAbout(int id, [Bind("Id,about")] Employee employeeProfile)
        {
            if (id != employeeProfile.Id)
            {
                return NotFound();
            }

            var employeeToUpdate = await _context.Employees.FindAsync(id);
            if (employeeToUpdate == null)
            {
                return NotFound();
            }
                try
                {
                    employeeToUpdate.about = employeeProfile.about;
                    _context.Update(employeeToUpdate);
                    await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), "Employee", new { id = employeeProfile.Id });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employeeProfile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                    return View(employeeProfile);
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
            var viewModel = new EditEmployeeViewModel
            {
                Id = employee.Id,
                employeeName = employee.employeeName,
                employeeJop = employee.employeeJop,
                backgroundPicturePath = employee.backgroundPicturePath,
                employeePicturePath = employee.employeePicturePath
            };

            return View(viewModel);
        }
        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditEmployeeViewModel viewModel)
        {
            var employee = await _context.Employees.FindAsync(viewModel.Id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.employeeName = viewModel.employeeName;
            employee.employeeJop = viewModel.employeeJop;

            // Handle Background Picture upload
            if (viewModel.backgroundPicture != null)
            {
                var backgroundFilePath = Path.Combine("wwwroot/uploads", viewModel.backgroundPicture.FileName);
                using (var stream = new FileStream(backgroundFilePath, FileMode.Create))
                {
                    await viewModel.backgroundPicture.CopyToAsync(stream);
                }
                employee.backgroundPicturePath = "/uploads/" + viewModel.backgroundPicture.FileName;
            }
            else
            {
                employee.backgroundPicturePath = employee.backgroundPicturePath;
            }

            // Handle Employee Picture upload
            if (viewModel.employeePicture != null)
            {
                var employeeFilePath = Path.Combine("wwwroot/uploads", viewModel.employeePicture.FileName);
                using (var stream = new FileStream(employeeFilePath, FileMode.Create))
                {
                    await viewModel.employeePicture.CopyToAsync(stream);
                }
                employee.employeePicturePath = "/uploads/" + viewModel.employeePicture.FileName;
            }
            else
            {
                employee.employeePicturePath= employee.employeePicturePath;
            }

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), "Employee", new { id = viewModel.Id });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Employees.Any(e => e.Id == viewModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    return View(viewModel);
                }
            }

        }
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
