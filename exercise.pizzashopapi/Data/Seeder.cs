using exercise.pizzashopapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.pizzashopapi.Data
{
    public static class Seeder
    {
        public async static void SeedPizzaShopApi(this WebApplication app)
        {
            using (var db = new DataContext())
            {
                if(!db.Customers.Any())
                {
                    
                    db.Add(new Customer() { Name="Nigel" });
                    db.Add(new Customer() { Name = "Dave" });
                    db.Add(new Customer() { Name = "Ibbi"});
                    await db.SaveChangesAsync();
                }
                if(!db.Pizzas.Any())
                {
                    db.Add(new Pizza() { Name = "Cheese & Pineapple" });
                    db.Add(new Pizza() { Name = "Vegan Cheese Tastic" });
                    db.Add(new Pizza() { Name = "Pepperoni" });
                    await db.SaveChangesAsync();

                }

                //order data
                if(!db.Orders.Any())
                {
                    db.Add(new Order() { PizzaId = 1, CustomerId = 1, OrderTime = DateTime.UtcNow.AddMinutes(40)}); 
                    db.Add(new Order() { PizzaId = 2, CustomerId = 2, OrderTime = DateTime.UtcNow.AddMinutes(80)});
                    db.Add(new Order() { PizzaId = 3, CustomerId = 3, OrderTime = DateTime.UtcNow.AddMinutes(-10)});
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}
