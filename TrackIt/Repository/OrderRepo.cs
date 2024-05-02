using Microsoft.EntityFrameworkCore;
using TrackIt.Data;
using TrackIt.Models;
using TrackIt.Repository.Irepository;

namespace TrackIt.Repository
{
    public class OrderRepo : MainRepo<OrderClass>, IOrder
    {
        private readonly Applicationdbcontext _db;
        private DbSet<OrderClass> _dbSet;
        public OrderRepo(Applicationdbcontext db): base(db)
        {
            _db = db;
            _dbSet = db.Set<OrderClass>();
        }
        public void Update(OrderClass order)
        {
            OrderClass two = _dbSet.FirstOrDefault(u=>u.Id == order.Id);
            if (two != null)
            {
                two.Arival=order.Arival;
                two.Product_id=order.Product_id;
                two.vendor=order.vendor;
                two.vendor_id=order.vendor_id;
                two.In_Stock=order.In_Stock;
                two.Quantity=order.Quantity;
                _dbSet.Update(two);
            }
        }

        public IEnumerable<OrderClass> NewGetall()
        {
            IQueryable<OrderClass> man= _dbSet.AsQueryable();
            man = man.Include(u => u.Product);
            man = man.Include("vendor");
            return man.ToList();
        }
    }
}
