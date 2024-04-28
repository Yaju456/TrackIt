using TrackIt.Models;

namespace TrackIt.Repository.Irepository
{
    public interface ISales: Imainrepo<SalesClass>
    {
        void Update(SalesClass sale);
    }
}
