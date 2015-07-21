using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business;
using FluentAssertions;
using FizzWare.NBuilder;

namespace NBuilderExample
{
    [TestClass]
    public class OrderServiceTests
    {
        [TestMethod]
        public void OrdersCanBeCreated()
        {
            var expected = new Order();

            var sut = new OrderService();
            var actual = sut.PlaceOrder(expected);

            actual.Id.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void OrdersCanBeCreatedWithNBuilder()
        {
            var expected = Builder<Order>.CreateNew().Build();

            var sut = new OrderService();
            var actual = sut.PlaceOrder(expected);

            actual.Id.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void OrdersHaveUpsTrackingNumber()
        {
            var expected = Builder<Order>.CreateNew().With(x => x.TrackingNumber = "1Z").Build();

            var sut = new OrderService();
            var actual = sut.PlaceOrder(expected);

            actual.TrackingNumber.Should().StartWith(expected.TrackingNumber);
        }

        [TestMethod]
        public void OrderShouldHaveOrderDateCloseToNow()
        {
            var expected = Builder<Order>.CreateNew().
                With(x => x.OrderDate = DateTime.Now).
                Build();

            var sut = new OrderService();
            var actual = sut.PlaceOrder(expected);

            actual.OrderDate.Should().BeCloseTo(expected.OrderDate);
        }

        [TestMethod]
        public void OrderShouldHaveDelieveryDate2DaysFromOrderDate()
        {
            var expected = Builder<Order>.CreateNew().
                With(x => x.OrderDate = DateTime.Now).
                With(x => x.DelieveryDate = x.OrderDate.AddDays(2)).
                Build();

            var sut = new OrderService();
            var actual = sut.PlaceOrder(expected);

            actual.DelieveryDate.Should().BeCloseTo(expected.DelieveryDate);
        }

        [TestMethod]
        public void OrdersShouldHaveOrderItemsWithProductNames()
        {
            var orderItems = Builder<OrderItem>.CreateListOfSize(2).
                TheFirst(1).WithConstructor(() => new OrderItem("Amazon Echo")).
                TheLast(1).WithConstructor(() => new OrderItem("Amazon Fire")).
                Build();

            var expected = Builder<Order>.CreateNew().With(x => x.OrderItems = orderItems).Build();

            var sut = new OrderService();
            var actual = sut.PlaceOrder(expected);

            actual.OrderItems.Select(x => x.ProductName).ShouldAllBeEquivalentTo(expected.OrderItems.Select(x => x.ProductName));
        }

        [TestMethod]
        public void OrdersShouldHaveOrderItemsWithTotalCostCalculated()
        {
            var orderItems = Builder<OrderItem>.CreateListOfSize(2).
                TheFirst(1).WithConstructor(() => new OrderItem("Amazon Echo"))
                    .With(x => x.TotalCost = 179 * x.Quantity).
                TheLast(1).WithConstructor(() => new OrderItem("Amazon Fire"))
                    .With(x => x.TotalCost = 119 * x.Quantity).Build();

            var expected = Builder<Order>.CreateNew().With(x => x.OrderItems = orderItems).Build();

            var sut = new OrderService();
            var actual = sut.PlaceOrder(expected);

            actual.OrderItems.Select(x => x.TotalCost).ShouldAllBeEquivalentTo(expected.OrderItems.Select(x => x.TotalCost));
        }
    }
}
