using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using System.Data;
using System.Text.Json.Serialization.Metadata;
using TrackIt.Models;
using TrackIt.PreData;
using TrackIt.Repository.Irepository;
using TrackIt.ViewModel;

namespace TrackIt.Controllers
{
    [Authorize(Roles =  Roll.client+ "," + Roll.Admin)]

    public class SalesController : Controller
    {
        private readonly IunitOfwork _db;
        private readonly UserManager<IdentityUser> _userManager;



        public SalesController(IunitOfwork db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            List<ProductClass> products = _db.Product.getAll(prop: null).ToList();
            return View(products);
        }

        //public IActionResult MyPurchase()
        //{
        //    List<StockClass> list = _db.Stock.getSpecifics(u=>u.Client_id== _userManager.GetUserId(HttpContext.User), prop: "Product").ToList();
        //    return View(list);
        //}

        public IActionResult Buy(int? id)
        {
            ProductVM data = new ProductVM();
            ProductClass one = _db.Product.GetOne(u => u.Id == id, prop: null);
            data.productClass = one;
            data.num = 0;
            return View(data);
        }

        [HttpPost]
        public IActionResult Buy(ProductVM data)
        {
            ProductClass one = _db.Product.GetOne(u=>u.Id==data.productClass.Id, prop: null);

            if(one.In_stock>=data.num && data.num>0)
            {
                SalesClass two = new SalesClass
                {
                    Product_id = one.Id,
                    Sales_Date = DateTime.Now,
                    Quantity = (int)data.num,
                    Rate = 19,
                    Client_id = _userManager.GetUserId(HttpContext.User)
                };
                _db.sales.Add(two);
                _db.Save();
                SalesClass JustSales = _db.sales.GetOne(u => u.Id == two.Id, prop: "");
                List<StockClass> StockList = _db.Stock.getSpecifics(u => u.Product_id == JustSales.Product_id && string.Equals(u.InStock, "Y"), prop: "").ToList();
                int i = 0;
                foreach (var One_stock in StockList)
                {
                    if (i >= two.Quantity)
                    {
                        break;
                    }
                    One_stock.InStock = "N";
                    One_stock.Sales_id = two.Id;
                 //   One_stock.Client_id = _userManager.GetUserId(HttpContext.User);
                    _db.Stock.Update(One_stock);
                    i++;
                }
                _db.Save();
                ProductClass Up_quantity = _db.Product.GetOne(u => u.Id == JustSales.Product_id, prop: null);
                Up_quantity.In_stock = _db.Stock.getSpecifics(a => a.Product_id == Up_quantity.Id && string.Equals(a.InStock, "Y"), prop: "").Count(); 
                _db.Product.Update(Up_quantity);
                _db.Save();
                TempData["success"] = "Item Bought successfully";
            }
            else
            {
                TempData["error"] = "Something went wrong";
            }
            return RedirectToAction("Index");
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
