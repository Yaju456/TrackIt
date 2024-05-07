using TrackIt.Models;

namespace TrackIt.Repository.Irepository
{
    public interface Ibill: Imainrepo<BillClass>
    {
        public void Update(BillClass bill);
    }
}
