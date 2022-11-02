namespace RestrauntServer.Services
{
    using Microsoft.EntityFrameworkCore;
    using RestrauntServer.Data;
    using RestrauntServer.Enums;
    using RestrauntServer.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdminService
    {
        private readonly RestrauntDb _context;

        public AdminService(RestrauntDb context)
        {
            _context = context;
        }

        public async Task<bool> PushToNextStatus(int orderId)
        {
            var order = await _context.Order.Where(x=>x.Id==orderId).FirstOrDefaultAsync();
            if (order != null)
            {
                switch (order.Status)
                {
                    case Status.Created:
                        order.Status = Status.Coocking;
                        break;
                    case Status.Coocking:
                        order.Status = order.IsDelivery ? Status.Delivering : Status.Done;
                        break;
                    case Status.Done:
                        order.Status = Status.Closed;
                        break;
                    default:
                        return false;
                }

                 _context.Order.Update(order);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Order.Include(x=>x.dishPunkts).ToListAsync();
        }

    }
}
