using TrackIt.Data;
using TrackIt.Repository.Irepository;

namespace TrackIt.Repository
{
    public class UnitofWork : IunitOfwork
    {
        private readonly Applicationdbcontext _db;
        public IClient? Client { get; private set; }

        public IOrder? Order { get; private set; }

        public Iproduct? Product { get; private set; }
        public ISales? sales { get; private set; }

        public IStock? Stock { get; private set; }

        public IVendor? Vendor { get; private set; }

        public UnitofWork(Applicationdbcontext db)
        {
            _db = db;
            Client= new ClientRepo(db);
            Order= new OrderRepo(db);
            Product= new ProductRepo(db);
            sales = new SalesRepo(db);
            Stock= new StockRepo(db);
            Vendor= new VendorRepo(db);

        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
