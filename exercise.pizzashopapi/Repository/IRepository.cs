using exercise.pizzashopapi.Models;

namespace exercise.pizzashopapi.Repository
{
    public interface IRepository
    {
        Task  CreateOrder(Order order);
        Task<Order> GetOrderByCustomer(string CustomerName);
        Task <List<Order>> GetAllOrders();
        Task <Order> GetOrderByCustomerId(int id);
        Task<List<Pizza>> GetPizzas();
        Task <Pizza> GetPizzaById(int id);
        Task <Order>UpdateOrder(Order order, int id);

        Task DeleteOrderById(int id);
        Task <List<Customer>> GetAllCustomers();

        

    }
}
