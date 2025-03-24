using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.iamnikitakostin.Data;
using ProductManagementSystem.iamnikitakostin.Models;

namespace ProductManagementSystem.iamnikitakostin.Controllers
{
    [Authorize(Roles = "Admin,Manager,User")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ApplicationDbContext context, ILogger<ProductsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // GET: Products/Details/5
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

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,IsActive,Price")] Product product)
        {
            product.DateAdded = DateTime.Now;
            product.DateUpdated = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Product with the Name of {product.Name} has been added");

                return RedirectToAction(nameof(Index));
            }
            _logger.LogError($"The model for product with the Name of {product.Name} has been corruptc");
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                _logger.LogError($"The product with the Id of {id} has not been found");

                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                _logger.LogError($"The product with the Id of {id} has not been found");

                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,DateAdded,IsActive,Price")] Product product)
        {
            if (id != product.Id)
            {
                _logger.LogError($"The product with the Id of {id} has not been found");

                return NotFound();
            }

            product.DateUpdated = DateTime.Now;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    _logger.LogError($"The product with the Id of {id} has been updated.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        _logger.LogError($"The product with the Id of {id} has not been found");

                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError("A critical error has been catched. For more info review the app.");
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogError($"The product with the Id of {id} has not been found");

                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                _logger.LogError($"The product with the Id of {id} has not been found");

                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _logger.LogError($"The product with the Id of {id} has not been found");

                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();

            _logger.LogError($"The product with the Id of {id} has been updated");

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            _logger.LogError($"The product with the Id of {id} has been found");
            
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
