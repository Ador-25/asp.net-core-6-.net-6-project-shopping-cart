using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Infrastructure;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
        public class ProductsController : Controller
        {
                private readonly DataContext _context;

                public ProductsController(DataContext context)
                {
                        _context = context;
                }

                public async Task<IActionResult> Index(string categorySlug = "", int p = 1)
                {
                        int pageSize = 9;
                        ViewBag.PageNumber = p;
                        ViewBag.PageRange = pageSize;
                        ViewBag.CategorySlug = categorySlug;

                        if (categorySlug == "")
                        {
                                ViewBag.TotalPages = (int)Math.Ceiling((decimal)_context.Products.Count() / pageSize);

                                return View(await _context.Products.OrderByDescending(p => p.Id).Skip((p - 1) * pageSize).Take(pageSize).ToListAsync());
                        }

                        Category category = await _context.Categories.Where(c => c.Slug == categorySlug).FirstOrDefaultAsync();
                        if (category == null) return RedirectToAction("Index");

                        var productsByCategory = _context.Products.Where(p => p.CategoryId == category.Id);
                        ViewBag.TotalPages = (int)Math.Ceiling((decimal)productsByCategory.Count() / pageSize);
            return View(await productsByCategory.OrderByDescending(p => p.Id).Skip((p - 1) * pageSize).Take(pageSize).ToListAsync());

        }

        public async Task<IActionResult> SearchIndex(string text)
        {
            int pageSize = 9;
            ViewBag.PageNumber = 1;
            ViewBag.PageRange = pageSize;
            ViewBag.CategorySlug = "";


            var products = from p in _context.Products
                         select p;

            if (!String.IsNullOrEmpty(text))
            {
                products= products.Where(s => s.Name!.Contains(text));
            }

            ViewBag.TotalPages = (int)Math.Ceiling((decimal)products.Count() / pageSize);
            return View(await products.OrderByDescending(p => p.Id).Skip((1 - 1) * pageSize).Take(pageSize).ToListAsync());
        }
        public async Task<IActionResult> ProductDetails(long id)
                {
                     var product = _context.Products.Find(id);
                    return View(product);
                }
        }
}
