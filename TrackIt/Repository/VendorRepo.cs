﻿using Microsoft.EntityFrameworkCore;
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
            VendorClass To_update= _versions.FirstOrDefault(u=>u.Id==obj.Id);
            if (To_update != null)
            {
                To_update.Name = obj.Name;
                To_update.Description = obj.Description;    
                To_update.PhoneNumber = obj.PhoneNumber;
                _versions.Update(To_update);
            }

        }
    }
}
