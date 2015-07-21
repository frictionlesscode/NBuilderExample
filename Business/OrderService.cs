using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class OrderService : IOrderService
    {
        public Order PlaceOrder(Order order)
        {
            // TODO DAL/Webservice.  Just fake that here

            var newOrder = new Order();
            newOrder.Id = 1;
            newOrder.OrderDate = DateTime.Now;
            newOrder.DelieveryDate = newOrder.OrderDate.AddDays(2);
            newOrder.TrackingNumber = "1Z9999999999999999";

            if (order.OrderItems != null)
            {
                int i=1;
                foreach (var orderItem in order.OrderItems)
                {
                    var newOrderItem = new OrderItem(orderItem.ProductName) { Quantity = orderItem.Quantity };
                    newOrderItem.Id = i++;
                    newOrderItem.TotalCost = GetTotalCost(GetProductCost(orderItem.ProductName), orderItem.Quantity);
                    newOrder.OrderItems.Add(newOrderItem);
                }
            }

            return newOrder;
        }

        private float GetProductCost(string productName)
        {
            if (productName == "Amazon Echo")
                return 179f;
            if (productName == "Amazon Kindle")
                return 119f;
            if (productName == "Amazon Fire")
                return 119f;

            throw new ArgumentException("Unable to price unknown product");
        }

        private float GetTotalCost(float price, int quanity)
        {
            return price * quanity; 
        }
    }

    public interface IOrderService
    {
        Order PlaceOrder(Order order);
    }
}
