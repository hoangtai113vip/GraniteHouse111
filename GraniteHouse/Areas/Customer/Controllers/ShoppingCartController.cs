using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraniteHouse.Data;
using GraniteHouse.Extensions;
using GraniteHouse.Models;
using GraniteHouse.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraniteHouse.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public ShoppingCartViewModel ShoppingCartVM { get; set; }

        public ShoppingCartController(ApplicationDbContext db)
        {
            _db = db;
            ShoppingCartVM = new ShoppingCartViewModel
            {
                Products = new List<Models.Products>()
            };
        }

        //get index Shopping cart
        public async Task<IActionResult> Index()
        {
            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if (lstShoppingCart.Count > 0)
            {
                foreach(int cartItem in lstShoppingCart)
                {
                    Products prod = _db.Products.Include(m => m.SpecialTags).Include(m => m.ProductTypes).Where(m => m.Id == cartItem).FirstOrDefault();
                    ShoppingCartVM.Products.Add(prod);
                }
              
            }
            return View(ShoppingCartVM);
            
            
        }
        //post action index
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public IActionResult IndexPost()
        {
            List<int> lstCartItems = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            ShoppingCartVM.Appointments.AppointmentDate = ShoppingCartVM.Appointments.AppointmentDate.AddHours(ShoppingCartVM.Appointments.AppointmentTime.Hour).
                AddMinutes(ShoppingCartVM.Appointments.AppointmentTime.Minute);
            Appointments appointments = ShoppingCartVM.Appointments;
            _db.Appointments.Add(appointments);
            _db.SaveChanges();

            int appointmentId = appointments.Id;

            foreach(int productId in lstCartItems)
            {
                ProductsSelectedForAppointment productsSelectedForAppointment = new ProductsSelectedForAppointment()
                {
                    AppointmentId = appointmentId,
                    ProductId = productId
                };

                _db.ProductsSelectedForAppointments.Add(productsSelectedForAppointment);
            }
            _db.SaveChanges();

            lstCartItems = new List<int>();

            HttpContext.Session.Set("ssShoppingCart", lstCartItems);
            return RedirectToAction("Index");

        }

    //remove 
    public IActionResult Remove(int id)
        {
            List<int> lstItemCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if (lstItemCart.Count > 0)
            {
                if (lstItemCart.Contains(id))
                {
                    lstItemCart.Remove(id);
                    HttpContext.Session.Set("ssShoppingCart", lstItemCart);
                }
            }
            return RedirectToAction("Index");
        }
    }
}