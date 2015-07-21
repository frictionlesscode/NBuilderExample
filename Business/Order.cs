using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime DelieveryDate { get; set; }

        public string TrackingNumber { get; set; }

        public IList<OrderItem> OrderItems { get; set; }
    }
}
