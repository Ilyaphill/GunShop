namespace GunShop.Models
{
    public class Gun
    {
        public int Id { get; set; } // Уникальный идентификатор гана
        public string Model { get; set; } // Модель гана
        public string Manufacturer { get; set; } // Производитель гана
        public string Type { get; set; } // Тип гана (пистолет, винтовка и т.д.)
        public string Caliber { get; set; } // Калибр гана
        public decimal Price { get; set; } // Цена гана
        public double Weight { get; set; } // Вес гана в килограммах
        public int MagazineCapacity { get; set; } // Вместимость магазина гана
        public string Description { get; set; } // Описание гана
    }
}
