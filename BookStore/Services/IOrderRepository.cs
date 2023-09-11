using System;
using BookStore.Models;

namespace BookStore.Services
{
	public interface IOrderRepository
	{
		IEnumerable<Order> GetOrders();
		IEnumerable<Order> GetUserOrders(string registrationUuid);
		Order GetOrder(string orderUuid);
		bool UpdateOrder(Order order);
	}
}

