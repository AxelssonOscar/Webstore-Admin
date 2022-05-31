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
        private List<Discount> discountList = new List<Discount>();

        private List<string> weatherTypes = new List<string>();

        private List<Day> Calendar = new List<Day>();

        //private List<string> holidays = new List<string>();

        public class Day
        {
            public Day(DateTime day)
            {
                CurrentDay = day;
            }
            public DateTime CurrentDay { get; set; }

            public int Weight { get; set; }

            public string WeatherType { get; set; }

            public List<Discount> discounts { get; set; }


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
            discountList = context.Discounts.ToList();


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

            foreach(Discount discount in discountList)
            {
                List<DateTime> listOfDiscountDates = new List<DateTime>();
                DateTime start = discount.StartDate;

                while(start.Date <= discount.EndDate.Date)
                {
                    DateTime newDate = new DateTime();
                    newDate = start;
                    listOfDiscountDates.Add(newDate);
                    start = start.AddDays(1);
                }

                var discountLength = discount.EndDate.Date - discount.StartDate.Date;



                for (int i = 0; i < Calendar.Count; i++)
                {
                    if (Calendar[i].CurrentDay.Date == discount.StartDate.Date)
                    {
                        for(int j = 0; j < discountLength.TotalDays; j++)
                        {
                            if(Calendar[i + j].discounts == null)
                                Calendar[i + j].discounts = new List<Discount>();
                            Calendar[i + j].discounts.Add(discount);
                        }
                    }
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

                for(int i = 0; i < productList.Count; i++)
                {
                    WeightedProducts newWeightedProduct = new WeightedProducts();
                    newWeightedProduct.Product = productList[i];
                    newWeightedProduct.Weight = 10;
                    newWeightedProduct.ProductPicked = false;
                    weightedProducts.Add(newWeightedProduct);
                    sum += 10;
                }

                if(day.discounts != null)
                {
                    foreach(Discount discount in day.discounts)
                    {
                        for(int i = 0; i < weightedProducts.Count; i++)
                        {
                            if(weightedProducts[i].Product == discount.Product)
                            {
                                weightedProducts[i].Weight = (int)(discount.Percent * 10);
                                sum += weightedProducts[i].Weight - 10;
                            }
                        }
                    }
                }
                
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

                    foreach(WeightedProducts product in weightedProducts)
                    {
                        product.ProductPicked = false;
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
            bool itemFound = false;

            
            while(!itemFound)
            {
                foreach (WeightedProducts product in weightedProducts)
                {
                    sum += product.Weight;
                    if (sum >= r && !product.ProductPicked)
                    {
                        product.ProductPicked = true;
                        return product.Product;
                    }
                }
                r = rnd.Next(weightedSum);
                sum = 0;
            }
            
            return null;
        }

    }
}
