namespace RestrauntServer.Services
{
    using Microsoft.EntityFrameworkCore;
    using RestrauntServer.Data;
    using RestrauntServer.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ClientORderService
    {
        private readonly RestrauntDb _dbContext;

        public ClientORderService(RestrauntDb dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersForUser(int id)
        {
            return await _dbContext.Order.Where(x=>x.ClientId == id).Include(x=>x.dishPunkts).ThenInclude(x=>x.Dish).ToListAsync();
        }

        public async Task<Order> GetOrderDetails(int clientId, int id)
        {
            return await _dbContext.Order.Include(x => x.dishPunkts).ThenInclude(x => x.Dish).Where(x=>x.Id==id && x.ClientId == clientId).FirstOrDefaultAsync();
        }

        public async  Task CreateOrder(Order order)
        {
            if (order.dishPunkts != null || order.dishPunkts.Count!=0)
            {
                var item = new Order()
                {
                    Status = Enums.Status.Created,
                    CreatedDate = System.DateTime.Now,
                    ClientId = order.ClientId,
                    Address = order.Address,
                    DeliveryDate = order.DeliveryDate,
                    IsDelivery = order.IsDelivery,
                    TableNumber = order.TableNumber
                };
                await _dbContext.Order.AddAsync(item);
                await _dbContext.SaveChangesAsync();
                var result = await _dbContext.Order.ToListAsync();
                var newOrderid = result.Last().Id;
                foreach (var dishItem in order.dishPunkts)
                {
                    var dishPunkt = new DishPunkt()
                    {
                        OrderId = newOrderid,
                        DishCount = dishItem.DishCount,
                        UsersNotes = dishItem.UsersNotes,
                        DishId = dishItem.DishId
                    };
                    var dish = await _dbContext.Dish.FindAsync(dishItem.DishId);
                    dish.OrderedCount += dishItem.DishCount;
                    _dbContext.Entry<Dish>(dish).State = EntityState.Modified;
                    await _dbContext.DishPunkts.AddAsync(dishPunkt);
                    await _dbContext.SaveChangesAsync();
                }
            }
           
        }
    }
}
