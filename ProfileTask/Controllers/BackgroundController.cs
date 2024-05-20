using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProfileTask.Controllers
{
    public class BackgroundController : Controller
    {
        // GET: BackgroundController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BackgroundController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BackgroundController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BackgroundController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BackgroundController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BackgroundController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BackgroundController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BackgroundController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
