using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Threading;

namespace ConsoleUI
{
    class Program
    {
        static ICarService _carService = new CarManager(new InMemoryCarDal());


        /// <summary>
        /// Verilen Car nesnesine ait propertyleri ekrana yazdırır.
        /// </summary>
        /// <param name="car"></param>
        static void Print(Car car)
        {
            string hyphen = "----------------------------------------------";

            Console.WriteLine(hyphen);
            Console.WriteLine($"Id: {car.Id}");
            Console.WriteLine($"BrandId: {car.BrandId}");
            Console.WriteLine($"ColorId: {car.ColorId}");
            Console.WriteLine($"ModelYear: {car.ModelYear}");
            Console.WriteLine($"DailyPrice: {car.DailyPrice}");
            Console.WriteLine($"Description: {car.Description}");
        }

        /// <summary>
        /// Tüm Car Listesini ekranda gösterir.
        /// </summary>
        static void PrintAllCar()
        {
            var carList = _carService.GetAll();
            foreach (var car in carList)
                Print(car);
        }

        /// <summary>
        /// Car nesnesine ait veri girişlerini kullanıcıya sunar ve girilen verileri Car nesnesi olarak geri döndürür.
        /// </summary>
        /// <returns></returns>
        static Car InputToCar()
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

        static void Main(string[] args)
        {
        start:
            try
            {
                Console.Clear();

                Console.WriteLine("1- Taşıtları Listele");
                Console.WriteLine("2- Yeni Taşıt Ekle");
                Console.WriteLine("3- Taşıt Düzenle");
                Console.WriteLine("4- Taşıt Sil");
                Console.Write("İşlem Kodu(1-4): ");
                byte chooseOperation = Convert.ToByte(Console.ReadLine());

                Console.Clear();

                if (chooseOperation == 1)
                {
                    PrintAllCar();
                }
                else if (chooseOperation == 2)
                {
                    Car createToCar = InputToCar();

                    _carService.Add(createToCar);
                }
                else if (chooseOperation == 3)
                {
                    PrintAllCar();
                    Console.Write("Düzenlenecek Araç Id: ");
                    int carId = Convert.ToInt32(Console.ReadLine());

                    Car updateToCar = InputToCar();
                    _carService.Update(carId, updateToCar);
                }
                else if (chooseOperation == 4)
                {
                    PrintAllCar();
                    Console.Write("Silinecek Araç Id: ");
                    int carId = Convert.ToInt32(Console.ReadLine());

                    _carService.DeleteById(carId);
                }
                else
                {
                    Console.WriteLine("Geçerli olmayan komut. Lütfen 1-4 arasında bir işlem kodu giriniz.");
                }

                Console.WriteLine("Başa Dönmek İçin Bir Tuşa Basın.");
                Console.ReadKey();

                goto start;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                Console.WriteLine("Başa Dönmek İçin Bir Tuşa Basın.");
                Console.ReadKey();
                goto start;
            }
        }
    }
}
