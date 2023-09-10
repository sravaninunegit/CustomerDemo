using CustomerData.Models;

namespace CustomerAPi.DataRepostitories
{
    public interface IDataService
    {
        IEnumerable<Customers> GetCustomers();
        IEnumerable<OrderItems> GetOrderItems();
        IEnumerable<Order> GetOrders();
    }
}
