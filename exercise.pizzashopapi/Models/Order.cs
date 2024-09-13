using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.pizzashopapi.Models
{
    public class Order
    {
        public int Id { get; set; }
      
        public int PizzaId { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
        public Pizza Pizza { get; set; }
        public DateTime OrderTime { get; set; }
        public string StatusOfOrder { get; set; }
        public static string GetOrderStatus(Order order)
        {
            var elapsedTime = DateTime.Now - order.OrderTime;

            if (elapsedTime.TotalMinutes < 3)
            {
                return "Preparing";
            }
            else if (elapsedTime.TotalMinutes < 15) // 3 minutes of preparation + 12 minutes of cooking
            {
                return "Cooking";
            }
            else if (order.StatusOfOrder == "Delivered")
            {
                return "Delivered";
            }
            else
            {
                return "Ready"; // If it's been more than 15 minutes and not delivered
            }
        }

    }
}
