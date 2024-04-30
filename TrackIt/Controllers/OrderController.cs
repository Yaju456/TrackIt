using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.Identity.Client;
using System.Text.Json.Serialization.Metadata;
using TrackIt.Data;
using TrackIt.Models;
using TrackIt.PreData;
using TrackIt.Repository.Irepository;
using TrackIt.ViewModel;

namespace TrackIt.Controllers
{
    [Authorize(Roles = Roll.Admin)]

    public class OrderController : Controller
    {
        private readonly IunitOfwork _db;
        public OrderController(IunitOfwork db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<SelectListItem> Vendor_id = _db.Vendor.getAll(prop: null).Select(u => new SelectListItem
            {
                Text = u.Id + " " + u.Name,
                Value = u.Id.ToString()
            });

            IEnumerable<SelectListItem> Product_id = _db.Product.getAll(prop: null).Select(u => new SelectListItem
            {
                Text = u.Id + " " + u.Name,
                Value = u.Id.ToString()
            });

            //OrderClass one = new OrderClass();
            //if (data!=null)
            //{
            //    one = data;
            //}
            ViewBag.Vendor_id = Vendor_id;
            ViewBag.Product_id = Product_id;
            return View();
        }

        public JsonResult GetAll()
        {
            List<OrderClass> order = _db.Order.getAll(prop: "vendor,Product").ToList();
            return new JsonResult(order);
        }
        
        public JsonResult Get(int? id) 
        {
            List<StockClass> list = _db.Stock.getSpecifics(u => u.Order_id == id, prop: null).ToList();
            return new JsonResult(list);
        }

        [HttpPost]
        public IActionResult Index(OrderClass obj)
        {
            if (ModelState.IsValid)
            {
                _db.Order.Add(obj);
                _db.Save();
                OrderClass justOrder = _db.Order.GetOne(a => a.Id == obj.Id, prop: "Product");
                
                //This code is to add each element in the stock
                for (int i = 0; i < justOrder.Quantity; i++)
                {
                    StockClass oneItem = new StockClass();
                    oneItem.Order_id = justOrder.Id;
                   // oneItem.serial_number = "OrderNO"+justOrder.Id+ justOrder.Product.Name + "id" + justOrder.Product.Id + "vendor" + justOrder.vendor_id + "no"+i;
                    oneItem.InStock = "Y";
                    oneItem.Product_id = justOrder.Product.Id;
                    _db.Stock.Add(oneItem);
                }
                _db.Save();

                //This Thing can be done using Trigger in sqlserver ask sir
                ProductClass Up_quantity = _db.Product.GetOne(a => a.Id == justOrder.Product_id, prop: "");
                Up_quantity.In_stock = _db.Stock.getSpecifics(a => a.Product_id == Up_quantity.Id && string.Equals(a.InStock, "Y"), prop: "").Count();
                _db.Product.Update(Up_quantity);
                _db.Save();
                return Json(new
                {
                    success=true,
                    message="Successful data entry"
                });;
            }
            else
            {
                return View(obj);
            }
        }

        public IActionResult AddSerial(int? id)
        {
            return View(id);
        }

        [HttpPost]
        public IActionResult AddSerial(IDandSerial data) 
        {
            if(ModelState.IsValid)
            {
                try
                {
                    StockClass one = _db.Stock.GetOne(u => u.Id == data.id, prop: null);
                    one.serial_number = data.serial_no;
                    _db.Stock.Update(one);
                    _db.Save();
                    return Json(new
                    {
                        success = true,
                        message = "Serial Number Updated"
                    });
                }
                catch (Exception ex)
                {
                    return Json(new
                    {
                        success=false,
                        message=ex.Message
                    });
                }

            }
            else
            {
                return Json(new
                {
                    success=false,
                    message="Modal state is Not validate"
                });
            }
        }
        public IActionResult Delete(int id)
        {
            OrderClass To_delete = _db.Order.GetOne(u => u.Id == id, prop: null);
            if(To_delete != null) 
            {
                _db.Order.Delete(To_delete);
                _db.Save();
                return Json(new
                {
                    success = true,
                    message= To_delete.Id+" id's recorde is Deleted"
                });
            }
            else
            {
                return Json(new
                {
                    success= false,
                    message="There was error while deleting"
                });
            }
        }
    }
}