using TrackIt.Models;

namespace TrackIt.Repository.Irepository
{
    public interface Iorderhasproduct: Imainrepo<OrderhasProducts>
    {
        public void Update(OrderhasProducts product);
    }
}
