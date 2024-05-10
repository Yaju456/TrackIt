using Microsoft.EntityFrameworkCore;
using TrackIt.Data;
using TrackIt.Models;
using TrackIt.Repository.Irepository;

namespace TrackIt.Repository
{
    public class PaymentRepo : MainRepo<PaymentClass>, IPayment
    {
        private readonly DbSet<PaymentClass> _paymentClasses;
        public PaymentRepo(Applicationdbcontext db) : base(db)
        {
            _paymentClasses = db.Set<PaymentClass>();   
        }

        public void Update(PaymentClass payment)
        {
            PaymentClass obj = _paymentClasses.FirstOrDefault(u => u.Id == payment.Id);
            if (obj != null) 
            {
                obj.Bill_id = payment.Bill_id;
                obj.Date = payment.Date;
                obj.Method = payment.Method;
                obj.Amount = payment.Amount;
                _paymentClasses.Update(obj);
            }
        }
    }
}
