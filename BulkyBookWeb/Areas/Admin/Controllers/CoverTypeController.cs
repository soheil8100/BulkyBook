
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.iRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }
        //Get
        public IActionResult Create()
        {

            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(CoverType obj)
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType create successfully";
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
            var coverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            //var categoryfromDbSingle = _db.Categories.SingleOrDefault(u => u.id == id);
            if (coverTypeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(coverTypeFromDbFirst);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(CoverType obj)
        {
           
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType update successfully";
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
            var coverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            //var categoryfromDbSingle = _db.Categories.SingleOrDefault(u => u.id == id);
            if (coverTypeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(coverTypeFromDbFirst);
        }
        //Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType deleted successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }
    }
}
