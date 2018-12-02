﻿using System;
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
    }
}