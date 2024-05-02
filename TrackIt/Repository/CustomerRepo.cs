using Microsoft.EntityFrameworkCore;
using TrackIt.Data;
using TrackIt.Models;
using TrackIt.Repository.Irepository;

namespace TrackIt.Repository
{
    public class CustomerRepo : MainRepo<CustomerClass>, ICustomer
    {
        private readonly Applicationdbcontext _db;
        private readonly DbSet<CustomerClass> _dbSet;
        public CustomerRepo(Applicationdbcontext db):base(db)
        {
            _db = db;
            _dbSet= db.Set<CustomerClass>();
        }
        public void Update(CustomerClass customer)
        {
            CustomerClass ToUpdate = new CustomerClass();
            ToUpdate=_dbSet.FirstOrDefault(u=>u.Id==customer.Id);
            if(ToUpdate!=null) 
            {
                ToUpdate.Name= customer.Name;
                ToUpdate.DistrictId= customer.DistrictId;
                ToUpdate.ProvinceId= customer.ProvinceId;
                ToUpdate.LocalBodyId= customer.LocalBodyId;
                _dbSet.Update(ToUpdate);
            }
        }
    }
}
