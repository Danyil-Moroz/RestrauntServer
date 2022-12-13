namespace RestrauntServer.Services
{
    using RestrauntServer.Data;
    using RestrauntServer.Models;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System.Linq;

    public class MenuService
    {
        private readonly RestrauntDb _context;

        public MenuService(RestrauntDb restrauntDb)
        {
            _context = restrauntDb;
        }

        public async Task<IEnumerable<Dish>> GetMenu()
        {
            var result = new List<Dish>();
            foreach (var item in await _context.Dish.Include(x => x.Category).ToListAsync())
            {
                item.Category = await _context.Category.Where(x => x.Id == item.CategoryId).FirstOrDefaultAsync();
                result.Add(item);
            }
            return result;
        }

        public async Task<Dish> GetDishDetails(int id)
        {
            return await _context.Dish.Include(x => x.Category).Where(x=>x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Category.ToListAsync();
        }
    }
}
