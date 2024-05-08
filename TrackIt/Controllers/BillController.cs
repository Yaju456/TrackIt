using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TrackIt.Models;
using TrackIt.PreData;
using TrackIt.Repository;
using TrackIt.Repository.Irepository;
using TrackIt.ViewModel;

namespace TrackIt.Controllers
{
    [Authorize(Roles = Roll.client+","+Roll.Admin)]
    public class BillController : Controller
    {
        private readonly IunitOfwork _db;
        private readonly UserManager<IdentityUser> _userManager;
        public BillController(IunitOfwork db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult CreateBill()
        {
            IEnumerable<SelectListItem> CustomerName = _db.customer.getAll(null).Select(u=> new SelectListItem
            {
                Text = u.Name,
                Value= u.Id.ToString(),
            });
            IEnumerable<SelectListItem> CustomerNumber = _db.customer.getAll(null).Select(u => new SelectListItem
            {
                Text = u.PhoneNumber.ToString(),
                Value = u.Id.ToString(),
            });
            IEnumerable<SelectListItem> ProductName = _db.Product.getAll(null).Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            });
            ViewBag.CustomerName=CustomerName;
            ViewBag.CustomerNumber=CustomerNumber;
            ViewBag.ProductName=ProductName;
            return View();
        }

        public JsonResult getCombill(int? id)
        {
            List<BillhasProductClass> data= _db.Billhasproduct.getSpecifics(u=>u.Bill_id==id, prop:"Product").ToList();
            return Json(data);
        }
        public JsonResult getCom()
        {
            string user_id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<BillhasProductClass> data = _db.Billhasproduct.getSpecifics(u=>u.Bill_id==null && u.User_id==user_id, prop: "Product").ToList();
            return new JsonResult(data);    
        }

        public JsonResult stockid(int? id)
        {
            List<StockClass> data = _db.Stock.getSpecifics(u=>u.billhasProduct_id==id,prop:null).ToList();
            return Json(data);
        }

        public IActionResult Deletebill(int id)
        {
            BillClass one = _db.Bill.GetOne(u => u.Id == id, null);
            List<BillhasProductClass> two= _db.Billhasproduct.getSpecifics(u=>u.Bill_id== id, prop:null).ToList();
            if (one != null)
            {
                try
                {
                    _db.Billhasproduct.DeleteMost(two);
                    _db.Save();
                    _db.Bill.Delete(one);
                    _db.Save();
                    return Json(new
                    {
                        success = true,
                        message = "Value Deleted Successfully"
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
                    message = "No value found"
                });
            }
        }
        public IActionResult DeleteCom(int id)
        {
            BillhasProductClass one = _db.Billhasproduct.GetOne(u => u.Id == id, null);
            if(one != null) 
            {
                _db.Billhasproduct.Delete(one);
                _db.Save();
                return Json(new
                {
                    success=true,
                    message="Value Deleted Successfully"
                });
            }
            else
            {
                return Json(new
                {
                    success=false,
                    message="Product not found"
                });
            }
        }

        public IActionResult Check(int? id)
        {
            BillClass one = _db.Bill.GetOne(u=>u.Id == id, prop: "Customer");
            return View(one);
        }
        public JsonResult AllBill()
        {
            List<BillClass> data = _db.Bill.getAll(prop: "Customer").ToList();
            return new JsonResult(data);
        }
        public IActionResult Addbill()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Addbill(BillClass bill)
        {
            if (ModelState.IsValid)
            {
                _db.Bill.Add(bill);
                _db.Save();

                List<BillhasProductClass> one = _db.Billhasproduct.getSpecifics(u => u.Bill_id == null, prop: null).ToList();
                foreach (var data in one)
                {
                    data.Bill_id = bill.Id;
                    _db.Billhasproduct.Update(data);
                    List<StockClass> To_update = _db.Stock.getSpecifics(u => u.billhasProduct_id == data.Id, null).ToList();
                    foreach (var lita in To_update)
                    {
                        lita.Customer_id = bill.Customer_id;
                        lita.InStock = "N";
                        _db.Stock.Update(lita);
                    }
                }
                _db.Save();
                return View();
            }
            TempData["error"] = "The bill Description was not valid";
            return RedirectToAction("CreateBill");
        }

        [HttpPost]
        public IActionResult addCom(BillhasProductVM obj)
        {
            obj.Class.total= obj.Class.Rate * obj.Class.Quantity;
            obj.Class.User_id=User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool success1;
            string message1;
            if(ModelState.IsValid)
            {
                try
                {
                    List<string> serial_no = _db.Stock.getSpecifics(u => u.serial_number != null, null).Select(u => u.serial_number).ToList();
                    if(obj.Serial_no.Contains(null))
                    {
                        throw new Exception("No Serial number is provided");
                    }
                    if (obj.Serial_no.Distinct().Count() != obj.Serial_no.Count())
                    {
                        throw new Exception("Serial no not unique");
                    }
                    foreach (var check in obj.Serial_no)
                    {
                        if (serial_no.Contains(check))
                        {
                            throw new Exception("Serial_no error not Unique");
                        }
                    }
                    
                    if (obj.Class.Id == 0)
                    {
                        int Instock_count = _db.Stock.getSpecifics(u => u.InStock == "Y" && u.Product_id == obj.Class.product_id && 
                        u.billhasProduct_id ==null, null).Count();
                        
                        if (Instock_count < obj.Class.Quantity)
                        {
                            throw new Exception("Not Enough Item avaibale in Stock");
                        }
                        List<StockClass> man = _db.Stock.getSpecifics(u => u.billhasProduct_id == null
                        && u.Product_id == obj.Class.product_id && u.InStock=="Y", null).Take(obj.Class.Quantity).ToList();
                        _db.Billhasproduct.Add(obj.Class);
                        _db.Save();
                        int count = 0;
                        foreach (var item in man)
                        {
                            item.billhasProduct_id = obj.Class.Id;
                            item.serial_number = obj.Serial_no[count].ToString();
                            count++;
                            _db.Stock.Update(item);
                        }
                        _db.Save();
                        success1 = true;
                        message1 = "Value Added";

                    }
                    else
                    {
                        List<StockClass> man = _db.Stock.getSpecifics(u=>u.billhasProduct_id ==obj.Class.Id, null).ToList();
                        int B_update = man.Count();

                        if(B_update>=obj.Class.Quantity)
                        {
                            int check = 0;
                            foreach(var  item in man) 
                            {
                                if(check<obj.Class.Quantity)
                                {

                                    item.serial_number = obj.Serial_no[check].ToString();
                                    check++;
                                }
                                else
                                {
                                    item.serial_number = null;
                                    item.billhasProduct_id = null;
                                }
                                _db.Stock.Update(item);
                            }
                        }
                        else 
                        { 
                            int additional_required= obj.Class.Quantity-B_update;
                            int Instock_count = _db.Stock.getSpecifics(u => u.InStock == "Y" && u.Product_id == obj.Class.product_id && u.billhasProduct_id == null, null).Count();
                            if (Instock_count < additional_required)
                            {
                                throw new Exception("Not Enough Item avaibale in Stock");
                            }
                            List<StockClass> newStock = _db.Stock.getSpecifics(u => u.billhasProduct_id == null
                            && u.Product_id == obj.Class.product_id && u.InStock == "Y", null).Take(additional_required).ToList();
                            man.AddRange(newStock);
                            var count = 0;
                            foreach(var item in man)
                            {
                                item.billhasProduct_id = obj.Class.Id;
                                item.serial_number = obj.Serial_no[count];
                                _db.Stock.Update(item);
                                count++;
                            }
                        }
                        _db.Billhasproduct.Update(obj.Class);
                        success1 = true;
                        message1 = "Value Updated";
                    }
                    _db.Save();
                    return Json(new
                    {
                        success = success1,
                        message = message1
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
                return Json(
                    new
                    {
                        success =false,
                        message="Modal state not valid"
                    });
            }
        }
    }
}
