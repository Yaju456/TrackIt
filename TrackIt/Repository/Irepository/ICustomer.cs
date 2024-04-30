using TrackIt.Models;

namespace TrackIt.Repository.Irepository
{
    public interface ICustomer: Imainrepo<CustomerClass>
    {
        public void Update(CustomerClass customer);
    }
}
