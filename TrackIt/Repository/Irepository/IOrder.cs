using TrackIt.Models;

namespace TrackIt.Repository.Irepository
{
    public interface IOrder:Imainrepo<OrderClass>
    {
        void Update(OrderClass order);
        public IEnumerable<OrderClass> NewGetall();

    }
}
