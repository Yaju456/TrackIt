using TrackIt.Models;

namespace TrackIt.Repository.Irepository
{
    public interface IClient: Imainrepo<ClinetClass>
    {
        void Update(ClinetClass clinet);
    }
}
