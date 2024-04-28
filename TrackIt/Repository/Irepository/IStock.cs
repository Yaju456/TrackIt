using TrackIt.Models;

namespace TrackIt.Repository.Irepository
{
    public interface IStock: Imainrepo<StockClass>
    {
        void Update(StockClass stock);
    }
}
