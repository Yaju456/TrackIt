namespace TrackIt.Repository.Irepository
{
    public interface IunitOfwork
    {
        public IClient? Client { get;}
        public ICustomer? customer { get;}
        public IOrder? Order { get;}
        public Iproduct? Product { get;}
        public IStock? Stock { get;}
        public IVendor? Vendor { get;}

        void Save();
    }
}
