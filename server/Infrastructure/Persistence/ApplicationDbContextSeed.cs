﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Security;
using Domain.EFModels;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            #region Add Default Roles

            var roles = new[]
            {
                new IdentityRole(AuthorizeRoles.Admin),
                new IdentityRole(AuthorizeRoles.Customer),
                new IdentityRole(AuthorizeRoles.Seller),
                new IdentityRole(AuthorizeRoles.Shipper)
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }

            #endregion

            #region Add Default Users

            var team4 = new List<string>() { "Mo", "Kero", "Emad", "Hady" };

            if (!context.ApplicationUser.Any())
            {
                foreach (string name in team4)
                {
                    foreach (var role in roles)
                    {
                        var user = new ApplicationUser()
                        {
                            FirstName = name,
                            LastName = role.Name,
                            Balance = 5000,
                            IsActive = true,
                            PhoneNumber = "0123456789",
                            Email = $"{name + role}@gmail.com",
                            UserName = $"{name + role}@gmail.com",
                            ImageProfile = $"{name}.png"
                        };

                        // Add user and save Context
                        await userManager.CreateAsync(user, $"{name}@1234");

                        // Add To UserRoles table
                        await userManager.AddToRoleAsync(user, role.Name);
                        //_userRepo.Add(user);
                        switch (role.Name)
                        {
                            case AuthorizeRoles.Admin:
                                await context.Admins.AddAsync(new Admin() { User = user });
                                break;
                            case AuthorizeRoles.Customer:
                                await context.Customers.AddAsync(new Customer() { User = user });
                                break;
                            case AuthorizeRoles.Seller:
                                await context.Sellers.AddAsync(new Seller() { User = user });
                                break;
                            case AuthorizeRoles.Shipper:
                                await context.Shippers.AddAsync(new Shipper() { User = user });
                                break;
                        }

                        await context.SaveChangesAsync();

                        var address = new Address
                        {
                            User = user,
                            Street = "Fun",
                            City = $"city{user.FirstName + user.LastName}",
                            PostalCode = 1234,
                            IsPrimary = true,
                            AddressPhone = "0123456789"
                        };

                        var addressTwo = new Address
                        {
                            User = user,
                            Street = "Fun",
                            City = $"city{user.FirstName + user.LastName}",
                            PostalCode = 1234,
                            IsPrimary = false,
                            AddressPhone = "0123456789"
                        };

                        await context.Addresses.AddAsync(address);
                        await context.Addresses.AddAsync(addressTwo);
                        await context.SaveChangesAsync();
                    }
                }
            }

            #endregion
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary

            #region Seed Categories

            if (!context.Categories.Any())
            {
                var cat1 = new Category()
                {
                    Code = "electronics-and-mobiles",
                    Name = "Electronics & Mobiles",
                    NameArabic = "اجهزة كهربائية",
                    Description = "",
                    DescriptionArabic = "",
                    Image = new Image() { ImageName = "cat-electronics.png" },
                    IsTop = false,
                };
                await context.Categories.AddAsync(cat1);
                await context.SaveChangesAsync();

                var cat2 = new Category()
                {
                    Code = "television",
                    Name = "Televesion",
                    NameArabic = "اجهزة تلفاز",
                    Description = "",
                    DescriptionArabic = "",
                    IsTop = true,
                    ParentCat = cat1,
                    Brands = new List<Brand>()
                    {
                        new Brand()
                        {
                            IsTop = true,
                            Code = "samsung",
                            Name = "Samsung",
                            Image = "samsung.png"
                        },
                        new Brand()
                        {
                            IsTop = true,
                            Code = "sony",
                            Name = "Sony",
                            Image = "sony.png"
                        },
                        new Brand()
                        {
                            IsTop = true,
                            Code = "lg",
                            Name = "LG",
                            Image = "lg.png"
                        },
                        new Brand()
                        {
                            IsTop = true,
                            Code = "Xiaomi",
                            Name = "LG",
                            Image = "lg.png"
                        },
                    }
                };
                await context.Categories.AddAsync(cat2);
                await context.SaveChangesAsync();

                var cat3 = new Category()
                {
                    Code = "laptops",
                    Name = "Laptops",
                    NameArabic = "اجهزة محمول",
                    Description = "",
                    DescriptionArabic = "",
                    IsTop = true,
                    ParentCat = cat1,
                    Brands = new List<Brand>()
                    {
                        new Brand()
                        {
                            IsTop = true,
                            Code = "dell",
                            Name = "Dell",
                            Image = "dell.png"
                        },
                        new Brand()
                        {
                            IsTop = true,
                            Code = "lenovo",
                            Name = "Lenovo",
                            Image = "lenovo.png"
                        },
                        new Brand()
                        {
                            IsTop = true,
                            Code = "apple",
                            Name = "Apple",
                            Image = "apple.png"
                        },
                        new Brand()
                        {
                            IsTop = true,
                            Code = "msi",
                            Name = "Msi",
                            Image = "msi.png"
                        }
                    }
                };
                await context.Categories.AddAsync(cat3);
                await context.SaveChangesAsync();

                var cat4 = new Category()
                {
                    Code = "eg-all-audio",
                    Name = "Audio",
                    NameArabic = "سماعات",
                    Description = "",
                    DescriptionArabic = "",
                    IsTop = true,
                    ParentCat = cat1,
                };
                await context.Categories.AddAsync(cat4);
                await context.SaveChangesAsync();

                var cat5 = new Category()
                {
                    Code = "video-games",
                    Name = "Video Games",
                    NameArabic = "بلايستيشن",
                    Description = "",
                    DescriptionArabic = "",
                    IsTop = true,
                    ParentCat = cat1,
                };
                await context.Categories.AddAsync(cat5);
                await context.SaveChangesAsync();

                var cat6 = new Category()
                {
                    Code = "cameras",
                    Name = "Cameras",
                    NameArabic = "كاميرات",
                    Description = "",
                    DescriptionArabic = "",
                    IsTop = true,
                    ParentCat = cat1,
                    Brands = new List<Brand>()
                    {
                        new Brand()
                        {
                            IsTop = true,
                            Code = "canon",
                            Name = "Canon",
                            Image = "canon.png"
                        },
                    }
                };
                await context.Categories.AddAsync(cat6);
                await context.SaveChangesAsync();

                var cat7 = new Category()
                {
                    Code = "all-printers-eg",
                    Name = "Printers",
                    NameArabic = "كاميرات",
                    Description = "",
                    DescriptionArabic = "",
                    IsTop = true,
                    ParentCat = cat1,
                    Brands = new List<Brand>()
                    {
                        new Brand()
                        {
                            IsTop = true,
                            Code = "hp",
                            Name = "HP",
                            Image = "hp.png"
                        },
                    }
                };
                await context.Categories.AddAsync(cat7);
                await context.SaveChangesAsync();

                var cat8 = new Category()
                {
                    Code = "networking-products-16523",
                    Name = "Networks",
                    NameArabic = "كاميرات",
                    Description = "",
                    DescriptionArabic = "",
                    IsTop = true,
                    ParentCat = cat1,
                };
                await context.Categories.AddAsync(cat8);
                await context.SaveChangesAsync();

                var cat9 = new Category()
                {
                    Code = "data-storage",
                    Name = "Data Storage",
                    NameArabic = "وحدات تخزين",
                    Description = "",
                    DescriptionArabic = "",
                    IsTop = true,
                    ParentCat = cat1,
                };
                await context.Categories.AddAsync(cat9);
                await context.SaveChangesAsync();

                var cat10 = new Category()
                {
                    Code = "computer-components-15997",
                    Name = "Computer Components",
                    NameArabic = "معدات اجهزة كومبيوتر",
                    Description = "",
                    DescriptionArabic = "",
                    IsTop = true,
                    ParentCat = cat1,
                };
                await context.Categories.AddAsync(cat10);
                await context.SaveChangesAsync();

                var cat11 = new Category()
                {
                    Code = "cables-and-accessories",
                    Name = "Cables and Accessories",
                    NameArabic = "اكسسوارات وكوابل",
                    Description = "",
                    DescriptionArabic = "",
                    IsTop = true,
                    ParentCat = cat1,
                };
                await context.Categories.AddAsync(cat11);
                await context.SaveChangesAsync();
            }

            #endregion

            #region Seed Products

            if (!context.Products.Any())
            {
                var pro1 = new Product()
                {
                    Name = "32 Inch Hd STD Television LE-32T1N Black",
                    NameArabic = "منتج باللغة العربية",
                    Description =
                        "Experience the joy of Samsung's LED picture quality for enhanced contrast and quicker response times.",
                    DescriptionArabic = "تلفزيون ذكي عالي الوضوح مقاس 32 بوصة مع مستقبل مدمج أسود",
                    Category = await context.Categories.FirstOrDefaultAsync(
                        c => c.Code == "television"
                    ),
                    BuyingPrice = 2400,
                    SellingPrice = 2649,
                    Revenue = 2649 - 2400,
                    Quantity = 10,
                    Discount = 0.18f,
                    IsAvailable = true,
                    AddedOn = DateTime.Now,
                    Weight = "500 gm",
                    Seller = await context.Sellers.FirstOrDefaultAsync(
                        s => s.User.Email == "MoSeller@gmail.com"
                    ),
                    //new properties
                    ModelNumber = "LE-32T1N",
                    WarrantyInDays = 730,
                    EstimateOrderTime = TimeSpan.FromHours(9.42),
                    IsFreeDelivered = true,
                    Brand = await context.Brands.FirstOrDefaultAsync(b => b.Code == "sony"),
                    ImageThumb = "p1.png",
                    ImagesGallery = new List<Image>()
                    {
                        new Image() { ImageName = "p1-1.png" },
                        new Image() { ImageName = "p1-2.png" },
                        new Image() { ImageName = "p1-3.png" },
                        new Image() { ImageName = "p1-4.png" },
                    },
                    MaxQuantityPerOrder = 3,
                    ProductHighlights = new List<ProductHighlights>()
                    {
                        new ProductHighlights()
                        {
                            Feature = "Harmonizes with your space and add sophisticated ambience"
                        },
                        new ProductHighlights()
                        {
                            Feature =
                                "Pictures are clear and detailed with enhanced contrast and depth"
                        },
                        new ProductHighlights()
                        {
                            Feature =
                                "Maintains accurate color, even from the sides, and allows everyone to enjoy the realistic picture"
                        },
                        new ProductHighlights()
                        {
                            Feature =
                                "Integrated HDMI interface renders wide video and audio transmission"
                        },
                        new ProductHighlights()
                        {
                            Feature =
                                "Watch videos, play music, or view photos through a USB connection"
                        },
                    },
                    Specifications = new List<ProductSpecifications>()
                    {
                        new ProductSpecifications() { Key = "Colour Name", Name = "Black" },
                        new ProductSpecifications() { Key = "Country of Origin", Name = "China" },
                        new ProductSpecifications()
                        {
                            Key = "Ideal Viewing Distance",
                            Name = "1.7 m"
                        },
                        new ProductSpecifications() { Key = "Model Name", Name = "CON32T10NHA1A" },
                        new ProductSpecifications() { Key = "Model Number", Name = "LE-32T1N" },
                        new ProductSpecifications() { Key = "Number of HDMI Ports", Name = "2" },
                        new ProductSpecifications() { Key = "Number of USB Ports", Name = "2" },
                        new ProductSpecifications() { Key = "RAM Type", Name = "DDR4" },
                        new ProductSpecifications() { Key = "Screen Size", Name = "32 inch" },
                    },
                    CustProSellReviews = new List<CustProSellReviews>()
                    {
                        new CustProSellReviews()
                        {
                            IsAsDescription = true,
                            IsDeliveredOnTime = true,
                            IsProductCommentAnonymous = false,
                            IsSellerCommentAnonymous = false,
                            ProductComment = "it's awesome product",
                            SellerComment = "The Seller is very kind",
                            ProductRate = Rate.Five,
                            SellerRate = Rate.Four,
                            ReviewDate = DateTime.Now,
                            Customer = await context.Customers.FirstOrDefaultAsync(
                                c => c.User.Email == "MoCustomer@gmail.com"
                            ),
                            Seller = await context.Sellers.FirstOrDefaultAsync(
                                s => s.User.Email == "HadySeller@gmail.com"
                            )
                        },
                        new CustProSellReviews()
                        {
                            IsAsDescription = true,
                            IsDeliveredOnTime = true,
                            IsProductCommentAnonymous = false,
                            IsSellerCommentAnonymous = false,
                            ProductComment = "this product is very bad ",
                            SellerComment = "The Seller is very stupid",
                            ProductRate = Rate.One,
                            SellerRate = Rate.Two,
                            ReviewDate = DateTime.Now,
                            Customer = await context.Customers.FirstOrDefaultAsync(
                                c => c.User.Email == "KeroCustomer@gmail.com"
                            ),
                            Seller = await context.Sellers.FirstOrDefaultAsync(
                                s => s.User.Email == "EmadSeller@gmail.com"
                            )
                        }
                    }
                };
                await context.Products.AddAsync(pro1);
                await context.SaveChangesAsync();

                var pro2 = new Product()
                {
                    Name =
                        "Vostro 3510 15.6 Inch Full Hd/ Core I5-1135G7/ 8Gb Ram/ 1Tb Hdd/ Intel Iris Xe Integrated / Ubuntu English/Arabic Black",
                    NameArabic = "لابتوب فوسترو 3510 بشاشة مقاس 15.6 بوصة بدقة كاملة الوضوح",
                    Description =
                        "Amplified display: A brilliant FHD panel (220 nits) offers more brightness and vivid color for an enhanced front-of-screen experience, and a 2-sided narrow border emphasizes your screen while helping minimize distractions.",
                    DescriptionArabic =
                        "شاشة كبيرة: يتميز بشاشة رائعة بدقة كاملة الوضوح وسطوع 220 شمعة لتوفير المزيد من السطوع والألوان الزاهية لتجربة مشاهدة رائعة من أمام الشاشة، وتتميز بحواف رفيعة على الجانبين مما يمنحها مظهراً مميزاً ويساعد على تقليل التشتت",
                    Category = await context.Categories.FirstOrDefaultAsync(
                        c => c.Code == "laptops"
                    ),
                    BuyingPrice = 3000,
                    SellingPrice = 3393,
                    Revenue = 3393 - 3000,
                    Quantity = 15,
                    Discount = 0.24f,
                    IsAvailable = true,
                    AddedOn = DateTime.Now,
                    Weight = "500 gm",
                    Seller = await context.Sellers.FirstOrDefaultAsync(
                        s => s.User.Email == "KeroSeller@gmail.com"
                    ),
                    //new properties
                    ModelNumber = "3510-E0003-BLK",
                    WarrantyInDays = 365,
                    EstimateOrderTime = TimeSpan.FromHours(9.12),
                    IsFreeDelivered = true,
                    Brand = await context.Brands.FirstOrDefaultAsync(b => b.Code == "dell"),
                    ImageThumb = "p2.png",
                    ImagesGallery = new List<Image>()
                    {
                        new Image() { ImageName = "p2-1.png" },
                        new Image() { ImageName = "p2-2.png" },
                        new Image() { ImageName = "p2-3.png" },
                        new Image() { ImageName = "p2-4.png" },
                    },
                    MaxQuantityPerOrder = 3,
                    ProductHighlights = new List<ProductHighlights>()
                    {
                        new ProductHighlights() { Feature = "Compact Size to handle everywhere" },
                        new ProductHighlights() { Feature = "Compact Size to handle everywhere" },
                        new ProductHighlights() { Feature = "SD Card Slot" }
                    },
                    Specifications = new List<ProductSpecifications>()
                    {
                        new ProductSpecifications()
                        {
                            Key = "Average Battery Life",
                            Name = "10 Hours"
                        },
                        new ProductSpecifications()
                        {
                            Key = "Camera Type",
                            Name = "Primary Camera"
                        },
                        new ProductSpecifications() { Key = "Colour Name", Name = "Black" },
                        new ProductSpecifications()
                        {
                            Key = "Country of Origin",
                            Name = "United States of America (USA)"
                        },
                        new ProductSpecifications()
                        {
                            Key = "Display Resolution",
                            Name = "1080x1920"
                        },
                        new ProductSpecifications()
                        {
                            Key = "Display Resolution Type",
                            Name = "Full HD"
                        },
                        new ProductSpecifications() { Key = "Display Type", Name = "LCD" },
                        new ProductSpecifications()
                        {
                            Key = "External Graphics",
                            Name = "Integrated"
                        },
                        new ProductSpecifications() { Key = "Feature 1", Name = "Bluetooth" },
                        new ProductSpecifications()
                        {
                            Key = "Feature 2",
                            Name = "Built-in Speaker"
                        },
                        new ProductSpecifications() { Key = "Feature 3", Name = "Compact Size" },
                        new ProductSpecifications() { Key = "Feature 4", Name = "Intel" },
                        new ProductSpecifications() { Key = "Feature 5", Name = "Travel" },
                        new ProductSpecifications()
                        {
                            Key = "Graphics Memory Name",
                            Name = "Intel Iris Xe"
                        },
                        new ProductSpecifications()
                        {
                            Key = "Graphics Memory Version",
                            Name = "Yes"
                        },
                        new ProductSpecifications() { Key = "HDMI Output", Name = "Yes" },
                        new ProductSpecifications() { Key = "Internal Memory", Name = "1 TB" },
                        new ProductSpecifications()
                        {
                            Key = "Keyboard Language",
                            Name = "English/Arabic"
                        },
                        new ProductSpecifications() { Key = "Model Name", Name = "Vostro 3510-2" },
                        new ProductSpecifications()
                        {
                            Key = "Model Number",
                            Name = "3510-E0003-BLK"
                        },
                        new ProductSpecifications()
                        {
                            Key = "Number of Cores",
                            Name = "3510-E0003-BLK"
                        },
                        new ProductSpecifications()
                        {
                            Key = "Number of HDMI Ports",
                            Name = "Quad Core"
                        },
                        new ProductSpecifications() { Key = "Number of Cores", Name = "1" },
                        new ProductSpecifications() { Key = "Number of USB Ports", Name = "3" },
                        new ProductSpecifications() { Key = "Operating System", Name = "Ubuntu" },
                        new ProductSpecifications()
                        {
                            Key = "Primary Camera Resolution",
                            Name = "0.3 MP"
                        },
                        new ProductSpecifications() { Key = "Processor Brand", Name = "Intel" },
                        new ProductSpecifications() { Key = "Processor Speed", Name = "2.4 GHz" },
                        new ProductSpecifications() { Key = "Processor Type", Name = "Core i5" },
                        new ProductSpecifications()
                        {
                            Key = "Processor Version",
                            Name = "Core i5-1135G7"
                        },
                        new ProductSpecifications()
                        {
                            Key = "Processor Version Number/Generation",
                            Name = "11th Gen"
                        },
                        new ProductSpecifications() { Key = "Product Height", Name = "0.71 inch" },
                        new ProductSpecifications() { Key = "Product Length", Name = "14.23 inch" },
                        new ProductSpecifications() { Key = "Product Weight", Name = "1.9 kg" },
                        new ProductSpecifications()
                        {
                            Key = "Product Width/Depth",
                            Name = "9.76 inch"
                        },
                        new ProductSpecifications() { Key = "RAM Size", Name = "8 GB" },
                        new ProductSpecifications() { Key = "RAM Type", Name = "DDR4" },
                        new ProductSpecifications() { Key = "Screen Size", Name = "15.6 inch" },
                        new ProductSpecifications() { Key = "SD Card Slot", Name = "Yes" },
                        new ProductSpecifications() { Key = "Storage Type", Name = "HDD" },
                        new ProductSpecifications() { Key = "Usage Type", Name = "Personal" },
                    }
                };
                await context.Products.AddAsync(pro2);
                await context.SaveChangesAsync();

                var pro3 = new Product()
                {
                    Name = "Gaming Console Wireless Controller For PlayStation 4",
                    NameArabic =
                        "جهاز تشغيل العاب الفيديو الرقمية بلاي ستيشن 5 (إصدار الأقراص) مع وحدة تحكم لاسلكية إضافية",
                    Description =
                        "Lightning Speed - Harness the power of a custom CPU, GPU, and SSD with Integrated I/O that rewrite the rules of what a PlayStation console can do.",
                    DescriptionArabic =
                        "منتج يتميز بسرعة عالية - استغل قوة وحدة المعالجة المركزية المخصصة وبطاقة الرسومات ومحرك الأقراص SSD مع وحدات الإدخال والإخراج المدمجة التي تعمل على إعادة كتابة القواعد التي يمكن لجهاز الألعاب بلايستيشن فعلها.",
                    Category = await context.Categories.FirstOrDefaultAsync(
                        c => c.Code == "video-games"
                    ),
                    BuyingPrice = 1000,
                    SellingPrice = 1485,
                    Revenue = 1485 - 1000,
                    Quantity = 10,
                    Discount = 0.64f,
                    IsAvailable = true,
                    AddedOn = DateTime.Now,
                    Weight = "500 gm",
                    Seller = await context.Sellers.FirstOrDefaultAsync(
                        s => s.User.Email == "EmadSeller@gmail.com"
                    ),
                    //new properties
                    ModelNumber = "RBCP4",
                    WarrantyInDays = 30,
                    EstimateOrderTime = TimeSpan.FromHours(9.12),
                    IsFreeDelivered = true,
                    Brand = await context.Brands.FirstOrDefaultAsync(b => b.Code == "sony"),
                    ImageThumb = "p3.png",
                    ImagesGallery = new List<Image>()
                    {
                        new Image() { ImageName = "p3-1.png" },
                        new Image() { ImageName = "p3-2.png" },
                        new Image() { ImageName = "p3-3.png" },
                    },
                    MaxQuantityPerOrder = 3,
                    ProductHighlights = new List<ProductHighlights>()
                    {
                        new ProductHighlights()
                        {
                            Feature =
                                "Leading-edge Bluetooth technology allows for seamless connectivity"
                        },
                        new ProductHighlights()
                        {
                            Feature =
                                "Ergonomically designed to ensure a tight, comfortable grip while playing"
                        },
                        new ProductHighlights()
                        {
                            Feature = "Features high sensitivity buttons for accurate controlling"
                        },
                        new ProductHighlights()
                        {
                            Feature = "Made of top-class materials for enhanced durability"
                        }
                    },
                    Specifications = new List<ProductSpecifications>()
                    {
                        new ProductSpecifications() { Key = "Colour Name", Name = "Black" },
                        new ProductSpecifications() { Key = "Model Number", Name = "RBCP4" },
                        new ProductSpecifications()
                        {
                            Key = "Type of Console Software",
                            Name = "PlayStation 4 (PS4)"
                        }
                    },
                    CustProSellReviews = new List<CustProSellReviews>()
                    {
                        new CustProSellReviews()
                        {
                            IsAsDescription = true,
                            IsDeliveredOnTime = true,
                            IsProductCommentAnonymous = false,
                            IsSellerCommentAnonymous = false,
                            ProductComment = "it's awesome product",
                            SellerComment = "The Seller is very kind",
                            ProductRate = Rate.Five,
                            SellerRate = Rate.Four,
                            ReviewDate = DateTime.Now,
                            Customer = await context.Customers.FirstOrDefaultAsync(
                                c => c.User.Email == "MoCustomer@gmail.com"
                            ),
                            Seller = await context.Sellers.FirstOrDefaultAsync(
                                s => s.User.Email == "HadySeller@gmail.com"
                            )
                        },
                        new CustProSellReviews()
                        {
                            IsAsDescription = true,
                            IsDeliveredOnTime = true,
                            IsProductCommentAnonymous = false,
                            IsSellerCommentAnonymous = false,
                            ProductComment = "this product is very bad ",
                            SellerComment = "The Seller is very stupid",
                            ProductRate = Rate.One,
                            SellerRate = Rate.Two,
                            ReviewDate = DateTime.Now,
                            Customer = await context.Customers.FirstOrDefaultAsync(
                                c => c.User.Email == "KeroCustomer@gmail.com"
                            ),
                            Seller = await context.Sellers.FirstOrDefaultAsync(
                                s => s.User.Email == "EmadSeller@gmail.com"
                            )
                        }
                    }
                };
                await context.Products.AddAsync(pro3);
                await context.SaveChangesAsync();

                var pro4 = new Product()
                {
                    Name =
                        "iPad Pro 2021 (5th Generation) 12.9-Inch, M1 Chip, 256GB, Wi-Fi, 5G, Silver with Facetime - Middle East Version",
                    NameArabic = "آي باد برو 2021 (الجيل الخامس) مقاس 12.9 بوصة مزود بمعالج شريحة",
                    Description =
                        "The ultimate iPad experience Now with breakthrough M performance, a breathtaking XDR display, and blazing‑fast 5G wireless Buckle up",
                    DescriptionArabic =
                        "استمتع بأفضل تجربة مع آي باد بفضل الأداء الثوري للمعالج ، وشاشة متطورة تعمل بتقنية ، واتصال لاسلكي عالي السرعة بتقنية 5",
                    Category = await context.Categories.FirstOrDefaultAsync(
                        c => c.Code == "electronics-and-mobiles"
                    ),
                    BuyingPrice = 25000,
                    SellingPrice = 30000,
                    Revenue = 30000 - 25000,
                    Quantity = 20,
                    Discount = 0.02f,
                    IsAvailable = true,
                    AddedOn = DateTime.Now,
                    Weight = "500 gm",
                    Seller = await context.Sellers.FirstOrDefaultAsync(
                        s => s.User.Email == "HadySeller@gmail.com"
                    ),
                    //new properties
                    ModelNumber = "MHR73AB/A - MHNX3LL/A",
                    WarrantyInDays = 365,
                    EstimateOrderTime = TimeSpan.FromHours(9.12),
                    IsFreeDelivered = true,
                    Brand = await context.Brands.FirstOrDefaultAsync(b => b.Code == "apple"),
                    ImageThumb = "p4.png",
                    MaxQuantityPerOrder = 1,
                    ProductHighlights = new List<ProductHighlights>()
                    {
                        new ProductHighlights()
                        {
                            Feature =
                                "The ultimate iPad experience Now with breakthrough M performance, a breathtaking XDR display, and blazing‑fast 5G wireless Buckle up"
                        },
                        new ProductHighlights()
                        {
                            Feature =
                                "M Chip With M, iPad Pro is the fastest device of its kind It’s designed to take full advantage of next‑level performance and custom technologies like the advanced image signal processor and unified memory architecture of M And with the incredible power efficiency of M, iPad Pro is still thin and light with all‑day battery life, making it as portable as it is powerful"
                        },
                        new ProductHighlights()
                        {
                            Feature =
                                "Ultra Wide camera with Center Stage iPad Pro features a new Ultra Wide camera with a 2MP sensor and a 22‑degree field of view, making it perfect for FaceTime and the new Center Stage feature It’s also great for epic Portrait mode selfies And it works with the TrueDepth camera to securely unlock iPad Pro with Face ID"
                        },
                        new ProductHighlights()
                        {
                            Feature =
                                "The LiDAR Scanner measures how long it takes light to reflect back from objects, so it can create a depth map of any space you’re in and unlock immersive AR experiences And it works with the powerful ISP to more accurately focus images and videos in low‑light conditions and reduce capture time"
                        }
                    },
                    Specifications = new List<ProductSpecifications>()
                    {
                        new ProductSpecifications() { Key = "Battery Size", Name = "9720 mAh" },
                        new ProductSpecifications()
                        {
                            Key = "Camera Type",
                            Name = "Primary Camera + Secondary Camera"
                        },
                        new ProductSpecifications() { Key = "Charging Type", Name = "Type-C" },
                        new ProductSpecifications() { Key = "Colour Name", Name = "Silver" },
                        new ProductSpecifications()
                        {
                            Key = "Connection Type",
                            Name = "Wifi+Cellular"
                        },
                        new ProductSpecifications()
                        {
                            Key = "Display Resolution Type",
                            Name = "Full HD"
                        },
                        new ProductSpecifications()
                        {
                            Key = "Display Type",
                            Name = "Retina Display"
                        },
                        new ProductSpecifications() { Key = "Network", Name = "5G" },
                        new ProductSpecifications() { Key = "Feature 1", Name = "Bluetooth" },
                        new ProductSpecifications()
                        {
                            Key = "Feature 2",
                            Name = "Built-in Speaker"
                        },
                        new ProductSpecifications() { Key = "Feature 3", Name = "Compact Size" },
                        new ProductSpecifications() { Key = "Feature 4", Name = "Intel" },
                        new ProductSpecifications() { Key = "Feature 5", Name = "Travel" },
                        new ProductSpecifications()
                        {
                            Key = "Graphics Memory Name",
                            Name = "Intel Iris Xe"
                        },
                        new ProductSpecifications()
                        {
                            Key = "Graphics Memory Version",
                            Name = "Yes"
                        },
                        new ProductSpecifications() { Key = "HDMI Output", Name = "Yes" },
                        new ProductSpecifications() { Key = "Internal Memory", Name = "1 TB" },
                        new ProductSpecifications()
                        {
                            Key = "Keyboard Language",
                            Name = "English/Arabic"
                        },
                        new ProductSpecifications() { Key = "Model Name", Name = "Vostro 3510-2" },
                        new ProductSpecifications()
                        {
                            Key = "Model Number",
                            Name = "3510-E0003-BLK"
                        },
                        new ProductSpecifications()
                        {
                            Key = "Number of Cores",
                            Name = "3510-E0003-BLK"
                        },
                        new ProductSpecifications()
                        {
                            Key = "Number of HDMI Ports",
                            Name = "Quad Core"
                        },
                        new ProductSpecifications() { Key = "Number of Cores", Name = "1" },
                        new ProductSpecifications() { Key = "Number of USB Ports", Name = "3" },
                        new ProductSpecifications() { Key = "Operating System", Name = "Ubuntu" },
                        new ProductSpecifications()
                        {
                            Key = "Primary Camera Resolution",
                            Name = "0.3 MP"
                        },
                        new ProductSpecifications() { Key = "Processor Brand", Name = "Intel" },
                        new ProductSpecifications() { Key = "Processor Speed", Name = "2.4 GHz" },
                        new ProductSpecifications() { Key = "Processor Type", Name = "Core i5" },
                        new ProductSpecifications()
                        {
                            Key = "Processor Version",
                            Name = "Core i5-1135G7"
                        },
                        new ProductSpecifications()
                        {
                            Key = "Processor Version Number/Generation",
                            Name = "11th Gen"
                        },
                        new ProductSpecifications() { Key = "Product Height", Name = "0.71 inch" },
                        new ProductSpecifications() { Key = "Product Length", Name = "14.23 inch" },
                        new ProductSpecifications() { Key = "Product Weight", Name = "1.9 kg" },
                        new ProductSpecifications()
                        {
                            Key = "Product Width/Depth",
                            Name = "9.76 inch"
                        },
                        new ProductSpecifications() { Key = "RAM Size", Name = "8 GB" },
                        new ProductSpecifications() { Key = "RAM Type", Name = "DDR4" },
                        new ProductSpecifications() { Key = "Screen Size", Name = "15.6 inch" },
                        new ProductSpecifications() { Key = "SD Card Slot", Name = "Yes" },
                        new ProductSpecifications() { Key = "Storage Type", Name = "HDD" },
                        new ProductSpecifications() { Key = "Usage Type", Name = "Personal" },
                    },
                    CustProSellReviews = new List<CustProSellReviews>()
                    {
                        new CustProSellReviews()
                        {
                            IsAsDescription = true,
                            IsDeliveredOnTime = true,
                            IsProductCommentAnonymous = false,
                            IsSellerCommentAnonymous = false,
                            ProductComment = "it's awesome product",
                            SellerComment = "The Seller is very kind",
                            ProductRate = Rate.Five,
                            SellerRate = Rate.Four,
                            ReviewDate = DateTime.Now,
                            Customer = await context.Customers.FirstOrDefaultAsync(
                                c => c.User.Email == "MoCustomer@gmail.com"
                            ),
                            Seller = await context.Sellers.FirstOrDefaultAsync(
                                s => s.User.Email == "HadySeller@gmail.com"
                            )
                        },
                        new CustProSellReviews()
                        {
                            IsAsDescription = true,
                            IsDeliveredOnTime = true,
                            IsProductCommentAnonymous = false,
                            IsSellerCommentAnonymous = false,
                            ProductComment = "this product is very bad ",
                            SellerComment = "The Seller is very stupid",
                            ProductRate = Rate.One,
                            SellerRate = Rate.Two,
                            ReviewDate = DateTime.Now,
                            Customer = await context.Customers.FirstOrDefaultAsync(
                                c => c.User.Email == "KeroCustomer@gmail.com"
                            ),
                            Seller = await context.Sellers.FirstOrDefaultAsync(
                                s => s.User.Email == "EmadSeller@gmail.com"
                            )
                        }
                    }
                };
                await context.Products.AddAsync(pro4);
                await context.SaveChangesAsync();

                var pro5 = new Product()
                {
                    Name = "Watch Series 7 GPS 45mm Aluminium Case with Sport Band Midnight",
                    NameArabic = "ساعة ابل سيريس 7",
                    Description =
                        "Big screen. Huge impact. The larger display enhances the entire experience, making Apple Watch easier to use and read. Series 7 represents our biggest and brightest thinking yet. The challenge was to create a bigger display while barely expanding the dimensions of the watch itself.",
                    DescriptionArabic =
                        "مزودة بشاشة كبيرة تتميز بمقاومة عالية للصدمات تتميز ساعة أبل بشاشة كبيرة لتحسين تجربة الاستخدام وسهولة القراءة تمثل ساعة سيريس 7 أكبر وأفضل أفكارنا حتى الآن كان التحدي هو الحصول على شاشة عرض أكبر",
                    Category = await context.Categories.FirstOrDefaultAsync(
                        c => c.Code == "eg-all-audio"
                    ),
                    BuyingPrice = 9000,
                    SellingPrice = 10400,
                    Revenue = 10400 - 9000,
                    Quantity = 40,
                    Discount = 0.08f,
                    IsAvailable = true,
                    AddedOn = DateTime.Now,
                    Weight = "500 gm",
                    Seller = await context.Sellers.FirstOrDefaultAsync(
                        s => s.User.Email == "HadySeller@gmail.com"
                    ),
                    //new properties
                    ModelNumber = "MKN53 / MKN53AE/AE / MKN53LL/A / MKN53ZP/A",
                    WarrantyInDays = 0,
                    EstimateOrderTime = TimeSpan.FromHours(17.40),
                    IsFreeDelivered = true,
                    Brand = await context.Brands.FirstOrDefaultAsync(b => b.Code == "apple"),
                    ImageThumb = "p5.png",
                    ImagesGallery = new List<Image>()
                    {
                        new Image() { ImageName = "p5-1.png" },
                        new Image() { ImageName = "p5-2.png" },
                        new Image() { ImageName = "p5-3.png" },
                        new Image() { ImageName = "p5-4.png" },
                    },
                    MaxQuantityPerOrder = 10,
                    ProductHighlights = new List<ProductHighlights>()
                    {
                        new ProductHighlights()
                        {
                            Feature = "Crack resistant our strongest front crystal ever"
                        },
                        new ProductHighlights()
                        {
                            Feature = "Dust Resistant with IP6X certification"
                        },
                        new ProductHighlights()
                        {
                            Feature = "Water resistant swimproof with WR50 water resistance"
                        },
                        new ProductHighlights()
                        {
                            Feature =
                                "Measure your blood oxygen level with a revolutionary sensor and app"
                        },
                        new ProductHighlights()
                        {
                            Feature =
                                "Along with other innovations like mindfulness and sleep tracking to keep you healthy from head to toe"
                        },
                    },
                    Specifications = new List<ProductSpecifications>()
                    {
                        new ProductSpecifications() { Key = "Brand Compatibility", Name = "Apple" },
                        new ProductSpecifications() { Key = "Colour Name", Name = "Midnight" },
                        new ProductSpecifications() { Key = "Connection Type", Name = "GPS" },
                        new ProductSpecifications() { Key = "Country Origin", Name = "China" },
                        new ProductSpecifications() { Key = "Feature 2", Name = "Dust Resistance" },
                        new ProductSpecifications() { Key = "Feature 3", Name = "Rechargeable" },
                        new ProductSpecifications() { Key = "Feature 4", Name = "Water Resistant" },
                        new ProductSpecifications() { Key = "Material", Name = "Aluminum" },
                        new ProductSpecifications() { Key = "Model Name", Name = "series 7 GPS" },
                        new ProductSpecifications()
                        {
                            Key = "Model Number",
                            Name = "MKN53 / MKN53AE/AE / MKN53LL/A / MKN53ZP/A"
                        },
                        new ProductSpecifications()
                        {
                            Key = "Smartwatch Dial Size",
                            Name = "45 mm"
                        },
                        new ProductSpecifications()
                        {
                            Key = "What's In The Box",
                            Name = "Smartwatch, Band & Charger"
                        },
                    },
                    CustProSellReviews = new List<CustProSellReviews>()
                    {
                        new CustProSellReviews()
                        {
                            IsAsDescription = true,
                            IsDeliveredOnTime = true,
                            IsProductCommentAnonymous = false,
                            IsSellerCommentAnonymous = false,
                            ProductComment = "it's awesome product",
                            SellerComment = "The Seller is very kind",
                            ProductRate = Rate.Five,
                            SellerRate = Rate.Four,
                            ReviewDate = DateTime.Now,
                            Customer = await context.Customers.FirstOrDefaultAsync(
                                c => c.User.Email == "MoCustomer@gmail.com"
                            ),
                            Seller = await context.Sellers.FirstOrDefaultAsync(
                                s => s.User.Email == "HadySeller@gmail.com"
                            )
                        },
                        new CustProSellReviews()
                        {
                            IsAsDescription = true,
                            IsDeliveredOnTime = true,
                            IsProductCommentAnonymous = false,
                            IsSellerCommentAnonymous = false,
                            ProductComment = "this product is very bad ",
                            SellerComment = "The Seller is very stupid",
                            ProductRate = Rate.One,
                            SellerRate = Rate.Two,
                            ReviewDate = DateTime.Now,
                            Customer = await context.Customers.FirstOrDefaultAsync(
                                c => c.User.Email == "KeroCustomer@gmail.com"
                            ),
                            Seller = await context.Sellers.FirstOrDefaultAsync(
                                s => s.User.Email == "EmadSeller@gmail.com"
                            )
                        }
                    }
                };
                await context.Products.AddAsync(pro5);
                await context.SaveChangesAsync();

                var pro6 = new Product()
                {
                    Name =
                        "PlayStation 5 Console (Disc Version) With Extra Wireless Controller - International Version",
                    NameArabic = "بلايستيشن 5 مزود باقراص ديجيتال",
                    Description =
                        "Lightning Speed - Harness the power of a custom CPU, GPU, and SSD with Integrated I/O that rewrite the rules of what a PlayStation console can do.",
                    DescriptionArabic =
                        "منتج يتميز بسرعة عالية - استغل قوة وحدة المعالجة المركزية المخصصة وبطاقة الرسومات ومحرك الأقراص مع وحدات الإدخال والإخراج المدمجة التي تعمل على إعادة كتابة القواعد التي يمكن لجهاز الألعاب بلايستيشن فعلها.",
                    Category = await context.Categories.FirstOrDefaultAsync(
                        c => c.Code == "video-games"
                    ),
                    BuyingPrice = 20000,
                    SellingPrice = 50000,
                    Revenue = 50000 - 20000,
                    Quantity = 8,
                    Discount = 0.06f,
                    IsAvailable = true,
                    AddedOn = DateTime.Now,
                    Weight = "500 gm",
                    Seller = await context.Sellers.FirstOrDefaultAsync(
                        s => s.User.Email == "HadySeller@gmail.com"
                    ),
                    //new properties
                    ModelNumber = "Playstation 5 + Extra DC",
                    WarrantyInDays = 730,
                    EstimateOrderTime = TimeSpan.FromHours(17.40),
                    IsFreeDelivered = true,
                    Brand = await context.Brands.FirstOrDefaultAsync(b => b.Code == "sony"),
                    ImageThumb = "p6.png",
                    ImagesGallery = new List<Image>()
                    {
                        new Image() { ImageName = "p6-1.png" },
                        new Image() { ImageName = "p6-2.png" },
                        new Image() { ImageName = "p6-3.png" },
                        new Image() { ImageName = "p6-4.png" },
                        new Image() { ImageName = "p6-5.png" },
                        new Image() { ImageName = "p6-6.png" },
                    },
                    MaxQuantityPerOrder = 3,
                    ProductHighlights = new List<ProductHighlights>()
                    {
                        new ProductHighlights()
                        {
                            Feature =
                                "Lightning Speed - Harness the power of a custom CPU, GPU, and SSD with Integrated I/O that rewrite the rules of what a PlayStation console can do."
                        },
                        new ProductHighlights()
                        {
                            Feature =
                                "Stunning Games - Marvel at incredible graphics and experience new PS5 features."
                        },
                        new ProductHighlights()
                        {
                            Feature =
                                "Breathtaking Immersion - Discover a deeper gaming experience with support for haptic feedback, adaptive triggers, and 3D Audio technology."
                        },
                        new ProductHighlights()
                        {
                            Feature =
                                "Enjoy smooth and fluid high-frame-rate gameplay at up to 120fps for compatible games."
                        },
                        new ProductHighlights()
                        {
                            Feature =
                                "With an HDR tv, supported ps5 games display an unbelievably vibrant and lifelike range of colours"
                        },
                    },
                    Specifications = new List<ProductSpecifications>()
                    {
                        new ProductSpecifications()
                        {
                            Key = "Ultra-High Speed SSD",
                            Name =
                                "Maximize your play sessions with near instant load times for installed PS5 games."
                        },
                        new ProductSpecifications()
                        {
                            Key = "Ray Tracing",
                            Name =
                                "Immerse yourself in worlds with a new level of realism as rays of light are individually simulated, creating true-to-life shadows and reflections in supported PS5 games."
                        },
                        new ProductSpecifications()
                        {
                            Key = "Tempest 3D AudioTech",
                            Name =
                                "Immerse yourself in soundscapes where it feels as if the sound comes from every direction. Through your compatible headphones your surroundings truly come alive with Tempest 3D AudioTech in supported games."
                        },
                        new ProductSpecifications()
                        {
                            Key = "Colour Name",
                            Name = "Extra (White) Controller"
                        },
                        new ProductSpecifications() { Key = "Country Origin", Name = "China" },
                        new ProductSpecifications() { Key = "Model Name", Name = "series 7 GPS" },
                        new ProductSpecifications()
                        {
                            Key = "Model Number",
                            Name = "MKN53 / MKN53AE/AE / MKN53LL/A / MKN53ZP/A"
                        },
                        new ProductSpecifications()
                        {
                            Key = "Type of Console Software",
                            Name = "PlayStation 5 (PS5)"
                        },
                        new ProductSpecifications()
                        {
                            Key = "What's In The Box",
                            Name =
                                "PlayStation5 Console,DualSense Wireless Controller,DualSense USB charging cable,ASTRO's PLAYROOM (Pre-installed game)Base,HDMI cable,AC power cord,Printed materials"
                        },
                    }
                };
                await context.Products.AddAsync(pro6);
                await context.SaveChangesAsync();

                var pro7 = new Product()
                {
                    Name = "11T Dual SIM Meteorite Gray 8GB RAM 256GB 5G - Global Version",
                    NameArabic =
                        "هاتف 11 ثنائي الشريحة بذاكرة رام 8 جيجابايت وذاكرة داخلية 256 جيجابايت ويدعم تقنية 5، بلون رمادي ميتيورايت - إصدار العالمي",
                    Description = "108MP pro-grade camera |Ultra-wide and Macro lenses",
                    DescriptionArabic =
                        "كاميرا احترافية بدقة 108 ميجابكسل | عدسة ماكرو وعدسة وواسعة للغاية",
                    Category = await context.Categories.FirstOrDefaultAsync(
                        c => c.Code == "electronics-and-mobiles"
                    ),
                    BuyingPrice = 8500,
                    SellingPrice = 9999,
                    Revenue = 30000 - 25000,
                    Quantity = 20,
                    Discount = 0.07f,
                    IsAvailable = true,
                    AddedOn = DateTime.Now,
                    Weight = "500 gm",
                    Seller = await context.Sellers.FirstOrDefaultAsync(
                        s => s.User.Email == "EmadSeller@gmail.com"
                    ),
                    //new properties
                    ModelNumber = "Xiaomi 11T 8/256",
                    WarrantyInDays = 365,
                    EstimateOrderTime = TimeSpan.FromHours(9.12),
                    IsFreeDelivered = true,
                    Brand = await context.Brands.FirstOrDefaultAsync(b => b.Code == "Xiaomi"),
                    ImageThumb = "p15.png",
                    ImagesGallery = new List<Image>()
                    {
                        new Image() { ImageName = "p7-1.png" },
                        new Image() { ImageName = "p7-2.png" },
                    },
                    MaxQuantityPerOrder = 1,
                    ProductHighlights = new List<ProductHighlights>()
                    {
                        new ProductHighlights()
                        {
                            Feature = "108MP pro-grade camera |Ultra-wide and Macro lenses"
                        },
                        new ProductHighlights()
                        {
                            Feature =
                                "Flagship Mediatek Dimensity 1200 Ultra | 6nm processor with dual 5G, up to 3.0GHz"
                        },
                        new ProductHighlights()
                        {
                            Feature =
                                "120GHz AdaptiveSync Amoled Display | Dual Speakers, Dolby Atmos"
                        },
                        new ProductHighlights() { Feature = "67W Wired Turbo Charging" }
                    },
                    Specifications = new List<ProductSpecifications>()
                    {
                        new ProductSpecifications() { Key = "Battery Size", Name = "9720 mAh" },
                        new ProductSpecifications()
                        {
                            Key = "Camera Type",
                            Name = "Primary Camera + Secondary Camera"
                        },
                        new ProductSpecifications() { Key = "Charging Type", Name = "Type-C" },
                        new ProductSpecifications() { Key = "Colour Name", Name = "Silver" },
                        new ProductSpecifications()
                        {
                            Key = "Connection Type",
                            Name = "Wifi+Cellular"
                        },
                        new ProductSpecifications()
                        {
                            Key = "Display Resolution Type",
                            Name = "Full HD"
                        },
                        new ProductSpecifications()
                        {
                            Key = "Display Type",
                            Name = "Retina Display"
                        },
                        new ProductSpecifications() { Key = "Network", Name = "5G" },
                        new ProductSpecifications() { Key = "Feature 1", Name = "Bluetooth" },
                        new ProductSpecifications()
                        {
                            Key = "Feature 2",
                            Name = "Built-in Speaker"
                        },
                        new ProductSpecifications() { Key = "Feature 3", Name = "Compact Size" },
                        new ProductSpecifications() { Key = "Feature 4", Name = "Intel" },
                        new ProductSpecifications() { Key = "Feature 5", Name = "Travel" },
                        new ProductSpecifications()
                        {
                            Key = "Graphics Memory Name",
                            Name = "Intel Iris Xe"
                        },
                        new ProductSpecifications()
                        {
                            Key = "Graphics Memory Version",
                            Name = "Yes"
                        },
                        new ProductSpecifications() { Key = "HDMI Output", Name = "Yes" },
                        new ProductSpecifications() { Key = "Internal Memory", Name = "1 TB" },
                        new ProductSpecifications()
                        {
                            Key = "Keyboard Language",
                            Name = "English/Arabic"
                        },
                        new ProductSpecifications() { Key = "Model Name", Name = "Vostro 3510-2" },
                        new ProductSpecifications()
                        {
                            Key = "Model Number",
                            Name = "3510-E0003-BLK"
                        },
                        new ProductSpecifications()
                        {
                            Key = "Number of Cores",
                            Name = "3510-E0003-BLK"
                        },
                        new ProductSpecifications()
                        {
                            Key = "Number of HDMI Ports",
                            Name = "Quad Core"
                        },
                        new ProductSpecifications() { Key = "Number of Cores", Name = "1" },
                        new ProductSpecifications() { Key = "Number of USB Ports", Name = "3" },
                        new ProductSpecifications() { Key = "Operating System", Name = "Ubuntu" },
                        new ProductSpecifications()
                        {
                            Key = "Primary Camera Resolution",
                            Name = "0.3 MP"
                        },
                        new ProductSpecifications() { Key = "Processor Brand", Name = "Intel" },
                        new ProductSpecifications() { Key = "Processor Speed", Name = "2.4 GHz" },
                        new ProductSpecifications() { Key = "Processor Type", Name = "Core i5" },
                        new ProductSpecifications()
                        {
                            Key = "Processor Version",
                            Name = "Core i5-1135G7"
                        },
                        new ProductSpecifications()
                        {
                            Key = "Processor Version Number/Generation",
                            Name = "11th Gen"
                        },
                        new ProductSpecifications() { Key = "Product Height", Name = "0.71 inch" },
                        new ProductSpecifications() { Key = "Product Length", Name = "14.23 inch" },
                        new ProductSpecifications() { Key = "Product Weight", Name = "1.9 kg" },
                        new ProductSpecifications()
                        {
                            Key = "Product Width/Depth",
                            Name = "9.76 inch"
                        },
                        new ProductSpecifications() { Key = "RAM Size", Name = "8 GB" },
                        new ProductSpecifications() { Key = "RAM Type", Name = "DDR4" },
                        new ProductSpecifications() { Key = "Screen Size", Name = "15.6 inch" },
                        new ProductSpecifications() { Key = "SD Card Slot", Name = "Yes" },
                        new ProductSpecifications() { Key = "Storage Type", Name = "HDD" },
                        new ProductSpecifications() { Key = "Usage Type", Name = "Personal" },
                    },
                    CustProSellReviews = new List<CustProSellReviews>()
                    {
                        new CustProSellReviews()
                        {
                            IsAsDescription = true,
                            IsDeliveredOnTime = true,
                            IsProductCommentAnonymous = false,
                            IsSellerCommentAnonymous = false,
                            ProductComment = "it's awesome product",
                            SellerComment = "The Seller is very kind",
                            ProductRate = Rate.Five,
                            SellerRate = Rate.Four,
                            ReviewDate = DateTime.Now,
                            Customer = await context.Customers.FirstOrDefaultAsync(
                                c => c.User.Email == "MoCustomer@gmail.com"
                            ),
                            Seller = await context.Sellers.FirstOrDefaultAsync(
                                s => s.User.Email == "HadySeller@gmail.com"
                            )
                        },
                        new CustProSellReviews()
                        {
                            IsAsDescription = true,
                            IsDeliveredOnTime = true,
                            IsProductCommentAnonymous = false,
                            IsSellerCommentAnonymous = false,
                            ProductComment = "this product is very bad ",
                            SellerComment = "The Seller is very stupid",
                            ProductRate = Rate.One,
                            SellerRate = Rate.Two,
                            ReviewDate = DateTime.Now,
                            Customer = await context.Customers.FirstOrDefaultAsync(
                                c => c.User.Email == "KeroCustomer@gmail.com"
                            ),
                            Seller = await context.Sellers.FirstOrDefaultAsync(
                                s => s.User.Email == "EmadSeller@gmail.com"
                            )
                        }
                    }
                };
                await context.Products.AddAsync(pro7);
                await context.SaveChangesAsync();
            }

            #endregion

            #region Seed Orders

            if (!context.Orders.Any())
            {
                #region Mo Orders

                #region Order 1

                var orderMo1 = new Order()
                {
                    Customer = await context.Customers.FirstOrDefaultAsync(
                        c => c.User.Email == "MoCustomer@gmail.com"
                    ),
                    Shipper = await context.Shippers.FirstOrDefaultAsync(
                        s => s.User.Email == "MoShipper@gmail.com"
                    ),
                    Discount = (decimal)0.5,
                    DeliveryStatus = DeliveryStatus.Delivered,
                    PaymentMethod = PaymentMethod.Cash,
                    OrderDate = DateTime.Now,
                    Address = await context.Addresses.FirstOrDefaultAsync(x => x.Id == 19),
                    //AddressPhone = "0123456789",
                    OrderItems = new Collection<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Quantity = 1,
                            Product = await context.Products.FirstOrDefaultAsync(p => p.Id == 1)
                        },
                        new OrderItem()
                        {
                            Quantity = 2,
                            Product = await context.Products.FirstOrDefaultAsync(p => p.Id == 2)
                        },
                    }
                };

                orderMo1.CalcTotalPrice();
                await context.Orders.AddAsync(orderMo1);
                //reduce product quantity
                foreach (var orderItem in orderMo1.OrderItems)
                {
                    var product = orderItem.Product;
                    var reducedQuantity = orderItem.Quantity;
                    product.Quantity -= reducedQuantity;
                }

                //reduce Customer Balance
                if (orderMo1.PaymentMethod == PaymentMethod.NoonBalance)
                {
                    var user = await context.ApplicationUser.FirstOrDefaultAsync(
                        c => c.Id == orderMo1.Customer.Id
                    );
                    user.Balance -= orderMo1.TotalPrice;
                }

                await context.SaveChangesAsync();

                #endregion

                #region Order 2

                var orderMo2 = new Order()
                {
                    Customer = await context.Customers.FirstOrDefaultAsync(
                        c => c.User.Email == "MoCustomer@gmail.com"
                    ),
                    Shipper = await context.Shippers.FirstOrDefaultAsync(
                        s => s.User.Email == "MoShipper@gmail.com"
                    ),
                    Discount = (decimal)0.5,
                    DeliveryStatus = DeliveryStatus.Processing,
                    PaymentMethod = PaymentMethod.Cash,
                    OrderDate = DateTime.Now,
                    Address = await context.Addresses.FirstOrDefaultAsync(x => x.Id == 19),
                    //AddressPhone = "0123456789",
                    OrderItems = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Quantity = 3,
                            Product = await context.Products.FirstOrDefaultAsync(p => p.Id == 3)
                        },
                    }
                };

                orderMo2.CalcTotalPrice();
                await context.Orders.AddAsync(orderMo2);
                //reduce product quantity
                foreach (var orderItem in orderMo2.OrderItems)
                {
                    var product = orderItem.Product;
                    var reducedQuantity = orderItem.Quantity;
                    product.Quantity -= reducedQuantity;
                }

                //reduce Customer Balance
                if (orderMo2.PaymentMethod == PaymentMethod.Cash)
                {
                    var user = await context.ApplicationUser.FirstOrDefaultAsync(
                        c => c.Id == orderMo2.Customer.Id
                    );
                    user.Balance -= orderMo1.TotalPrice;
                }

                await context.SaveChangesAsync();

                #endregion

                #endregion

                #region Kero Orders

                #region Order 1

                var orderKero1 = new Order()
                {
                    Customer = await context.Customers.FirstOrDefaultAsync(
                        c => c.User.Email == "KeroCustomer@gmail.com"
                    ),
                    Shipper = await context.Shippers.FirstOrDefaultAsync(
                        s => s.User.Email == "KeroShipper@gmail.com"
                    ),
                    Discount = (decimal)0.5,
                    DeliveryStatus = DeliveryStatus.Delivered,
                    Address = await context.Addresses.FirstOrDefaultAsync(x => x.Id == 19),
                    PaymentMethod = PaymentMethod.Paypal,
                    OrderDate = DateTime.Now,
                    //AddressPhone = "0123456789",
                    OrderItems = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Quantity = 1,
                            Product = await context.Products.FirstOrDefaultAsync(p => p.Id == 1)
                        },
                        new OrderItem()
                        {
                            Quantity = 2,
                            Product = await context.Products.FirstOrDefaultAsync(p => p.Id == 2)
                        },
                    }
                };
                orderKero1.CalcTotalPrice();
                await context.Orders.AddAsync(orderKero1);
                //reduce product quantity
                foreach (var orderItem in orderKero1.OrderItems)
                {
                    var product = orderItem.Product;
                    var reducedQuantity = orderItem.Quantity;
                    product.Quantity -= reducedQuantity;
                }

                //reduce Customer Balance
                if (orderKero1.PaymentMethod == PaymentMethod.NoonBalance)
                {
                    var user = await context.ApplicationUser.FirstOrDefaultAsync(
                        c => c.Id == orderKero1.Customer.Id
                    );
                    user.Balance -= orderKero1.TotalPrice;
                }

                await context.SaveChangesAsync();

                #endregion

                #region Order 2

                var orderKero2 = new Order()
                {
                    Customer = await context.Customers.FirstOrDefaultAsync(
                        c => c.User.Email == "KeroCustomer@gmail.com"
                    ),
                    Shipper = await context.Shippers.FirstOrDefaultAsync(
                        s => s.User.Email == "KeroShipper@gmail.com"
                    ),
                    Discount = (decimal)0.5,
                    DeliveryStatus = DeliveryStatus.Processing,
                    PaymentMethod = PaymentMethod.Cash,
                    Address = await context.Addresses.FirstOrDefaultAsync(x => x.Id == 19),
                    OrderDate = DateTime.Now,
                    //AddressPhone = "0123456789",
                    OrderItems = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Quantity = 3,
                            Product = await context.Products.FirstOrDefaultAsync(p => p.Id == 3)
                        }
                    }
                };
                orderKero2.CalcTotalPrice();
                await context.Orders.AddAsync(orderKero2);
                //reduce product quantity
                foreach (var orderItem in orderKero2.OrderItems)
                {
                    var product = orderItem.Product;
                    var reducedQuantity = orderItem.Quantity;
                    product.Quantity -= reducedQuantity;
                }

                //reduce Customer Balance
                if (orderKero2.PaymentMethod == PaymentMethod.Cash)
                {
                    var user = await context.ApplicationUser.FirstOrDefaultAsync(
                        c => c.Id == orderKero2.Customer.Id
                    );
                    user.Balance -= orderKero2.TotalPrice;
                }

                await context.SaveChangesAsync();

                #endregion

                #endregion

                #region Emad Orders

                #region Order 1

                var orderEmad1 = new Order()
                {
                    Customer = await context.Customers.FirstOrDefaultAsync(
                        c => c.User.Email == "EmadCustomer@gmail.com"
                    ),
                    Shipper = await context.Shippers.FirstOrDefaultAsync(
                        s => s.User.Email == "EmadShipper@gmail.com"
                    ),
                    Discount = (decimal)0.5,
                    DeliveryStatus = DeliveryStatus.Delivered,
                    PaymentMethod = PaymentMethod.Cash,
                    Address = await context.Addresses.FirstOrDefaultAsync(x => x.Id == 19),
                    OrderDate = DateTime.Now,
                    // AddressPhone = "0123456789",
                    OrderItems = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Quantity = 1,
                            Product = await context.Products.FirstOrDefaultAsync(p => p.Id == 4)
                        },
                        new OrderItem()
                        {
                            Quantity = 2,
                            Product = await context.Products.FirstOrDefaultAsync(p => p.Id == 5)
                        },
                    }
                };
                orderEmad1.CalcTotalPrice();
                await context.Orders.AddAsync(orderEmad1);
                //reduce product quantity
                foreach (var orderItem in orderEmad1.OrderItems)
                {
                    var product = orderItem.Product;
                    var reducedQuantity = orderItem.Quantity;
                    product.Quantity -= reducedQuantity;
                }

                //reduce Customer Balance
                if (orderEmad1.PaymentMethod == PaymentMethod.NoonBalance)
                {
                    var user = await context.ApplicationUser.FirstOrDefaultAsync(
                        c => c.Id == orderEmad1.Customer.Id
                    );
                    user.Balance -= orderEmad1.TotalPrice;
                }

                await context.SaveChangesAsync();

                #endregion

                #region Order 2

                var orderEmad2 = new Order()
                {
                    Customer = await context.Customers.FirstOrDefaultAsync(
                        c => c.User.Email == "EmadCustomer@gmail.com"
                    ),
                    Shipper = await context.Shippers.FirstOrDefaultAsync(
                        s => s.User.Email == "EmadShipper@gmail.com"
                    ),
                    Discount = (decimal)0.5,
                    DeliveryStatus = DeliveryStatus.Processing,
                    PaymentMethod = PaymentMethod.Cash,
                    OrderDate = DateTime.Now,
                    Address = await context.Addresses.FirstOrDefaultAsync(x => x.Id == 19),
                    // AddressPhone = "0123456789",
                    OrderItems = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Quantity = 2,
                            Product = await context.Products.FirstOrDefaultAsync(p => p.Id == 6)
                        }
                    }
                };
                orderEmad2.CalcTotalPrice();
                await context.Orders.AddAsync(orderEmad2);
                //reduce product quantity
                foreach (var orderItem in orderEmad2.OrderItems)
                {
                    var product = orderItem.Product;
                    var reducedQuantity = orderItem.Quantity;
                    product.Quantity -= reducedQuantity;
                }

                //reduce Customer Balance
                if (orderEmad2.PaymentMethod == PaymentMethod.NoonBalance)
                {
                    var user = await context.ApplicationUser.FirstOrDefaultAsync(
                        c => c.Id == orderEmad2.Customer.Id
                    );
                    user.Balance -= orderEmad2.TotalPrice;
                }

                await context.SaveChangesAsync();

                #endregion

                #endregion

                #region Hady Orders

                #region Order 1

                var orderHady1 = new Order()
                {
                    Customer = await context.Customers.FirstOrDefaultAsync(
                        c => c.User.Email == "HadyCustomer@gmail.com"
                    ),
                    Shipper = await context.Shippers.FirstOrDefaultAsync(
                        s => s.User.Email == "HadyShipper@gmail.com"
                    ),
                    Discount = (decimal)0.5,
                    DeliveryStatus = DeliveryStatus.Delivered,
                    PaymentMethod = PaymentMethod.Cash,
                    OrderDate = DateTime.Now,
                    Address = await context.Addresses.FirstOrDefaultAsync(x => x.Id == 19),
                    //AddressPhone = "0123456789",
                    OrderItems = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Quantity = 1,
                            Product = await context.Products.FirstOrDefaultAsync(p => p.Id == 4)
                        },
                        new OrderItem()
                        {
                            Quantity = 2,
                            Product = await context.Products.FirstOrDefaultAsync(p => p.Id == 5)
                        }
                    }
                };
                orderHady1.CalcTotalPrice();
                await context.Orders.AddAsync(orderHady1);
                //reduce product quantity
                foreach (var orderItem in orderHady1.OrderItems)
                {
                    var product = orderItem.Product;
                    var reducedQuantity = orderItem.Quantity;
                    product.Quantity -= reducedQuantity;
                }

                //reduce Customer Balance
                if (orderHady1.PaymentMethod == PaymentMethod.NoonBalance)
                {
                    var user = await context.ApplicationUser.FirstOrDefaultAsync(
                        c => c.Id == orderHady1.Customer.Id
                    );
                    user.Balance -= orderHady1.TotalPrice;
                }

                await context.SaveChangesAsync();

                #endregion

                #region Order 2

                var orderHady2 = new Order()
                {
                    Customer = await context.Customers.FirstOrDefaultAsync(
                        c => c.User.Email == "HadyCustomer@gmail.com"
                    ),
                    Shipper = await context.Shippers.FirstOrDefaultAsync(
                        s => s.User.Email == "HadyShipper@gmail.com"
                    ),
                    Discount = (decimal)0.5,
                    DeliveryStatus = DeliveryStatus.Processing,
                    PaymentMethod = PaymentMethod.Cash,
                    OrderDate = DateTime.Now,
                    Address = await context.Addresses.FirstOrDefaultAsync(x => x.Id == 19),
                    //AddressPhone = "0123456789",
                    OrderItems = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Quantity = 2,
                            Product = await context.Products.FirstOrDefaultAsync(p => p.Id == 6)
                        }
                    }
                };
                orderHady2.CalcTotalPrice();
                await context.Orders.AddAsync(orderHady2);
                //reduce product quantity
                foreach (var orderItem in orderHady2.OrderItems)
                {
                    var product = orderItem.Product;
                    var reducedQuantity = orderItem.Quantity;
                    product.Quantity -= reducedQuantity;
                }

                //reduce Customer Balance
                if (orderHady2.PaymentMethod == PaymentMethod.NoonBalance)
                {
                    var user = await context.ApplicationUser.FirstOrDefaultAsync(
                        c => c.Id == orderHady2.Customer.Id
                    );
                    user.Balance -= orderHady2.TotalPrice;
                }

                await context.SaveChangesAsync();

                #endregion

                #endregion
            }

            #endregion
        }
    }
}
