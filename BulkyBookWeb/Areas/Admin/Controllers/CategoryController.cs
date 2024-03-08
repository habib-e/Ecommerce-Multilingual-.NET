
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using BulkyBookWeb.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ApplicationDbContext _db;
        //public CategoryController(ApplicationDbContext db)
        //{
        //    _db = db;
        //}

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string term = "", string orderBy = "", int currentPage = 1)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower(); // Search term

            //IQueryable<Category> objCategoryList = _db.Categories;
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            ////Pagination
            //int totalRecords = objCategoryList.Count();
            //var catData = new CategoryViewModel();
            //if (!string.IsNullOrEmpty(term))
            //{
            //    objCategoryList = objCategoryList.Where(u => u.Name.ToLower().StartsWith(term));
            //} // Search term
            //int pageSize = 5;
            //int totalPages = (int)Math.Ceiling(totalRecords / (decimal)pageSize);
            //objCategoryList = objCategoryList.Skip((currentPage - 1) * pageSize).Take(pageSize);

            //catData.Categories = objCategoryList;
            //catData.PageSize = totalPages;
            //catData.CurrentPage = currentPage;
            //catData.TotalPages = totalPages;
            //catData.Term = term;


            return View(objCategoryList);
        }




        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            else
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Category created successfully";
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
            var categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            else
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Category updated successfully";

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
            var categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Category deleted successfully";

            return RedirectToAction("Index");


        }
    }
}
