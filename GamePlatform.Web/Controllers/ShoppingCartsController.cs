using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GamePlatform.Domain.Domain;
using GamePlatform.Repository;
using GamePlatform.Service.Interface;
using System.Security.Claims;
using Stripe;

namespace GamePlatform.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartsController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        // GET: ShoppingCarts
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId != null)
            {
                return View(_shoppingCartService.GetCartInfo(userId));
            }
            return Redirect("/Identity/Account/Login");
        }

        // POST: ShoppingCarts/Delete/5
        public IActionResult DeleteFromShoppingCart(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _shoppingCartService.DeleteFromCart(userId, id);

            return RedirectToAction("Index");
        }

        // POST: ShoppingCarts/Order
        public IActionResult Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _shoppingCartService.Order(userId);

            return RedirectToAction("Index", "ShoppingCarts");
        }

        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {
            StripeConfiguration.ApiKey = "sk_test_51Io84IHBiOcGzrvu4sxX66rTHq8r5nxIxRiJPbOHB4NwVJOE1jSlxgYe741ITs024uXhtpBFtxm3RoCZc3kafocC00IhvgxkL0";
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = this._shoppingCartService.GetCartInfo(userId);

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(order.TotalPrice) * 100),
                Description = "EShop Application Payment",
                Currency = "usd",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                this.Order();
                return RedirectToAction("SuccessPayment");

            }
            else
            {
                return RedirectToAction("NotsuccessPayment");
            }
        }
    }
}
