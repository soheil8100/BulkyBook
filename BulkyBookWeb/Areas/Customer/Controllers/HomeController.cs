using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.iRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable < Product > ProductList = _unitOfWork.Product.GetAll(incldeProperties: "Category,CoverType");

            return View(ProductList);
        }

        public IActionResult Details(int productId)
        {
            ShoppingCard cartObj = new()
            {
                Count = 1,
                ProductId = productId,  
                Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == productId, incldeProperties: "Category,CoverType")
            };
       
            return View(cartObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCard shoppingCard)
        {
            // find UserID
            var claimIdentity =(ClaimsIdentity) User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCard.ApplicationUserId = claim.Value;

            ShoppingCard cartFromDb = _unitOfWork.ShoppingCard.GetFirstOrDefault(
                u=> u.ApplicationUserId == claim.Value && u.ProductId == shoppingCard.ProductId
                );

            if (cartFromDb == null)
            {
                _unitOfWork.ShoppingCard.Add(shoppingCard);

            }
            else
            {
                _unitOfWork.ShoppingCard.IncrementCount(cartFromDb, shoppingCard.Count);
            }

            _unitOfWork.Save();
           

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}