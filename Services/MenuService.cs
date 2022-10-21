namespace RestrauntServer.Services
{
    using RestrauntServer.Data;
    using RestrauntServer.Models;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class MenuService
    {
        private readonly RestrauntDb _context;

        public MenuService(RestrauntDb restrauntDb)
        {
            _context = restrauntDb;
        }

        public async Task<IEnumerable<Dish>> GetMenu()
        {
            return await _context.Dish.ToListAsync();
        }

        public async Task<Dish> GetDishDetails(int id)
        {
            return await _context.Dish.FindAsync(id);
        }
    }
}
