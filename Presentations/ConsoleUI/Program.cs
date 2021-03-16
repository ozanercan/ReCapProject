using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Dtos;
using System;

namespace ConsoleUI
{
    internal class Program
    {
        private static readonly ICarService _carService = new CarManager(new EfCarDal(), new RentalManager(new EfRentalDal()));
        private static readonly IBrandService _brandService = new BrandManager(new EfBrandDal());
        private static readonly IColorService _colorService = new ColorManager(new EfColorDal());
        private static readonly ICustomerService _customerService = new CustomerManager(new EfCustomerDal(), new UserManager(new EfUserDal()));
        private static readonly IUserService _userService = new UserManager(new EfUserDal());
        private static readonly IRentalService _rentalService = new RentalManager(new EfRentalDal());

        public static string hyphen = "----------------------------------------------";

        /// <summary>
        /// Verilen Car nesnesine ait propertyleri ekrana yazdırır.
        /// </summary>
        /// <param name="car"></param>
        private static void Print(Car car)
        {
            Console.WriteLine(hyphen);
            Console.WriteLine($"Id: {car.Id}");
            Console.WriteLine($"BrandId: {car.BrandId}");
            Console.WriteLine($"ColorId: {car.ColorId}");
            Console.WriteLine($"ModelYear: {car.ModelYear}");
            Console.WriteLine($"DailyPrice: {car.DailyPrice}");
            Console.WriteLine($"Description: {car.Description}");
        }

        private static void Print(CarDetailDto carDetailTo)
        {
            Console.WriteLine(hyphen);
            Console.WriteLine($"Car Description : {carDetailTo.Description}");
            Console.WriteLine($"Brand: {carDetailTo.BrandName}");
            Console.WriteLine($"Color: {carDetailTo.Description}");
            Console.WriteLine($"DailyPrice: {carDetailTo.DailyPrice}");
        }

        private static void Print(CustomerDetailDto customerDetailDto)
        {
            Console.WriteLine(hyphen);
            Console.WriteLine($"Company Name: {customerDetailDto.CompanyName}");
            Console.WriteLine($"First Name: {customerDetailDto.FirstName}");
            Console.WriteLine($"Last Name: {customerDetailDto.LastName}");
            Console.WriteLine($"Email: {customerDetailDto.Email}");
        }

        private static void Print(Brand brand)
        {
            Console.WriteLine(hyphen);
            Console.WriteLine($"Id: {brand.Id}");
            Console.WriteLine($"Name: {brand.Name}");
        }

        private static void Print(Color color)
        {
            Console.WriteLine(hyphen);
            Console.WriteLine($"Id: {color.Id}");
            Console.WriteLine($"Name: {color.Name}");
        }

        /// <summary>
        /// Tüm Car Listesini ekranda gösterir.
        /// </summary>
        private static void PrintAllCar()
        {
            var carListResult = _carService.GetAll();
            if (!carListResult.Success)
            {
                Console.WriteLine(carListResult.Message);
            }
            else
            {
                foreach (var car in carListResult.Data)
                    Print(car);
            }
        }

        private static void PrintCarDetails()
        {
            var carDetailsResult = _carService.GetCarDetails();
            if (!carDetailsResult.Success)
            {
                Console.WriteLine(carDetailsResult.Message);
            }
            else
            {
                foreach (var car in carDetailsResult.Data)
                    Print(car);
            }
        }

        private static void PrintCustomerDetails()
        {
            var customerDetailsResult = _customerService.GetCustomerDetails();
            if (!customerDetailsResult.Success)
            {
                Console.WriteLine(customerDetailsResult.Message);
            }
            else
            {
                foreach (var customerDetail in customerDetailsResult.Data)
                    Print(customerDetail);
            }
        }

        /// <summary>
        /// Tüm Brand Listesini ekranda gösterir.
        /// </summary>
        private static void PrintAllBrand()
        {
            var brandListResult = _brandService.GetAll();
            if (!brandListResult.Success)
            {
                Console.WriteLine(brandListResult.Message);
            }
            else
            {
                foreach (var brand in brandListResult.Data)
                    Print(brand);
            }
        }

        private static void PrintAllColor()
        {
            var colorListResult = _colorService.GetAll();
            if (!colorListResult.Success)
            {
                Console.WriteLine(colorListResult.Message);
            }
            else
            {
                foreach (var color in colorListResult.Data)
                    Print(color);
            }
        }

        /// <summary>
        /// Car nesnesine ait veri girişlerini kullanıcıya sunar ve girilen verileri Car nesnesi olarak geri döndürür.
        /// </summary>
        /// <returns></returns>
        private static Car InputToCar()
        {
            Console.Write("Brand Id: ");
            int brandId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Color Id: ");
            int colorId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Model Year: ");
            int modelYear = Convert.ToInt32(Console.ReadLine());
            Console.Write("Daily Price: ");
            decimal dailyPrice = Convert.ToInt32(Console.ReadLine());
            Console.Write("Description: ");
            string description = Console.ReadLine();

            return new Car()
            {
                BrandId = brandId,
                ColorId = colorId,
                DailyPrice = dailyPrice,
                Description = description,
                ModelYear = modelYear
            };
        }

        private static Brand InputToBrand()
        {
            Console.Write("Brand Name: ");
            string brandName = Console.ReadLine();

            return new Brand()
            {
                Name = brandName
            };
        }

        private static RentalCreateDto InputToRentalCreateDto()
        {
            DateTime rentDate = new DateTime();
            Console.Write("CarId: ");
            int carId = Convert.ToInt32(Console.ReadLine());
            Console.Write("CustomerId: ");
            int customerId = Convert.ToInt32(Console.ReadLine());
            Console.Write("RentDate: (Automatic Input Press A)");
            string rentDateString = Console.ReadLine();
            if (rentDateString.ToLower() == "a")
                rentDate = DateTime.Now;

            return new RentalCreateDto()
            {
                CarId = carId,
                CustomerId = customerId,
                RentDate = rentDate
            };
        }

        private static Color InputToColor()
        {
            Console.Write("Color Name: ");
            string colorName = Console.ReadLine();

            return new Color()
            {
                Name = colorName
            };
        }

        private static UserCreateDto InputToUserCreateDto()
        {
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            return new UserCreateDto()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };
        }

        private static CustomerCreateDto InputToCustomerCreateDto()
        {
            Console.Write("Company Name: ");
            string companyName = Console.ReadLine();

            return new CustomerCreateDto()
            {
                CompanyName = companyName
            };
        }

        private static void Main(string[] args)
        {
        start:
            try
            {
                Console.Clear();

                Console.WriteLine("1- Araç İşlemleri");
                Console.WriteLine("2- Marka İşlemleri");
                Console.WriteLine("3- Renk İşlemleri");
                Console.WriteLine("4- Müşteri İşlemleri");
                Console.WriteLine("4- Kira İşlemleri");
                Console.Write("İşlem Kodu(1-5): ");
                byte chooseOperation = Convert.ToByte(Console.ReadLine());

                Console.Clear();
                if (chooseOperation == 1)
                {
                    #region Car Operations

                    Console.WriteLine("1- Taşıtları Listele");
                    Console.WriteLine("2- Yeni Taşıt Ekle");
                    Console.WriteLine("3- Taşıt Düzenle");
                    Console.WriteLine("4- Taşıt Sil");
                    Console.WriteLine("5- Taşıtları Detaylı Listele");
                    Console.Write("İşlem Kodu(1-4): ");
                    byte chooseCarOperation = Convert.ToByte(Console.ReadLine());

                    Console.Clear();

                    if (chooseCarOperation == 1)
                    {
                        PrintAllCar();
                    }
                    else if (chooseCarOperation == 2)
                    {
                        Car createToCar = InputToCar();

                        var businessResult = _carService.Add(createToCar);
                        Console.WriteLine(businessResult.Message);
                    }
                    else if (chooseCarOperation == 3)
                    {
                        PrintAllCar();
                        Console.Write("Düzenlenecek Araç Id: ");
                        int carId = Convert.ToInt32(Console.ReadLine());

                        Car updateToCar = InputToCar();
                        var businessResult = _carService.Update(carId, updateToCar);
                        Console.WriteLine(businessResult.Message);
                    }
                    else if (chooseCarOperation == 4)
                    {
                        PrintAllCar();
                        Console.Write("Silinecek Araç Id: ");
                        int carId = Convert.ToInt32(Console.ReadLine());

                        var businessResult = _carService.DeleteById(carId);
                        Console.WriteLine(businessResult.Message);
                    }
                    else if (chooseCarOperation == 5)
                    {
                        PrintCarDetails();
                    }
                    else
                    {
                        Console.WriteLine("Geçerli olmayan komut. Lütfen 1-4 arasında bir işlem kodu giriniz.");
                    }

                    #endregion Car Operations
                }
                else if (chooseOperation == 2)
                {
                    #region Brand Operations

                    Console.WriteLine("1- Markaları Listele");
                    Console.WriteLine("2- Yeni Marka Ekle");
                    Console.WriteLine("3- Marka Düzenle");
                    Console.WriteLine("4- Marka Sil");
                    Console.Write("İşlem Kodu(1-4): ");
                    byte chooseBrandOperation = Convert.ToByte(Console.ReadLine());

                    Console.Clear();

                    if (chooseBrandOperation == 1)
                    {
                        PrintAllBrand();
                    }
                    else if (chooseBrandOperation == 2)
                    {
                        Brand createToBrand = InputToBrand();

                        var businessResult = _brandService.Add(createToBrand);
                        Console.WriteLine(businessResult.Message);
                    }
                    else if (chooseBrandOperation == 3)
                    {
                        PrintAllBrand();
                        Console.Write("Düzenlenecek Marka Id: ");
                        int brandId = Convert.ToInt32(Console.ReadLine());

                        Brand updateToBrand = InputToBrand();
                        var businessResult = _brandService.Update(brandId, updateToBrand);
                        Console.WriteLine(businessResult.Message);
                    }
                    else if (chooseBrandOperation == 4)
                    {
                        PrintAllBrand();
                        Console.Write("Silinecek Marka Id: ");
                        int brandId = Convert.ToInt32(Console.ReadLine());

                        var businessResult = _brandService.DeleteById(brandId);
                        Console.WriteLine(businessResult.Message);
                    }
                    else
                    {
                        Console.WriteLine("Geçerli olmayan komut. Lütfen 1-4 arasında bir işlem kodu giriniz.");
                    }

                    #endregion Brand Operations
                }
                else if (chooseOperation == 3)
                {
                    #region Color Operations

                    Console.WriteLine("1- Renkleri Listele");
                    Console.WriteLine("2- Yeni Renk Ekle");
                    Console.WriteLine("3- Renk Düzenle");
                    Console.WriteLine("4- Renk Sil");
                    Console.Write("İşlem Kodu(1-4): ");
                    byte chooseColorOperation = Convert.ToByte(Console.ReadLine());

                    Console.Clear();

                    if (chooseColorOperation == 1)
                    {
                        PrintAllColor();
                    }
                    else if (chooseColorOperation == 2)
                    {
                        Color createToColor = InputToColor();

                        var businessResult = _colorService.Add(createToColor);
                        Console.WriteLine(businessResult.Message);
                    }
                    else if (chooseColorOperation == 3)
                    {
                        PrintAllColor();
                        Console.Write("Düzenlenecek Renk Id: ");
                        int colorId = Convert.ToInt32(Console.ReadLine());

                        Color updateToColor = InputToColor();
                        var businessResult = _colorService.Update(colorId, updateToColor);
                        Console.WriteLine(businessResult.Message);
                    }
                    else if (chooseColorOperation == 4)
                    {
                        PrintAllColor();
                        Console.Write("Silinecek Renk Id: ");
                        int colorId = Convert.ToInt32(Console.ReadLine());

                        var businessResult = _colorService.DeleteById(colorId);
                        Console.WriteLine(businessResult.Message);
                    }
                    else
                    {
                        Console.WriteLine("Geçerli olmayan komut. Lütfen 1-4 arasında bir işlem kodu giriniz.");
                    }

                    #endregion Color Operations
                }
                else if (chooseOperation == 4)
                {
                    #region Customer Operations

                    Console.WriteLine("1- Müşterileri Listele");
                    Console.WriteLine("2- Yeni Müşteri Ekle");
                    //Console.WriteLine("3- Müşteri Düzenle");
                    //Console.WriteLine("4- Müşteri Sil");
                    Console.Write("İşlem Kodu(1-2): ");
                    byte chooseColorOperation = Convert.ToByte(Console.ReadLine());

                    Console.Clear();

                    if (chooseColorOperation == 1)
                    {
                        PrintCustomerDetails();
                    }
                    else if (chooseColorOperation == 2)
                    {
                        UserCreateDto userCreateDto = InputToUserCreateDto();

                        var userAddResult = _userService.Add(userCreateDto);

                        CustomerCreateDto customerCreateDto = InputToCustomerCreateDto();

                        var customerAddResult = _customerService.Add(customerCreateDto);

                        Console.WriteLine(userAddResult.Message);
                    }
                    else
                    {
                        Console.WriteLine("Geçerli olmayan komut. Lütfen 1-2 arasında bir işlem kodu giriniz.");
                    }

                    #endregion Color Operations
                }
                else if (chooseOperation == 5)
                {
                    #region Rent Operations

                    Console.WriteLine("1- Kiraları Listele");
                    Console.WriteLine("2- Yeni Kira Ekle");
                    //Console.WriteLine("3- Müşteri Düzenle");
                    //Console.WriteLine("4- Müşteri Sil");
                    Console.Write("İşlem Kodu(1-2): ");
                    byte chooseRentOperation = Convert.ToByte(Console.ReadLine());

                    Console.Clear();

                    if (chooseRentOperation == 1)
                    {
                        //PrintCustomerDetails();
                    }
                    else if (chooseRentOperation == 2)
                    {
                        RentalCreateDto rentalCreateDto = InputToRentalCreateDto();
                        var addResult = _rentalService.Add(rentalCreateDto);
                        Console.WriteLine(addResult.Message);
                    }
                    else
                    {
                        Console.WriteLine("Geçerli olmayan komut. Lütfen 1-2 arasında bir işlem kodu giriniz.");
                    }

                    #endregion Color Operations
                }
                else
                {
                    Console.WriteLine("Geçerli olmayan komut. Lütfen 1-3 arasında bir işlem kodu giriniz.");
                }

                Console.WriteLine("Başa Dönmek İçin Bir Tuşa Basın.");
                Console.ReadKey();

                goto start;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.InnerException}");
                Console.WriteLine("Başa Dönmek İçin Bir Tuşa Basın.");
                Console.ReadKey();
                goto start;
            }
        }
    }
}