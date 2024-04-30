using Microsoft.EntityFrameworkCore;
using TrackIt.Data;
using TrackIt.Models;
using TrackIt.Repository.Irepository;

namespace TrackIt.Repository
{
    public class StockRepo : MainRepo<StockClass>, IStock
    {
        private readonly Applicationdbcontext _db;
        private readonly DbSet<StockClass> _stock;
        public StockRepo(Applicationdbcontext db):base(db)
        {
            _db = db;
            _stock= db.Set<StockClass>();
        }
        public void Update(StockClass stock)
        {
            StockClass one = _stock.FirstOrDefault(a=>a.Id==stock.Id);
            if (one!=null) 
            {
                one.Order_id=stock.Order_id;
                one.Client_id=stock.Client_id;
                one.Product_id=stock.Product_id;
            }
        }
    }
}
