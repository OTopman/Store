using System;
using BookStore.Models;

namespace BookStore.Services
{
	public class OrderRepository : IOrderRepository
	{

        private readonly StoreDbContext Repository;
		public OrderRepository(StoreDbContext dbContext)
		{
            Repository = dbContext;
		}

        public Order GetOrder(string orderUuid)
        {
            return Repository.Orders.FirstOrDefault(ord => ord.OrderUuid == orderUuid)!;
        }

        public IEnumerable<Order> GetOrders()
        {
            return Repository.Orders;
        }

        public IEnumerable<Order> GetUserOrders(string registrationUuid)
        {
            return Repository.Orders.Where(ord => ord.RegistrationUuid == registrationUuid);
        }

        public bool UpdateOrder(Order order)
        {
            // Check if order exist
            Order order1 = this.GetOrder(orderUuid: order.OrderUuid);
            //if(order1 is null)
            //{
            //    throw new Exception("Order not found.");
            //}

            // Update record
            order1.Status = order.Status != null ? order.Status : order1.Status;
            order.Amount = order.Amount > 0 ? order.Amount : order1.Amount;
            order.UpdatedAt = DateTime.Now;
            Repository.Orders.Update(order);
            return Repository.SaveChanges() > 0;

        }
    }
}

