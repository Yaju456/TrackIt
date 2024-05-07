using TrackIt.Models;

namespace TrackIt.Repository.Irepository
{
    public interface IbillhasProduct: Imainrepo<BillhasProductClass>
    {
        void Update(BillhasProductClass obj);
    }
}
