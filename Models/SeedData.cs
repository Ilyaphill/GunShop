using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GunShop.Data;
using GunShop.Models;
using System;
using System.Linq;

namespace GunShop.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new GunShopContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<GunShopContext>>()))
            {
                // Look for any guns.
                if (context.Gun.Any())
                {
                    return;   // DB has been seeded
                }

                context.Gun.AddRange(
                    new Gun
                    {
                        Model = "Colt M1911",
                        Manufacturer = "Colt",
                        Type = "Handgun",
                        Caliber = "45 ACP",
                        Price = 799.99M,
                        Weight = 1.1, // 1.1 kg
                        MagazineCapacity = 7,
                        Description = "A classic American handgun with a long history."
                    },
                    new Gun
                    {
                        Model = "Glock 17",
                        Manufacturer = "Glock",
                        Type = "Handgun",
                        Caliber = "9mm",
                        Price = 599.99M,
                        Weight = 0.75, // 0.75 kg
                        MagazineCapacity = 17,
                        Description = "A popular semi-automatic pistol known for its reliability."
                    },
                    new Gun
                    {
                        Model = "Remington 700",
                        Manufacturer = "Remington",
                        Type = "Rifle",
                        Caliber = "7mm",
                        Price = 999.99M,
                        Weight = 3.6, // 3.6 kg
                        MagazineCapacity = 5,
                        Description = "A bolt-action rifle popular among hunters."
                    },
                    new Gun
                    {
                        Model = "Benelli M4",
                        Manufacturer = "Benelli",
                        Type = "Shotgun",
                        Caliber = "12 gauge",
                        Price = 1299.99M,
                        Weight = 3.4, // 3.4 kg
                        MagazineCapacity = 6,
                        Description = "A tactical shotgun favored by law enforcement and military."
                    },
                    new Gun
                    {
                        Model = "Smith & Wesson M&P Shield",
                        Manufacturer = "Smith & Wesson",
                        Type = "Handgun",
                        Caliber = "9mm",
                        Price = 479.99M,
                        Weight = 0.68, // 0.68 kg
                        MagazineCapacity = 8,
                        Description = "A compact, concealable handgun ideal for self-defense."
                    },
                    new Gun
                    {
                        Model = "Sig Sauer P226",
                        Manufacturer = "Sig Sauer",
                        Type = "Handgun",
                        Caliber = "9mm",
                        Price = 849.99M,
                        Weight = 1.1, // 1.1 kg
                        MagazineCapacity = 15,
                        Description = "A full-sized pistol known for its accuracy and durability."
                    },
                    new Gun
                    {
                        Model = "Winchester Model 70",
                        Manufacturer = "Winchester",
                        Type = "Rifle",
                        Caliber = "30-06",
                        Price = 1149,
                        Weight = 3.4, // 3.4 kg
                        MagazineCapacity = 5,
                        Description = "A popular bolt-action rifle for hunting and target shooting."
                    },
                    new Gun
                    {
                        Model = "Mossberg 500",
                        Manufacturer = "Mossberg",
                        Type = "Shotgun",
                        Caliber = "12 gauge",
                        Price = 349.99M,
                        Weight = 3.2, // 3.2 kg
                        MagazineCapacity = 5,
                        Description = "A versatile shotgun known for its reliability and ruggedness."
                    },
                    new Gun
                    {
                        Model = "Beretta 92FS",
                        Manufacturer = "Beretta",
                        Type = "Handgun",
                        Caliber = "9mm",
                        Price = 699.99M,
                        Weight = 0.94, // 0.94 kg
                        MagazineCapacity = 15,
                        Description = "A military-grade pistol renowned for its accuracy and ergonomics."
                    },
                    new Gun
                    {
                        Model = "HK MP5",
                        Manufacturer = "Heckler & Koch",
                        Type = "Submachine Gun",
                        Caliber = "9mm",
                        Price = 2499.99M,
                        Weight = 2.5, // 2.5 kg
                        MagazineCapacity = 30,
                        Description = "A compact and reliable submachine gun used by military and law enforcement."
                    },
                    new Gun
                    {
                        Model = "FN SCAR 17S",
                        Manufacturer = "FN Herstal",
                        Type = "Rifle",
                        Caliber = "7.62mm",
                        Price = 3299.99M,
                        Weight = 3.6, // 3.6 kg
                        MagazineCapacity = 20,
                        Description = "A versatile battle rifle known for its accuracy and modularity."
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
