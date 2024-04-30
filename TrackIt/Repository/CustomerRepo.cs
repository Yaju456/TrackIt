using TrackIt.Data;
using TrackIt.Models;
using TrackIt.Repository.Irepository;

namespace TrackIt.Repository
{
    public class CustomerRepo : MainRepo<CustomerClass>, ICustomer
    {
        private readonly Applicationdbcontext _db;
        public CustomerRepo(Applicationdbcontext db):base(db)
        {
            _db = db;
        }
        public void Update(CustomerClass customer)
        {
            
        }
    }
}
