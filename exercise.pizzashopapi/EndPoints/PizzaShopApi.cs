using exercise.pizzashopapi.Data;
using exercise.pizzashopapi.Models;
using exercise.pizzashopapi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace exercise.pizzashopapi.EndPoints
{
    public static class PizzaShopApi
    {

        public static void ConfigurePizzaShopApi(this WebApplication app)
        {
            var PizzaShop = app.MapGroup("pizza");


            
            PizzaShop.MapGet("/orders", GetOrders);

            
            PizzaShop.MapGet("/orders/customer/{customerId}", GetOrderByCustomerId);

            
            PizzaShop.MapPost("/orders", CreateOrder);

            
            PizzaShop.MapGet("/", GetPizzas);

            
            PizzaShop.MapGet("/{id}", GetPizzaById);

            
            PizzaShop.MapPut("/orders/{id}", UpdateOrder);

            
            PizzaShop.MapDelete("/orders/{id}", DeleteOrderById);

            
            PizzaShop.MapGet("/customers", GetAllCustomers);


        }
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async static Task<IResult> GetOrders(IRepository repository)

        {
            try
            {

                var orders = await repository.GetAllOrders();
                var results = new List<OrderDTO>();
                foreach (var order in orders) { 
                
                    OrderDTO orderDTO = new OrderDTO();
                    orderDTO.Customer = order.Customer.Name;
                    orderDTO.Pizza = order.Pizza.Name;
                    orderDTO.OrderTime = order.OrderTime;
                    results.Add(orderDTO);
                }


                return TypedResults.Ok(results);
            }
            catch (Exception)
            {

                return TypedResults.Problem("Error retrieving orders");

            }

        }
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async static Task<IResult> GetOrderByCustomerId(IRepository repository, int customerId)
        {
            try
            {
                var Customer = await repository.GetOrderByCustomerId(customerId);
                OrderDTO orderDTO = new OrderDTO();
                {
                    orderDTO.Customer = Customer.Customer.Name;
                    orderDTO.Pizza = Customer.Pizza.Name;
                    orderDTO.OrderTime = Customer.OrderTime;
                }

                return TypedResults.Ok(orderDTO);
            }
            catch (Exception)
            {

                return TypedResults.NotFound("Order for Customer does not exist");

            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]


        public async static Task<IResult> CreateOrder(IRepository repository, Order order)
        {
            try
            {
                await repository.CreateOrder(order);
                return TypedResults.Ok(order);
            }
            catch (Exception)
            {

                return TypedResults.BadRequest("Not created");
            }

        }       
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async static Task<IResult> GetPizzas(IRepository repository)
        {
            return TypedResults.Ok(await  repository.GetPizzas());
        }
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async static Task<IResult> GetPizzaById(IRepository repository, int id)
        {
            try
            {

            var pizza = await repository.GetPizzaById(id);
            return TypedResults.Ok(pizza);
            }
            catch (Exception)
            {

                return TypedResults.Problem("Error getting pizzas");
            }

        }
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async static Task<IResult> UpdateOrder(IRepository repository, Order order, int id)
        {

            try
            {
            var orders = await repository.GetAllOrders();
            var chosenOrder = orders.FirstOrDefault(o => o.Id == id);

            var customers = await repository.GetAllCustomers();
            var chosenCustomer = customers.FirstOrDefault(x => x.Id == order.CustomerId);

            var pizzas = await repository.GetPizzas();
            var chosenPizza =  pizzas.FirstOrDefault(x => x.Id == order.PizzaId);

            chosenPizza = order.Pizza;
            chosenCustomer = order.Customer;

            chosenOrder.Pizza = chosenPizza;
            chosenOrder.Customer = chosenCustomer;
            chosenOrder.OrderTime = order.OrderTime;
            return TypedResults.Ok(chosenOrder);
            }
            catch (Exception)
            {

                return TypedResults.NotFound($"Pizza with ID {id} not found.");
            }



        }

        
    [ProducesResponseType(StatusCodes.Status200OK)]


    public async static Task<IResult> DeleteOrderById(IRepository repository, int id)
    {
        try
        {
            var TargetOrder = repository.DeleteOrderById(id);
            return TypedResults.Ok(TargetOrder);

        }
        catch (Exception)
        {

                return TypedResults.NotFound($"Order with ID {id} not found.");
            }
    }

        [ProducesResponseType(StatusCodes.Status200OK)]

        public async static Task<IResult> GetAllCustomers(IRepository repository)
        {
            try
            {
                return TypedResults.Ok(await repository.GetAllCustomers());

            }
            catch (Exception)
            {

                return TypedResults.Problem("Error getting all customers");
            }
        }

    





    }
}
