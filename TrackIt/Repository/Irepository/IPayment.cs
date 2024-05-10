using TrackIt.Models;

namespace TrackIt.Repository.Irepository
{
    public interface IPayment: Imainrepo<PaymentClass>
    {
        void Update(PaymentClass payment);
    }
}
