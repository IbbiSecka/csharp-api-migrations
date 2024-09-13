using exercise.pizzashopapi.Data;
using exercise.pizzashopapi.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace exercise.pizzashopapi.Repository
{
    public class Repository : IRepository
    {
        private DataContext _db;

        public async Task<List<Order>> GetAllOrders()
        {
            return await _db.Orders
                .Include(x => x.Pizza)
                .Include(x => x.Customer)
                .ToListAsync();
        }

        public async Task <Order> GetOrderByCustomerId(int id)
        {
            return await _db.Orders
                .Include(x => x.Pizza)
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.CustomerId == id);
                
        }

        public async Task<Order> GetOrderByCustomer(string CustomerName)
        {
            return await _db.Orders
                .Include(x => x.Pizza)
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.Customer.Name == CustomerName);
        }

        public async Task<Pizza> GetPizzaById(int id)
        {
            return await _db.Pizzas
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Pizza>> GetPizzas()
        {
            return await _db.Pizzas .ToListAsync();
        }
      
        public async Task CreateOrder(Order order)
        {
            
            await _db.Orders.AddAsync(order);
            
        }

        public async Task<Order> UpdateOrder(Order order, int id)
        {
            Order TargetOrder = await _db.Orders.FirstOrDefaultAsync(x => x.Id == id);
            TargetOrder.OrderTime = order.OrderTime;
            TargetOrder.Pizza = order.Pizza;
            TargetOrder.Customer = order.Customer;

            return TargetOrder;
        }

        public async Task DeleteOrderById(int id)
        {
            var chosenOrder = await _db.Orders.FirstOrDefaultAsync(x => x.Id == id);
             _db.Remove(chosenOrder);
        }

        public async Task <List<Customer>>  GetAllCustomers()
        {
            return await _db.Customers.ToListAsync();
        }
       
    }
}
