namespace GunShop.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int GunId { get; set; }
        public Gun Gun { get; set; }
        public int Quantity { get; set; }
    }
}
