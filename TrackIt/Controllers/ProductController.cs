using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrackIt.Data;
using TrackIt.Models;
using TrackIt.PreData;
using TrackIt.Repository.Irepository;

namespace TrackIt.Controllers
{
    public class ProductController : Controller
    {
        private readonly IunitOfwork _db;
        public ProductController(IunitOfwork db)
        {
            _db = db;
        }
      

        public JsonResult GetAll()
        {
            List<ProductClass> Products = _db.Product.getAll(prop: null).ToList();
            return new JsonResult(Products);
        }
        public IActionResult Index()
        {
            IEnumerable<SelectListItem> CompanyName = Company.names.Select(u => new SelectListItem
            {
                Text = u,
                Value = u
            });

            IEnumerable<SelectListItem> CatagoryType = Company.type.Select(u => new SelectListItem { Text = u, Value = u });
            ViewBag.CompanyName = CompanyName;
            ViewBag.CatagoryType = CatagoryType;
            return View();
        }
        [HttpPost]
        public IActionResult Index(ProductClass obj)
        {
            if (ModelState.IsValid)
            {
                _db.Product.Add(obj);
                _db.Save();
                return Json(new
                {
                    message = "New data Added",
                    success = true
                }); ;
            }
            else
            {
                return View(obj);
            }
        }

        public IActionResult Delete(int? id)
        {
            ProductClass To_delete = _db.Product.GetOne(u => u.Id == id, prop:null);
            if (To_delete != null)
            {
                _db.Product.Delete(To_delete);
                _db.Save();
                return Json(new
                {
                    success = true,
                    message = To_delete.Id + " id number product Deleted"
                });
            }
            else
            {
                return Json(new
                {
                    success= false,
                    Message="There was an error deleting"
                });
            }
        }

    }
}
