﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using TrackIt.Data;
using TrackIt.Models;
using TrackIt.PreData;
using TrackIt.Repository.Irepository;

namespace TrackIt.Controllers
{
   // [Authorize(Roles = Roll.Admin)]

    public class ProductController : Controller
    {
        private readonly IunitOfwork _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IunitOfwork db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
      

        public JsonResult GetAll()
        {
            List<ProductClass> Products = _db.Product.getAll(prop: null).ToList();
            foreach(var data in Products)
            {
                data.In_stock = _db.Stock.getSpecifics(u => u.Product_id == data.Id && u.InStock=="Y", null).Count();
                _db.Product.Update(data);
            }
            _db.Save();
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
        public IActionResult Index(ProductClass obj, IFormFile? file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;//for www root folder path

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string Productpath = Path.Combine(wwwRootPath, @"Images\Product");
                if (!string.IsNullOrEmpty(obj.ImgUrl))
                {
                    //delete old image 
                    var oldimage = Path.Combine(wwwRootPath, obj.ImgUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldimage))
                    {
                        System.IO.File.Delete(oldimage);
                    }
                }
                using (var filestream = new FileStream(Path.Combine(Productpath, fileName), FileMode.Create))
                {
                    file.CopyTo(filestream);
                }

                obj.ImgUrl= @"\Images\Product\" + fileName;
            }

            if (ModelState.IsValid)
            {


                _db.Product.Add(obj);
                _db.Save();
                return RedirectToAction("Index");
                
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
                string wwwRootPath = _webHostEnvironment.WebRootPath;//for www root folder path
                string Productpath = Path.Combine(wwwRootPath, @"Images\Product");
                if (!string.IsNullOrEmpty(To_delete.ImgUrl))
                {
                    //delete old image 
                    var oldimage = Path.Combine(wwwRootPath, To_delete.ImgUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldimage))
                    {
                        System.IO.File.Delete(oldimage);
                    }
                }
               


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
