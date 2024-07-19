namespace GunShop.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int GunId { get; set; }
        public Gun Gun { get; set; }
        public int Quantity { get; set; }
    }
}
