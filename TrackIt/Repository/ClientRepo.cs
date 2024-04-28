using Microsoft.EntityFrameworkCore;
using TrackIt.Data;
using TrackIt.Models;
using TrackIt.Repository.Irepository;

namespace TrackIt.Repository
{
    public class ClientRepo : MainRepo<ClinetClass>, IClient
    {
        private readonly Applicationdbcontext _db;
        DbSet<ClinetClass> _clients;
        public ClientRepo(Applicationdbcontext db): base(db)
        {
            _db = db;
            _clients = db.Set<ClinetClass>();
        }
        public void Update(ClinetClass clinet)
        {
            ClinetClass one = _clients.FirstOrDefault(a=>a.Id == clinet.Id);
            if (one != null) 
            {
                one.Description = clinet.Description;
                one.Name = clinet.Name;
                _clients.Update(one);
            }
        }
    }
}
