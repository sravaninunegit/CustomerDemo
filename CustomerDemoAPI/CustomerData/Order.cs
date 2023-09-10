

namespace CustomerData.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string OrderStatus { get; set; }
        public string OrderDate { get; set; }
        public string RequiredDate { get; set; }
        public string ShippedDate { get; set; }
        
              

    }
}
