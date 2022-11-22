using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Infrastructure;
using ShoppingCart.Models.ViewModels;

namespace ShoppingCart.Controllers
{
    public class CartViewModelsController : Controller
    {
        private readonly DataContext _context;

        public CartViewModelsController(DataContext context)
        {
            _context = context;
        }

        // GET: CartViewModels
        public async Task<IActionResult> Index()
        {
            var user = await _context.Users.FirstAsync(u => u.UserName == User.Identity.Name);

            return View(await _context.OrderCarts.Where(p=> p.UserEmail== user.Email).
                  ToListAsync());
        }

        // GET: CartViewModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.OrderCarts == null)
            {
                return NotFound();
            }

            var cartViewModel = await _context.OrderCarts
                .FirstOrDefaultAsync(m => m.id == id);
            if (cartViewModel == null)
            {
                return NotFound();
            }
            cartViewModel.CartItems = _context.CartItems.Where(i => i.OrderId==id).ToList();

            return View(cartViewModel);
        }

        // GET: CartViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CartViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,UserEmail,UserPhone,UserAddress,GrandTotal,LocationIsDhaka,LocationOutsideDhaka,DeliveryLocation,OrderCompleted,DeliveryCharge")] CartViewModel cartViewModel)
        {
            if (ModelState.IsValid)
            {
                cartViewModel.id = Guid.NewGuid();
                _context.Add(cartViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cartViewModel);
        }

        // GET: CartViewModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.OrderCarts == null)
            {
                return NotFound();
            }

            var cartViewModel = await _context.OrderCarts.FindAsync(id);
            if (cartViewModel == null)
            {
                return NotFound();
            }
            return View(cartViewModel);
        }

        // POST: CartViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,UserEmail,UserPhone,UserAddress,GrandTotal,LocationIsDhaka,LocationOutsideDhaka,DeliveryLocation,OrderCompleted,DeliveryCharge")] CartViewModel cartViewModel)
        {
            if (id != cartViewModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartViewModelExists(cartViewModel.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cartViewModel);
        }

        // GET: CartViewModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.OrderCarts == null)
            {
                return NotFound();
            }

            var cartViewModel = await _context.OrderCarts
                .FirstOrDefaultAsync(m => m.id == id);
            if (cartViewModel == null)
            {
                return NotFound();
            }

            return View(cartViewModel);
        }

        // POST: CartViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.OrderCarts == null)
            {
                return Problem("Entity set 'DataContext.OrderCarts'  is null.");
            }
            var cartViewModel = await _context.OrderCarts.FindAsync(id);
            if (cartViewModel != null)
            {
                _context.OrderCarts.Remove(cartViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartViewModelExists(Guid id)
        {
          return _context.OrderCarts.Any(e => e.id == id);
        }
    }
}
