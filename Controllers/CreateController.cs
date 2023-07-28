using Corporate_Solutions_test.Data;
using Corporate_Solutions_test.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Corporate_Solutions_test.Controllers
{
    //[Authorize(Roles = "admin")]
    [Authorize]
    public class CreateController : Controller
    {
        private readonly ProductContext _context;

        public CreateController(ProductContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Name,Count,Price")] ProductModel product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(product);
        }
    }
}
