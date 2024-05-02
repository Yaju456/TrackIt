using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.Identity.Client;
using NuGet.Frameworks;
using System.Collections.Generic;
using System.Text.Json.Serialization.Metadata;
using TrackIt.Data;
using TrackIt.Models;
using TrackIt.PreData;
using TrackIt.Repository.Irepository;
using TrackIt.ViewModel;

namespace TrackIt.Controllers
{
//    [Authorize(Roles = Roll.Admin)]

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
            foreach(var data in order)
            {
                data.In_Stock = _db.Stock.getSpecifics(u => u.Order_id == data.Id && u.InStock == "Y", null).Count();
                data.Quantity= _db.Stock.getSpecifics(u => u.Order_id == data.Id, null).Count();
                _db.Order.Update(data);
            }
            _db.Save();
            return new JsonResult(order);
        }
        
        public JsonResult Get(int? id) 
        {
            List<StockClass> list = _db.Stock.getSpecifics(u => u.Order_id == id, prop: "Product,Customer").ToList();
            return new JsonResult(list);
        }

        [HttpPost]
        public IActionResult Index(OrderClass obj)
        {
            if (ModelState.IsValid)
            {
                obj.In_Stock = obj.Quantity;
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

                return Json(new
                {
                    success=true,
                    message="Successful data entry"
                });;
            }
            else
            {
                return Json(new
                {
                    success = false,
                    message = "Error Modal State"
                }); ;
            }
        }

        public IActionResult AddSerial(int? id)
        {
             IEnumerable < SelectListItem > Customer_id = _db.customer.getAll(prop: null).Select(u => new SelectListItem
            {
                Text = u.Id + " " + u.Name,
                Value = u.Id.ToString()
            });
            ViewBag.Customer_id = Customer_id;
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
                    if(data.customer_id!=null)
                    {
                        one.Customer_id = data.customer_id;
                        one.InStock = "N";
                    }
                    else
                    {
                        one.InStock = "Y";
                    }
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


        [HttpPost]
        public IActionResult Addstock(Add_stock data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    StockClass one = new StockClass();
                    one.Id = data.id;
                    one.Order_id= data.order_id;
                    if(data.customer_id==0)
                    {
                        one.Customer_id =null;
                        one.InStock = "Y";

                    }
                    else
                    {
                        one.Customer_id=data.customer_id;
                        one.InStock = "Y";
                    }
                    
                    one.serial_number = data.serial_no;
                    one.Product_id = _db.Order.GetOne(u => u.Id == data.order_id,prop:null).Product_id;
                    _db.Stock.Add(one);
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
                        success = false,
                        message = ex.Message
                    });
                }

            }
            else
            {
                return Json(new
                {
                    success = false,
                    message = "Modal state is Not validate"
                });
            }
        }






        public IActionResult Deletestock(int? id)
        {
            StockClass To_Delete = _db.Stock.GetOne(u => u.Id == id, prop: null);
            if(To_Delete != null)
            {
                _db.Stock.Delete(To_Delete);
                _db.Save();
                return Json(new
                {
                    success = true,
                    message = To_Delete.Id + " id's recorde is Deleted"
                });
            }
            else
            {
                return Json(new
                {
                    success = false,
                    message = "There was error while deleting"
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