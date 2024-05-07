using TrackIt.Models;

namespace TrackIt.Repository.Irepository
{
    public interface IBucket:Imainrepo<BucketClass>
    {
        void Update(BucketClass bucket);
    }
}
