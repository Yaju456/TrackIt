using Microsoft.EntityFrameworkCore;
using TrackIt.Data;
using TrackIt.Models;
using TrackIt.Repository.Irepository;

namespace TrackIt.Repository
{
    public class BillRepo : MainRepo<BillClass>, Ibill
    {
        private readonly DbSet<BillClass> _billSet;
        public BillRepo(Applicationdbcontext db) : base(db)
        {
            _billSet = db.Set<BillClass>();
        }

        public void Update(BillClass bill)
        {
            BillClass billClass= _billSet.FirstOrDefault(u=>u.Id==bill.Id);
            if(billClass != null ) 
            {
                billClass.Description = bill.Description;
                billClass.total = bill.total;
                billClass.Customer_id = bill.Customer_id;
                _billSet.Update(billClass);
            }
        }
    }
}
