using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DigitalGameStore.Models;
using DigitalGameStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
//using DigitalGameStore.Repository;

namespace DigitalGameStore.Controllers
{
    public class AdminController : Controller
    {
        private IRepository repository;
        private readonly IWebHostEnvironment hostEnvironment;
        public AdminController(IRepository repository, IWebHostEnvironment hostEnvironment)
        {
            this.repository = repository;
            this.hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        // ------------------------------------------------------------------------------------------------------------------------------------
        // (READ)

        public IActionResult Products()
        {
            IEnumerable<Product> Products = repository.Products;
            return View(Products);
        }

        // ------------------------------------------------------------------------------------------------------------------------------------
        // (DETAILS)

        public IActionResult DetailsProduct(int? id)
        {
            if (id == null)
            {
                return NotFound("ID nije poslan u zahtjevu.");
            }
            Product p = repository.Products.Where(p => p.ProductId == id).FirstOrDefault();
            if (p == null)
            {
                return NotFound("Poslan ID nije važeći.");
            }
            return View(new AddProductViewModel { Product=p, ProductClasses=repository.ProductClasses});
        }

        // ------------------------------------------------------------------------------------------------------------------------------------
        // (CREATE)

        [HttpGet]
        public IActionResult AddProduct()
        {
            //Product p = new Product();
            AddProductViewModel2 apvm = new AddProductViewModel2
            {
                ProductClasses = repository.ProductClasses
            };
            return View(apvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct([Bind] AddProductViewModel2 pvm)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            Product product = new Product
            {
                ProductId = repository.Products.Max(p => p.ProductId) + 1,
                ProductClassId = pvm.ProductClassId,
                ProductDescription = pvm.ProductDescription,
                ProductName = pvm.ProductName,
                ProductPrice = pvm.ProductPrice
            };
            product.ProductImgUrl = "~/images/" + product.ProductId.ToString() + ".jpeg";
            if (ModelState.IsValid)
            {
                string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath, "images");
                string uploadFilePath = Path.Combine(uploadsFolder, product.ProductId.ToString() + ".jpeg");
                pvm.Picture.CopyTo(new FileStream(uploadFilePath, FileMode.Create));
                repository.getContext().Product.Add(product);
                repository.getContext().SaveChanges();
                return RedirectToAction("Products");
            }
            else
            {
                return View(new AddProductViewModel2 {
                    ProductClassId = pvm.ProductClassId,
                    ProductDescription = pvm.ProductDescription,
                    ProductName = pvm.ProductName,
                    ProductPrice = pvm.ProductPrice,
                    ProductClasses = repository.ProductClasses
                });
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------
        // (UPDATE)

        [HttpGet]
        public IActionResult EditProduct(int? id)
        {
            if(id == null)
            {
                return NotFound("ID nije poslan u zahtjevu.");
            }
            Product p = repository.Products.Where(p => p.ProductId == id).FirstOrDefault();
            if(p == null)
            {
                return NotFound("Poslan ID nije važeći.");
            }
            return View(new AddProductViewModel2 {
                ProductId = p.ProductId,
                ProductClassId = p.ProductClassId,
                ProductDescription = p.ProductDescription,
                ProductName = p.ProductName,
                ProductPrice = p.ProductPrice,
                ProductClasses = repository.ProductClasses
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(int? id, [Bind] AddProductViewModel2 apvm)
        {
            if(id == null)
            {
                return NotFound("ID nije poslan u zahtjevu.");
            }

            Product product = new Product
            {
                ProductId = apvm.ProductId,
                ProductClassId = apvm.ProductClassId,
                ProductDescription = apvm.ProductDescription,
                ProductName = apvm.ProductName,
                ProductPrice = apvm.ProductPrice
            };
            product.ProductImgUrl = "~/images/" + product.ProductId.ToString() + ".jpeg";
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath, "images");
                string uploadFilePath = Path.Combine(uploadsFolder, product.ProductId.ToString() + ".jpeg");
                apvm.Picture.CopyTo(new FileStream(uploadFilePath, FileMode.Create));
                repository.getContext().Product.Update(product);
                repository.getContext().SaveChanges();
                return RedirectToAction("Products");
            }
            return View(new AddProductViewModel { Product = product, ProductClasses = repository.ProductClasses });
        }

        // ------------------------------------------------------------------------------------------------------------------------------------
        // (DELETE)

        [HttpGet]
        public IActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                return NotFound("ID nije poslan u zahtjevu.");
            }
            Product p = repository.Products.Where(p => p.ProductId == id).FirstOrDefault();
            if (p == null)
            {
                return NotFound("Poslan ID nije važeći.");
            }
            return View(p);
        }

        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            if (id == null)
            {
                return NotFound("ID nije poslan u zahtjevu.");
            }
            Product p = repository.Products.Where(p => p.ProductId == id).FirstOrDefault();
            if (p == null)
            {
                return NotFound("Poslan ID nije važeći.");
            }
            repository.getContext().Product.Remove(p);
            repository.getContext().SaveChanges();
            return RedirectToAction("Products");
        }
    }
}
