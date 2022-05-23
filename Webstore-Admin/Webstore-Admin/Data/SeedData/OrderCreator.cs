using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Webstore_Admin.Data.Contexts;
using Webstore_Admin.Models;

namespace Webstore_Admin.Data.SeedData
{


    public class OrderCreator
    {
        public List<Order> orderList = new List<Order>();
        private List<Product> productList = new List<Product>();
        private List<Customer> customerList = new List<Customer>();

        private List<string> weatherTypes = new List<string>();

        private List<Day> Calendar = new List<Day>();
        private List<Tuple<Day, int>> ListOfHolidays = new List<Tuple<Day, int>>();

        private List<Tuple<Product, string>> HolidayProducts = new List<Tuple<Product, string>>();

        //private List<string> holidays = new List<string>();

        public class Day
        {
            public Day(DateTime day)
            {
                CurrentDay = day;
            }
            public DateTime CurrentDay { get; set; }

            public string HolidayName { get; set; }

            public int Weight { get; set; }

            public string WeatherType { get; set; }

        }

        public class WeightedProducts
        {
            public Product Product;
            public int Weight;
            public bool ProductPicked;
        }

        private Random rnd = new Random();

        public void Init(AppDbContext context)
        {
            weatherTypes.Add("Sunny");
            weatherTypes.Add("Cloudy");
            weatherTypes.Add("Snowy");
            weatherTypes.Add("Storm");

            productList = context.Products.ToList();
            customerList = context.Customers.ToList();


            //Skapa en lista på produkter som säljer bättre under högtider.
            HolidayProducts.Add(Tuple.Create(productList[0], "Påsk"));
            HolidayProducts.Add(Tuple.Create(productList[1], "Påsk"));
            HolidayProducts.Add(Tuple.Create(productList[2], "Påsk"));

            HolidayProducts.Add(Tuple.Create(productList[3], "Midsommar"));
            HolidayProducts.Add(Tuple.Create(productList[4], "Midsommar"));
            HolidayProducts.Add(Tuple.Create(productList[5], "Midsommar"));

            HolidayProducts.Add(Tuple.Create(productList[13], "Jul"));
            HolidayProducts.Add(Tuple.Create(productList[14], "Jul"));
            HolidayProducts.Add(Tuple.Create(productList[15], "Jul"));


            bool done = false;
            int DayCounter = 0;

            //Skapa dagarna och lägg in de i kalendern.
            while(!done)
            {
                Day day = new Day(DateTime.Today.AddDays((-(365*2)) + DayCounter));

                DayCounter++;
                Calendar.Add(day);
                if(day.CurrentDay.Equals(DateTime.Today))
                {
                    done = true;
                }
            }

            PopulateCalendar();
        }

        //Fyller kalendern med data.
        public void PopulateCalendar()
        {
            //Sätter helgdagarna på rätt datum.
            for(int i = 0; i < Calendar.Count; i++)
            {
                //Månad 12, dag 24 är jul.
                if (Calendar[i].CurrentDay.Month == 12 && Calendar[i].CurrentDay.Day == 24)
                {
                    Calendar[i].HolidayName = "Jul";
                    ListOfHolidays.Add(Tuple.Create(Calendar[i], i));
                }
                //Månad 6, dag 25 är midsommar.
                else if (Calendar[i].CurrentDay.Month == 6 && Calendar[i].CurrentDay.Day == 25)
                {
                    Calendar[i].HolidayName = "Midsommar";
                    ListOfHolidays.Add(Tuple.Create(Calendar[i], i));
                }
                //Månad 4, dag 17 är påsk.
                else if (Calendar[i].CurrentDay.Month == 4 && Calendar[i].CurrentDay.Day == 17)
                {
                    Calendar[i].HolidayName = "Påsk";
                    ListOfHolidays.Add(Tuple.Create(Calendar[i], i));
                }
            }

            for(int i = 0; i < Calendar.Count; i++)
            {
                //Hela vintern är kall.
                if (Calendar[i].CurrentDay.Month == 11 || Calendar[i].CurrentDay.Month == 12 || Calendar[i].CurrentDay.Month == 1)
                {
                    Calendar[i].WeatherType = "Snowy";
                }
                //Hela sommaren är varm.
                else if (Calendar[i].CurrentDay.Month == 6 || Calendar[i].CurrentDay.Month == 7 || Calendar[i].CurrentDay.Month == 8)
                {
                    Calendar[i].WeatherType = "Sunny";
                }
                //Slumpa resterande dagar med 1/10 chans för cloudy.
                else
                    Calendar[i].WeatherType = rnd.Next(10) == 1 ? "Cloudy" : "Weather";

            }

            for(int i = 0; i < ListOfHolidays.Count; i++)
            {
                //Sätter weighted values för dagarna innan högtiden.
                for(int j = 10; j > 0; j--)
                {
                    Calendar[ListOfHolidays[i].Item2 - j].HolidayName = ListOfHolidays[i].Item1.HolidayName;
                    Calendar[ListOfHolidays[i].Item2 - j].Weight = -(j - 10) * 10;
                }
                //Sätter weighted values för dagarna efter högtiden.
                for(int j = 1; j < 3; j++)
                {
                    Calendar[ListOfHolidays[i].Item2 - j].HolidayName = ListOfHolidays[i].Item1.HolidayName;
                    Calendar[ListOfHolidays[i].Item2 - j].Weight = 100 - j * 33;
                }
            }
        }

        public void CreateOrders(AppDbContext context)
        {
            //Ladda hem produkterna och kunderna.
            productList = context.Products.ToList();
            customerList = context.Customers.ToList();

            //const int ORDERS_PER_DAY = 10;
            
            //Loopa igenom hela kalendern en dag i taget.
            foreach(Day day in Calendar)
            {
                //Skapar en lista av samtliga produkter och deras vägning beroende på vad det är för sorts dag.
                List<WeightedProducts> weightedProducts = new List<WeightedProducts>();
                int sum = 0;
                bool isHoliday = false;
                for(int i = 0; i < productList.Count; i++)
                {
                    WeightedProducts newWeightedProduct = new WeightedProducts();
                    newWeightedProduct.Product = productList[i];
                    newWeightedProduct.Weight = 10;
                    newWeightedProduct.ProductPicked = false;
                    weightedProducts.Add(newWeightedProduct);
                    sum += 10;
                }
                //Är det en högtidsdag så ska produkterna ha olika vägningar. 
                if(day.HolidayName != null)
                {
                    isHoliday = true;
                    for (int i = 0; i < HolidayProducts.Count; i++)
                    {
                        if(day.HolidayName == HolidayProducts[i].Item2)
                        {
                            weightedProducts[i].Product = HolidayProducts[i].Item1;
                            weightedProducts[i].Weight = day.Weight;
                            sum += day.Weight - 10;
                        }
                    }
                }

                if(isHoliday)
                {
                    //Skapar 10-25 ordrar vid högtid.
                    for (int i = 0; i < rnd.Next(25) + 10; i++)
                    {
                        Order newOrder = new Order();
                        newOrder.Customer = customerList[rnd.Next(customerList.Count)];
                        newOrder.CustomerId = newOrder.Customer.Id;

                        newOrder.OrderCreated = day.CurrentDay;

                        //Skapa produkterna i ordern.
                        newOrder.OrderDetails = CreateItemsInOrders(weightedProducts, sum);
                        newOrder.WeatherType = day.WeatherType;
                        newOrder.Temperature = 0;
                        newOrder.WindSpeed = 0;

                        orderList.Add(newOrder);
                    }
                }
                else
                {
                    //Skapa 1-15 ordrar per dag då det inte är högtid.
                    for (int i = 0; i < rnd.Next(15) + 1; i++)
                    {
                        Order newOrder = new Order();
                        newOrder.Customer = customerList[rnd.Next(customerList.Count)];
                        newOrder.CustomerId = newOrder.Customer.Id;

                        newOrder.OrderCreated = day.CurrentDay;

                        //Skapa produkterna i ordern.
                        newOrder.OrderDetails = CreateItemsInOrders(weightedProducts, sum);
                        newOrder.WeatherType = day.WeatherType;
                        newOrder.Temperature = 0;
                        newOrder.WindSpeed = 0;

                        orderList.Add(newOrder);
                    }
                }
                
            }

        }

        //Skapa produkterna till ordern.
        public ICollection<OrderDetail> CreateItemsInOrders(List<WeightedProducts> weightedProducts, int sum)
        {
            Collection<OrderDetail> orderDetails = new Collection<OrderDetail>();

            //Slumpa fram antalet unika produkter.
            for (int j = 0; j < rnd.Next(4) + 1; j++)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.Product = GetItem(weightedProducts, sum);
                orderDetail.ProductId = orderDetail.Product.Id;
                orderDetail.Amount = rnd.Next(5) + 1;
                orderDetail.Price = orderDetail.Product.Price * orderDetail.Amount;
                orderDetails.Add(orderDetail);
            }

            return orderDetails;
        }

        //Hämta en produkt till ordern. TODO: Fixa så att den inte hämtar duplicerade produkter.
        public Product GetItem(List<WeightedProducts> weightedProducts, int weightedSum)
        {
            double r = rnd.Next(weightedSum);
            double sum = 0;

            foreach (WeightedProducts product in weightedProducts)
            {
                sum += product.Weight;
                if (sum >= r)
                {
                    product.ProductPicked = true;
                    return product.Product;
                }
            }
            return null;
        }

    }
}
