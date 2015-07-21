using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class OrderItem
    {
        public OrderItem(string productName)
        {
            ProductName = productName;
        }

        public int Id { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public float TotalCost { get; set; }
    }
}
