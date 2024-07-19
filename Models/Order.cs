namespace GunShop.Models
{
    public class Order
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CardNumber { get; set; }
        public DateTime OrderDate { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}

