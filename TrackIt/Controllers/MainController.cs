using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = Roll.Admin)]


    public class MainController : Controller
    {
        private readonly IunitOfwork _db;
        public MainController(IunitOfwork db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Addvendor() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Addvendor(VendorClass obj)
        {
            if(ModelState.IsValid) 
            {
                _db.Vendor.Add(obj);
                _db.Save();
                return Json(new { Data=_db.Vendor.getAll(prop:"").ToList()});
            }
            else
            { return View(obj); }
        }

        public IActionResult AddClient()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddClient(ClinetClass one)
        {
            if(ModelState.IsValid) 
            {
                _db.Client.Add(one);
                _db.Save();
                return Json(new { Data = _db.Client.getAll(prop: "").ToList() });
            }
            else
            { 
                return View(one); 
            }
        }
    }
}
