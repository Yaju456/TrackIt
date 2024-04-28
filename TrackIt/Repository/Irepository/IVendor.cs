using TrackIt.Models;

namespace TrackIt.Repository.Irepository
{
    public interface IVendor:Imainrepo<VendorClass>
    {
        void Update(VendorClass obj);
    }
}
