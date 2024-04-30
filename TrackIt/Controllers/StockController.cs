using Microsoft.AspNetCore.Mvc;
using TrackIt.Models;
using TrackIt.Repository.Irepository;

namespace TrackIt.Controllers
{
    public class StockController : Controller
    {
        private readonly IunitOfwork _db;
        public StockController(IunitOfwork db)
        {
            _db = db;
        }

        public JsonResult getAll()
        {
            List<StockClass> stockClasses = _db.Stock.getAll(prop: "Order,Client,Product,Sales").ToList();
            return new JsonResult(stockClasses);
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
