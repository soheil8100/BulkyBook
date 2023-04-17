
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.iRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        //Get
        public IActionResult Upsert(int? id)
        {

            ProductVm productVm = new()
            {
                product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text= i.Name,
                    Value = i.Id.ToString()
                })
            };
             

            if (id == null || id == 0)
            {
                //Create Product
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeLisr"] = CoverTypeLisr;
                return View(productVm);
            }
            else
            {
                //Update Product
                productVm.product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id==id);
                return View(productVm);

            }

        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(ProductVm obj, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                string wwwRothPath = _hostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var upload = Path.Combine(wwwRothPath, @"Images\Product");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRothPath, obj.product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }


                    using (var fileStreams = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);

                    }
                    obj.product.ImageUrl = @"\Images\Product\" + fileName + extension;
                }

                if (obj.product.Id == 0) 
                {
                    _unitOfWork.Product.Add(obj.product);
                }
                else
                {
                    _unitOfWork.Product.Update(obj.product);
                }
                _unitOfWork.Save();

                TempData["success"] = "Product Created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }
       
        
        #region API CAllS
        [HttpGet]
        public IActionResult GetAll()
        {
            var produdutList = _unitOfWork.Product.GetAll(incldeProperties: "Category,CoverType");
            return Json(new { data = produdutList });
        }
        [HttpDelete]
        
        public IActionResult Delete(int? id)
        {

            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new {success = false, message = "Error While Deleting"});

            }

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Remove(obj);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Delete successful" });

            }
            return View(obj);

        }
        #endregion

    }
}
