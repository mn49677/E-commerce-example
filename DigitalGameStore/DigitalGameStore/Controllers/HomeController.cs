using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DigitalGameStore.ViewModels;
using DigitalGameStore.Models;
//using DigitalGameStore.Repository;

namespace DigitalGameStore.Controllers
{
    public class HomeController : Controller
    {
        private IRepository repository;
        public HomeController(IRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel(repository.Products);
            return View(model);
        }
        public IActionResult PS4()
        {
            HomeViewModel model = new HomeViewModel(repository.Products.Where(p => p.ProductClassId == 0));
            return View(model);
        }

        public IActionResult XBOX()
        {
            HomeViewModel model = new HomeViewModel(repository.Products.Where(p => p.ProductClassId == 1));
            return View(model);
        }

        public IActionResult PC()
        {
            HomeViewModel model = new HomeViewModel(repository.Products.Where(p => p.ProductClassId == 2));
            return View(model);
        }

        public IActionResult Console()
        {
            HomeViewModel model = new HomeViewModel(repository.Products.Where(p => p.ProductClassId == 3));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string search)
        {
            HomeViewModel model = new HomeViewModel(repository.Products.Where(p => p.ProductDescription.Contains(search) || p.ProductName.Contains(search)));
            return View(model);
        }

        public IActionResult Product(int id)
        {
            Product p = repository.Products.Where(p => p.ProductId == id).FirstOrDefault();
            Review r = new Review();
            IEnumerable<int> rm = repository.Reviews.Where(r => r.ProductId == id).Select(r => r.ReviewEvaluation);
            double avg = rm.Average();
            ProductViewModel pvm = new ProductViewModel
            {
                Product = p,
                Review = r,
                ReviewMark = avg
            };
            return View(pvm);
        }

        public IActionResult Service()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Review(ProductViewModel pvm, int id)
        {
            System.Diagnostics.Debug.WriteLine(pvm.Review.ReviewComment);
            System.Diagnostics.Debug.WriteLine(pvm.Review.ReviewEvaluation);
            System.Diagnostics.Debug.WriteLine(pvm.Review.ProductId);
            System.Diagnostics.Debug.WriteLine(pvm.Review.UserId);
            Review r = pvm.Review;
            r.ReviewId = repository.Reviews.Max(r => r.ReviewId) != null ? ((Int32.Parse(repository.Reviews.Max(r => r.ReviewId)) + 1).ToString()) : "0";
            repository.getContext().Review.Add(pvm.Review);
            repository.getContext().SaveChanges();
            return RedirectToAction("Product", new { id=id});
        }

        [HttpGet]
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
            return View(new AddProductViewModel { Product = p, ProductClasses = repository.ProductClasses });
        }
    }
}
