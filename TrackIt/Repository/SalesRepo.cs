using Microsoft.EntityFrameworkCore;
using TrackIt.Data;
using TrackIt.Models;
using TrackIt.Repository.Irepository;

namespace TrackIt.Repository
{
    public class SalesRepo : MainRepo<SalesClass>, ISales
    {
        private readonly Applicationdbcontext _db;
        private readonly DbSet<SalesClass> _sales;
        public SalesRepo(Applicationdbcontext db): base(db)
        {
            _db = db;
            _sales = db.Set<SalesClass>();
        }
        public void Update(SalesClass sale)
        {
            SalesClass obj= _sales.FirstOrDefault(u=>u.Id==sale.Id);
            if (obj!=null) 
            {
                obj.Product_id= sale.Product_id;
                obj.Client_id=sale.Client_id;
                obj.Sales_Date=sale.Sales_Date;
                obj.Rate=sale.Rate;
                obj.Quantity=sale.Quantity;
            }
        }
    }
}
