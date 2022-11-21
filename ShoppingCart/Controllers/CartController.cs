using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Infrastructure;
using ShoppingCart.Models;
using ShoppingCart.Models.ViewModels;

namespace ShoppingCart.Controllers
{
        public class CartController : Controller
        {
                private readonly DataContext _context;
                private static CartViewModel _cartViewModel;
                UserManager<AppUser> _userManager;

                public CartController(DataContext context, UserManager<AppUser> userManager)
                {
                        _context = context;
                        _userManager = userManager; 
                        _cartViewModel = new CartViewModel();
                }

                public IActionResult Index()
                {
                        List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

                        CartViewModel cartVM = new()
                        {
                                CartItems = cart,
                                GrandTotal = cart.Sum(x => x.Quantity * x.Price)
                        };
                        if (cartVM.GrandTotal > 0)
                        {

                        }
                        return View(cartVM);
                }
        public IActionResult IndexDhaka()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartVM = new()
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price)
            };
            if (cartVM.GrandTotal > 0)
            {
                if (cartVM.DeliveryLocation == Location.Dhaka)
                    cartVM.GrandTotal += 60;
            }
            return View(cartVM);
        }
        public IActionResult IndexOutsideDhaka()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartVM = new()
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price)
            };
            if (cartVM.GrandTotal > 0)
            {
                if (cartVM.DeliveryLocation == Location.Dhaka)
                    cartVM.GrandTotal += 130;
            }
            return View(cartVM);
        }

        public async Task<IActionResult> Add(long id,int s)
                {
                        Product product = await _context.Products.FindAsync(id);
                        

                        List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

                        CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();
           // cartItem.MySize = (size)s;  //ERROR HERE
                        if (cartItem == null)
                        {
                                cart.Add(new CartItem(product));
                        }
                        else
                        {
                                cartItem.Quantity += 1;
                        }

                        HttpContext.Session.SetJson("Cart", cart);

                        TempData["Success"] = "The product has been added!";

                        return Redirect(Request.Headers["Referer"].ToString());
                }

                public async Task<IActionResult> Decrease(long id)
                {
                        List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

                        CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

                        if (cartItem.Quantity > 1)
                        {
                                --cartItem.Quantity;
                        }
                        else
                        {
                                cart.RemoveAll(p => p.ProductId == id);
                        }

                        if (cart.Count == 0)
                        {
                                HttpContext.Session.Remove("Cart");
                        }
                        else
                        {
                                HttpContext.Session.SetJson("Cart", cart);
                        }

                        TempData["Success"] = "The product has been removed!";

                        return RedirectToAction("Index");
                }

                public async Task<IActionResult> Remove(long id)
                {
                        List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

                        cart.RemoveAll(p => p.ProductId == id);

                        if (cart.Count == 0)
                        {
                                HttpContext.Session.Remove("Cart");
                        }
                        else
                        {
                                HttpContext.Session.SetJson("Cart", cart);
                        }

                        TempData["Success"] = "The product has been removed!";

                        return RedirectToAction("Index");
                }

                public IActionResult Clear()
                {
                        HttpContext.Session.Remove("Cart");

                        return RedirectToAction("Index");
                }
        public IActionResult CheckOut(string id,int loc)
        {
            try
            {
                List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
                User appUser = _context.Users.First(x => x.UserName == id);
                _cartViewModel.id = Guid.NewGuid();
                _cartViewModel.UserEmail = appUser.Email;
                _cartViewModel.UserPhone = appUser.PhoneNumber;
                _cartViewModel.UserAddress = appUser.Address;
                _cartViewModel.GrandTotal = cart.Sum(x => x.Quantity * x.Price);
                _cartViewModel.GrandTotal += 60;
                _cartViewModel.LocationIsDhaka = true;
                var temp = HttpContext.Session.Get("");
                _context.OrderCarts.Add(_cartViewModel);
                _context.SaveChanges();

                foreach (CartItem c in cart)
                {
                    c.OrderId = _cartViewModel.id;
                    _context.CartItems.Add(c);
                }
                _context.SaveChanges();
                return RedirectToAction("Clear");
            }
            catch
            {
                return RedirectToAction("Index");
            }
           
        }
        public IActionResult CheckOutSide(string id, int loc)
        {
            try
            {
                List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
                User appUser = _context.Users.First(x => x.UserName == id);
                _cartViewModel.id = Guid.NewGuid();
                _cartViewModel.UserEmail = appUser.Email;
                _cartViewModel.UserPhone = appUser.PhoneNumber;
                _cartViewModel.UserAddress = appUser.Address;
                _cartViewModel.GrandTotal = cart.Sum(x => x.Quantity * x.Price);
                _cartViewModel.GrandTotal += 130;

                _cartViewModel.LocationOutsideDhaka = true;
                var temp = HttpContext.Session.Get("");
                _context.OrderCarts.Add(_cartViewModel);
                _context.SaveChanges();

                foreach (CartItem c in cart)
                {
                    c.OrderId = _cartViewModel.id;
                    _context.CartItems.Add(c);
                }
                _context.SaveChanges();
                return RedirectToAction("Clear");
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }

    }
}
