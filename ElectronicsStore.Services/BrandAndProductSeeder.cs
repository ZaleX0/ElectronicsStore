using ElectronicsStore.Data;
using ElectronicsStore.Data.Entities;
using ElectronicsStore.Services.Interfaces;
using ElectronicsStore.Services.Models;

namespace ElectronicsStore.Services;

public class BrandAndProductSeeder
{
	private readonly ElectronicsStoreDbContext _context;
	private readonly IAuthorizationService _authService;

	public BrandAndProductSeeder(ElectronicsStoreDbContext context, IAuthorizationService authService)
	{
		_context = context;
		_authService = authService;
	}

	public async Task Seed()
	{
		if (!_context.Database.CanConnect())
			return;

		if (!_context.Brands.Any())
		{
			var brands = GetBrands();
			var categories = GetCategories();
            var products = GetProducts();
			_context.Brands.AddRange(brands);
			_context.Categories.AddRange(categories);
            _context.Products.AddRange(products);
			_context.SaveChanges();
        }

        if (!_context.Roles.Any())
        {
            var roles = GetRoles();
            _context.AddRange(roles);

            var registerDto = GetAdminUser();
            await _authService.RegisterUser(registerDto, 2);
        }
    }

	private IEnumerable<Role> GetRoles()
	{
		return new List<Role>
		{
			new Role { Name = "User" },
			new Role { Name = "Admin" }
		};
	}

	private RegisterDto GetAdminUser()
	{
		return new RegisterDto { Email = "admin@admin.com", Password = "ZAQ!2wsx" };
	}

	private IEnumerable<Brand> GetBrands()
	{
		return new List<Brand>
		{
			new Brand { Name = "Sony" },
			new Brand { Name = "Xiaomi" },
			new Brand { Name = "Apple" },
		};
	}

	private IEnumerable<Category> GetCategories()
	{
		return new List<Category>
		{
			new Category { Name = "Headphones" },
			new Category { Name = "Smartphones" },
			new Category { Name = "Watches" },
		};
	}

    private IEnumerable<Product> GetProducts()
    {
		return new List<Product>
		{
			new Product { BrandId = 1, CategoryId = 1, Price = 39.99m,
				Name = "Sony WI C100 In-Ear Wireless Headphones - Black",
				Description = "Sony WI-C100 Wireless In-ear Headphones - Up to 25 hours of battery life - Water resistant -Built-in mic for phone calls - Voice Assistant compatible - Reliable Bluetooth® connection - Black. Long battery life of up to 25 hours Enjoy up to 25 hours of non-stop music. And if your Bluetooth headphones are running low on power, a quick 10-minute charge will give you up to 60 minutes of playback. These comfortable Sony wireless earbuds will get you through the day, from your morning commute to unwinding at the end of the day. Make your music sound more natural. Tuning is well-balanced from low to high frequencies, and vocals are natural and clear. This means these in-ear headphones can match any music genre. When an original music source is compressed. Your sound, just how you like it. With these Sony headphones, you can tailor your sound to your personal preference. Choose from a variety of presets to match the genre of music you're listening to or create and save your own custom presets using the equaliser feature on the Sony | Connect App.",
				ImageUrl = "https://media.4rgos.it/i/Argos/1349870_R_Z001A?w=1500&h=880&qlt=70&fmt=webp"
			},
			new Product { BrandId = 1, CategoryId = 1, Price = 209.99m,
				Name = "Sony noise cancelling wireless headphones WH-H910N",
				Description = "The Sony WH-H910N/h.ear on 3 are Bluetooth-enabled over-ear headphones with a great overall noise cancelling feature. They're comfortable, sturdily-built, and offer a long continuous battery life depending on your usage habits. Their companion app also allows you to make a wide variety of adjustments to their audio reproduction. Unfortunately, even though they can effectively block out ambient chatter and high-pitched noise, their active noise cancelling (ANC) system's performance is weakest when it comes to hard-to-isolate sounds like the rumble of bus engines. They also have a bulky design that limits their portability, and their integrated microphone struggles to isolate your voice from loud background noise.",
				ImageUrl = "https://www.tio.pl/foto/produkt_big/10/przechwytywanie1(2).png"
			},
			new Product { BrandId = 1, CategoryId = 2, Price = 519.99m,
				Name = "Sony Xperia 10 IV",
				Description = "The Xperia 10 IV features a plastic shell, which is a clear departure from the aluminium and glass construction of the Xperia 1 IV – this is of course a cost-saving measure from Sony. It doesn’t look or feel too cheap in the hand, though, and a matte rear plastic panel emulates the frosted glass rear of the 1 IV to some extent. This plastic build also keeps the 10 IV light, coming in at just 161g. Even with a 6-inch display, the Xperia also feels like a compact device. It does feature a surprisingly small camera housing that neatly tucks the triple camera arrangement away, meaning that the Xperia sits almost flush to any surface it's laid on and doesn’t wobble or rock around. Elsewhere on the device you’ll find a combination lock button and fingerprint sensor that performs reliably, as well as a 3.5mm headphone jack – something Sony remains steadfast on including despite many other companies having ditched the port years ago. It's safe to say that the Xperia 10 IV doesn’t feature the most elaborate construction, but its slim and understated build certainly gives the illusion that it may be a more premium device than it really is. It’s available in a selection of four colours – black, white, lavender and mint – with our matte black review model looking fairly sleek and stealthy.",
				ImageUrl = "https://f00.esfr.pl/foto/5/105934163929/e37af7c4fa80587d4739049fdf94ea1b/sony-smartfon-xperia-10-iv-czarny,105934163929_3.jpg"
            },

			new Product { BrandId = 2, CategoryId = 2, Price = 429.99m,
				Name = "Xiaomi Mi 11 Lite 5G 8/256GB",
				Description = "Xiaomi Mi 11 Lite 5G weighs 157g with dimensions 160,5 x 75,7 x 6,8mm and a 6.55 inches-inch display. The screen resolution is 2400x1080 pixels, the image is bright and clear. The rear camera has a 64Mp sensor. For high-quality selfies, the front camera in 20Mp is responsible.\r\nXiaomi Mi 11 Lite 5G's release date is March 2021. We also note the following technical characteristics of the device: 8Gb of RAM and 256GB of internal storage, which works together with the Qualcomm Snapdragon 780G processor. The model will be a great choice for people who are used to the Android 11 OS.",
				ImageUrl = "https://f01.esfr.pl/foto/7/93094402961/8654a2fd5bb116820f3e8ab621347690/xiaomi-smartfon-xiaomi-11-lite5gne-tblack-8-256,93094402961_3.jpg"
            },
			new Product { BrandId = 2, CategoryId = 2, Price = 299.99m,
				Name = "Xiaomi Redmi Note 10 Pro",
				Description = "The Xiaomi Redmi Note 10 Pro sits at the top of Xiaomi’s mid-range Redmi lineup, and it packs quite a punch for a phone with a modest price tag. It’s powered by Qualcomm’s Snapdragon 732G chipset and features a 6.67-inch AMOLED FHD+ display with a 120 Hz refresh rate. There are three memory/storage configurations: 6 GB / 64 GB, 6 GB / 128 GB, and 8 GB / 128 GB. The rear camera hump houses a 108 MP main camera (binning down to 12 MP output), an 8 MP ultra-wide, and a 5 MP macro module. There’s also a 2 MP depth-sensing camera for bokeh simulation.",
				ImageUrl = "https://prod-api.mediaexpert.pl/api/images/gallery_500_500/thumbnails/images/29/2904659/Smartfon-XIAOMI-Redmi-Note-10-Pro-6-128GB-Szary-front-tyl__1.jpg"
            },

			new Product { BrandId = 3, CategoryId = 3, Price = 339.99m,
				Name = "Smartwatch Apple Watch SE 2gen",
				Description = "Apple Watch SE features a stunning Retina display, with thin borders and curved corners, that is 30 percent larger than Series 3. The interface allows for large and easy-to-read app icons and fonts, while complications are precise and informative. A variety of new watch faces are optimized for the display, so customers can easily view notifications, text messages, workout metrics, and more. With the S5 System in Package (SiP) and dual-core processor, Apple Watch SE delivers incredibly fast performance, up to two times faster than Apple Watch Series 3. The Digital Crown with haptic feedback generates incremental clicks with an extraordinary mechanical feel as it is rotated. Apple Watch SE features the latest speaker and microphone, which are optimized for better sound quality for phone calls, Siri, and Walkie-Talkie,1 along with Bluetooth 5.0.",
				ImageUrl = "https://f00.esfr.pl/foto/4/111559085497/ff4c7d27aa76ab1630f171f0eb34ca70/apple-apple-watch-se-40-mid-al-mid-sp-gps-pro,111559085497_8.jpg"
            },
		};
    }
}
