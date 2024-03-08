
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using BulkyBookWeb.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ApplicationDbContext _db;
        //public ProductController(ApplicationDbContext db)
        //{
        //    _db = db;
        //}

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string term = "", string orderBy = "", int currentPage = 1)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower(); // Search term

            //IQueryable<Product> objProductList = _db.Categories;
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            ////Pagination
            //int totalRecords = objProductList.Count();
            //var catData = new ProductViewModel();
            //if (!string.IsNullOrEmpty(term))
            //{
            //    objProductList = objProductList.Where(u => u.Name.ToLower().StartsWith(term));
            //} // Search term
            //int pageSize = 5;
            //int totalPages = (int)Math.Ceiling(totalRecords / (decimal)pageSize);
            //objProductList = objProductList.Skip((currentPage - 1) * pageSize).Take(pageSize);

            //catData.Categories = objProductList;
            //catData.PageSize = totalPages;
            //catData.CurrentPage = currentPage;
            //catData.TotalPages = totalPages;
            //catData.Term = term;


            return View(objProductList);
        }




        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            else
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Product created successfully";
                return RedirectToAction("Index");
            }

        }


        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            //var productFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            else
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Product updated successfully";

                return RedirectToAction("Index");
            }

        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            //var productFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Product deleted successfully";

            return RedirectToAction("Index");


        }
    }
}
