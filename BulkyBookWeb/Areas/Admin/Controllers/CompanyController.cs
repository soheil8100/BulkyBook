
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.iRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.InteropServices;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        //Get
        public IActionResult Upsert(int? id)
        {

            Company Company = new();
            

            if (id == null || id == 0)
            {
                //Create Product
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeLisr"] = CoverTypeLisr;
                return View(Company);
            }
            else
            {
                //Update Product
                Company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
                return View(Company);

            }

        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(Company obj)
        {

            if (ModelState.IsValid)
            {


                if (obj.Id == 0)
                {
                    _unitOfWork.Company.Add(obj);
                    TempData["success"] = "Company Created successfully";

                }
                else
                {
                    _unitOfWork.Company.Update(obj);
                    TempData["success"] = "Company Updated successfully";

                }
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(obj);

        }


        #region API CAllS
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _unitOfWork.Company.GetAll();
            return Json(new { data = companyList });
        }
        [HttpDelete]

        public IActionResult Delete(int? id)
        {

            var obj = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });

            }

            
            if (ModelState.IsValid)
            {
                _unitOfWork.Company.Remove(obj);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Delete successful" });

            }
            return View(obj);

        }
        #endregion

    }
}
