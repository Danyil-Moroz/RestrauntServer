namespace RestrauntServer.Services
{
    using RestrauntServer.Data;
    using RestrauntServer.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System.Linq;

    public class CleintAccountService
    {
        private readonly RestrauntDb _context;

        public CleintAccountService(RestrauntDb restrauntDb)
        {
            _context = restrauntDb;
        }

        public async Task<Client> GetCLient(string email, string password)
        {
            return await _context.Client.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateClient(Client client)
        {
            var duplicateEmail = await _context.Client.Where(x => x.Email == client.Email).FirstOrDefaultAsync();
            if (duplicateEmail==null)
            {
                var item = new Client
                {
                    Email = client.Email,
                    Password = client.Password,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    IsAdmin = false,
                };
                await _context.Client.AddAsync(item);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
