
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.iRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
            return View(objCategoryList);
        }
        //Get
        public IActionResult Create()
        {

            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                //ModelState.AddModelError("Custumkey", "The Display order can not exactly match the Name.");
                ModelState.AddModelError("name", "The Display order can not exactly match the Name.");

            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category create successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        //Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.id == id);
            //var categoryfromDbSingle = _db.Categories.SingleOrDefault(u => u.id == id);
            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }
            return View(categoryFromDbFirst);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                //ModelState.AddModelError("Custumkey", "The Display order can not exactly match the Name.");
                ModelState.AddModelError("name", "The Display order can not exactly match the Name.");

            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category update successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.id == id);
            //var categoryfromDbSingle = _db.Categories.SingleOrDefault(u => u.id == id);
            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }
            return View(categoryFromDbFirst);
        }
        //Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.id == id);
            if (obj == null)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category deleted successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }
    }
}
