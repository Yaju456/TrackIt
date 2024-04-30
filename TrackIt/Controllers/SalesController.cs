using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using System.Text.Json.Serialization.Metadata;
using TrackIt.Models;
using TrackIt.PreData;
using TrackIt.Repository.Irepository;
using TrackIt.ViewModel;

namespace TrackIt.Controllers
{
    public class SalesController : Controller
    {
        private readonly IunitOfwork _db;

        public SalesController(IunitOfwork db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult getall()
        {
            List<SalesClass> list = _db.sales.getAll(prop: "Clinet,Product").ToList();
            return new JsonResult(list);
        }

        public IActionResult AddSales()
        {
            IEnumerable<SelectListItem> Client_id = _db.Client.getAll(prop: null).Select(u => new SelectListItem
            {
                Text = u.Id + " " + u.Name,
                Value = u.Id.ToString()
            });

            IEnumerable<SelectListItem> Product_id = _db.Product.getAll(prop: null).Select(u => new SelectListItem
            {
                Text = u.Id + " " + u.Name,
                Value = u.Id.ToString()
            });
            SalesVM salesVM = new SalesVM();
            salesVM.Client = Client_id;
            salesVM.Product = Product_id;
            return View(salesVM);
        }



        [HttpPost]
        public IActionResult AddSales(SalesVM one)

        {
            if (ModelState.IsValid)
            {
                _db.sales.Add(one.TheirSales);
                _db.Save();
                SalesClass JustSales = _db.sales.GetOne(u => u.Id == one.TheirSales.Id, prop: "");
                List<StockClass> StockList = _db.Stock.getSpecifics(u => u.Product_id == JustSales.Product_id && string.Equals(u.InStock, "Y"), prop: "").ToList();
                int i = 0;
                foreach (var One_stock in StockList)
                {
                    if (i >= one.TheirSales.Quantity)
                    {
                        break;
                    }
                    One_stock.InStock = "N";
                    _db.Stock.Update(One_stock);
                    i++;
                }
                ProductClass Up_quantity = _db.Product.GetOne(u => u.Id == JustSales.Product_id, prop: null);
                Up_quantity.In_stock = _db.Stock.getSpecifics(a => a.Product_id == Up_quantity.Id && string.Equals(a.InStock, "Y"), prop: "").Count();
                _db.Product.Update(Up_quantity);
                _db.Save();
                return Json(new { data = _db.sales.getAll(prop: "Clinet,Product") });
            }
            IEnumerable<SelectListItem> Client_id = _db.Client.getAll(prop: null).Select(u => new SelectListItem
            {
                Text = u.Id + " " + u.Name,
                Value = u.Id.ToString()
            });

            IEnumerable<SelectListItem> Product_id = _db.Product.getAll(prop: null).Select(u => new SelectListItem
            {
                Text = u.Id + " " + u.Name,
                Value = u.Id.ToString()
            });

            one.Client = Client_id;
            one.Product = Product_id;
            return View(one);
        }



    }
}
