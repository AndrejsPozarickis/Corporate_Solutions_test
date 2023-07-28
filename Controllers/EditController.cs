using Corporate_Solutions_test.Data;
using Corporate_Solutions_test.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Corporate_Solutions_test.Controllers
{
    [Authorize]
    public class EditController : Controller
    {
        private readonly ProductContext _context;

        public EditController(ProductContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(
            [Bind("Id,Name,Count,Price")] ProductModel product)
        {
            var productToUpdate = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == product.Id);

            if(productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.Count = product.Count;
                productToUpdate.Price = product.Price;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
                
            return RedirectToAction("Index", "Home");
        }
    }
}
