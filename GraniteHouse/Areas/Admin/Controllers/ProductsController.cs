﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GraniteHouse.Data;
using GraniteHouse.Models;
using GraniteHouse.Models.ViewModel;
using GraniteHouse.Unitily;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraniteHouse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private ApplicationDbContext _db;
        private HostingEnvironment _hostingEnvironment;

        [BindProperty]
        public ProductsViewModel ProductsVM { get; set; }

        public ProductsController(ApplicationDbContext db, HostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            ProductsVM = new ProductsViewModel()
            {
                ProductTypes = _db.ProductTypes.ToList(),
                SpecialTags = _db.SpecialTags.ToList(),
                Products =new Products()
                

               

            };
           

        }
        public async Task<IActionResult> Index()
        {
            var products = _db.Products.Include(m => m.ProductTypes).Include(m => m.SpecialTags);
            return View(await products.ToListAsync());
        }
        //Get Create
        public IActionResult Create()
        {
            return View(ProductsVM);
        }
        //post create
        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(ProductsVM);
            }
            _db.Products.Add(ProductsVM.Products);
            await _db.SaveChangesAsync();

            // image being saved
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            var productsFromDb = _db.Products.Find(ProductsVM.Products.Id);
            if (files.Count != 0)
            {
                //image has been uploaded
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(files[0].FileName);

                using (var filestream = new FileStream(Path.Combine(uploads, ProductsVM.Products.Id + extension), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }
                productsFromDb.Image = @"\" + SD.ImageFolder + @"\" + ProductsVM.Products.Id + extension;

            }

            else
            {
                // when user does not upload image
                var uploads = Path.Combine(webRootPath, SD.ImageFolder + @"\" + SD.DefaultProductImage);
                System.IO.File.Copy(uploads, webRootPath + @"\" + SD.ImageFolder + @"\" + ProductsVM.Products.Id + ".png");
                productsFromDb.Image = @"\" + SD.ImageFolder + @"\" + ProductsVM.Products.Id + ".png";
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        //Get: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProductsVM.Products = await _db.Products.Include(m => m.SpecialTags).Include(m => m.ProductTypes).SingleOrDefaultAsync(m => m.Id == id);
            if (ProductsVM.Products == null)
            {
                return NotFound();
            }
            return View(ProductsVM);
        }

        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                var productFromDb = _db.Products.Where(m => m.Id == ProductsVM.Products.Id).FirstOrDefault();

                if (files.Count > 0 && files[0] != null)
                {
                    //if user upload a new image
                    var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                    var extension_new = Path.GetExtension(files[0].FileName);
                    var extension_old = Path.GetExtension(productFromDb.Image);
                    if (System.IO.File.Exists(Path.Combine(uploads, ProductsVM.Products.Id + extension_old)))
                    {
                        System.IO.File.Delete(Path.Combine(Path.Combine(uploads, ProductsVM.Products.Id + extension_old)));

                    }
                    using (var filestream = new FileStream(Path.Combine(uploads, ProductsVM.Products.Id + extension_new), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    ProductsVM.Products.Image = @"\" + SD.ImageFolder + @"\" + ProductsVM.Products.Id + extension_new;
                    if (ProductsVM.Products.Image != null)
                    {
                        productFromDb.Image = ProductsVM.Products.Image;
                    }
                    productFromDb.Name = ProductsVM.Products.Name;
                    productFromDb.Price = ProductsVM.Products.Price;
                    productFromDb.Available = ProductsVM.Products.Available;
                    productFromDb.ProductTypeId = ProductsVM.Products.ProductTypeId;
                    productFromDb.SpecialTagsID = ProductsVM.Products.SpecialTagsID;
                    productFromDb.ShadeColor = ProductsVM.Products.ShadeColor;
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
            
                

            }
            return View(ProductsVM);
        }

        // Get Procduct Details
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProductsVM.Products = await _db.Products.Include(m => m.SpecialTags).Include(m => m.ProductTypes).SingleOrDefaultAsync(m => m.Id == id);
            if (ProductsVM.Products == null)
            {
                return NotFound();
            }
            return View(ProductsVM);
        }

        //Get delete Product
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProductsVM.Products = await _db.Products.Include(m => m.SpecialTags).Include(m => m.ProductTypes).SingleOrDefaultAsync(m => m.Id == id);
            if (ProductsVM.Products == null)
            {
                return NotFound();
            }
            return View(ProductsVM);
        }

        //Post delete Product
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int Id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            Products products = await _db.Products.FindAsync(Id);
            if (products == null)
            {
                return NotFound();
            }
            else
            {
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(products.Image);
                if (System.IO.File.Exists(Path.Combine(uploads, products.Id + extension)))
                {
                    System.IO.File.Delete(Path.Combine(uploads, products.Id + extension));
                }
                _db.Products.Remove(products);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));


            }
        }

    }
}