using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TrackIt.Data;
using TrackIt.Models;
using TrackIt.Repository.Irepository;

namespace TrackIt.Repository
{
    public class VendorRepo : MainRepo<VendorClass>, IVendor
    {
        private readonly Applicationdbcontext _context;
        private readonly DbSet<VendorClass> _versions;
        public VendorRepo(Applicationdbcontext db): base(db)
        {
            _context = db;
            _versions = db.Set<VendorClass>();
        }
        
        public void Update(VendorClass obj)
        {
            throw new NotImplementedException();
        }
    }
}
