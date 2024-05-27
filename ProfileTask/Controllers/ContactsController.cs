using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProfileTask.Models;
using ProfileTask.ViewModel;

public class ContactsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ContactsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Contacts
    public async Task<IActionResult> Index()
    {
        var contacts = await _context.contacts.Include(c => c.Employee).ToListAsync();
        return View(contacts);
    }
    public async Task<IActionResult> ListByID(int Id)
    {
        var result = await _context.contacts.Include(b => b.Employee).Where(x => x.EmployeeId == Id).ToListAsync();
        return View(result);
    }

    // GET: Contacts/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var contacts = await _context.contacts
            .Include(c => c.Employee)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (contacts == null)
        {
            return NotFound();
        }

        return View(contacts);
    }

    // GET: Contacts/Create
    public IActionResult Create()
    {
        ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "employeeName");
        return View();
    }

    // POST: Contacts/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,EmployeeId,Phone,Address,Email")] Contacts contacts)
    {

        try
        {
            await _context.AddAsync(contacts);
            await _context.SaveChangesAsync();
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "employeeName", contacts.EmployeeId);
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ContactsExists(contacts.Id))
            {
                return NotFound();
            }
            else
            {
                return View(contacts);
            }
        }
    }

    // GET: Contacts/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {


        var contacts = await _context.contacts.Where(x => x.EmployeeId == id || x.Id == id).FirstOrDefaultAsync();

        if (contacts == null)
        {
            return NotFound();
        }
        var viewModel = new EditContactViewModel
        {
            EmployeeId = contacts.EmployeeId,
            Phone = contacts.Phone,
            Address = contacts.Address,
            Email = contacts.Email
        };

        return View(viewModel);
    }

    // POST: Contacts/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditContactViewModel viewModel)
    {
        try
        {

           var contact = await _context.contacts
           .FirstOrDefaultAsync(c => c.EmployeeId == viewModel.EmployeeId);
             if (contact == null)
             {
                 return NotFound();
             }

            contact.Phone = viewModel.Phone;
            contact.Address = viewModel.Address;
            contact.Email = viewModel.Email;

            _context.Update(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), "Employee", new { id = viewModel.EmployeeId });

        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.contacts.Any(c => c.EmployeeId == viewModel.EmployeeId))
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

        var contacts = await _context.contacts
            .Include(c => c.Employee)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (contacts == null)
        {
            return NotFound();
        }

        return View(contacts);
    }

    // POST: Contacts/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var contacts = await _context.contacts.FindAsync(id);
        _context.contacts.Remove(contacts);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ContactsExists(int id)
    {
        return _context.contacts.Any(e => e.Id == id);
    }
}
