using Microsoft.EntityFrameworkCore;
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
        private List<DiscountProduct> discountList = new List<DiscountProduct>();

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

            public List<DiscountProduct> DiscountProducts { get; set; }


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
            discountList = context.DiscountProducts.Include(d => d.Discount).OrderBy(d => d.Discount.StartDate).ToList();


            bool done = false;
            int DayCounter = 0;

            //Skapa dagarna och lägg in de i kalendern.
            while(!done)
            {
                Day day = new Day(DateTime.Today.AddDays((-(365*3) - 10) + DayCounter));

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

            foreach (DiscountProduct discountProduct in discountList)
            {
                List<DateTime> listOfDiscountDates = new List<DateTime>();
                DateTime start = discountProduct.Discount.StartDate;

                while (start.Date <= discountProduct.Discount.EndDate.Date)
                {
                    DateTime newDate = new DateTime();
                    newDate = start;
                    listOfDiscountDates.Add(newDate);
                    start = start.AddDays(1);
                }

                var discountLength = discountProduct.Discount.EndDate.Date - discountProduct.Discount.StartDate.Date;



                for (int i = 0; i < Calendar.Count; i++)
                {
                    if (Calendar[i].CurrentDay.Date == discountProduct.Discount.StartDate.Date)
                    {
                        for (int j = 0; j < discountLength.TotalDays; j++)
                        {
                            if (Calendar[i + j].DiscountProducts == null)
                                Calendar[i + j].DiscountProducts = new List<DiscountProduct>();
                            Calendar[i + j].DiscountProducts.Add(discountProduct);
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

            int counter = 0;

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

                if (day.DiscountProducts != null)
                {
                    foreach (DiscountProduct discountProduct in day.DiscountProducts)
                    {
                        for (int i = 0; i < weightedProducts.Count; i++)
                        {
                            if(weightedProducts[i].Product == discountProduct.Product)
                            {
                                weightedProducts[i].Weight = (int)discountProduct.Discount.Percent;
                                sum += weightedProducts[i].Weight - 10;
                            }
                        }
                    }
                }

                for (int i = 0; i < rnd.Next(1, 100); i++)
                {
                    Order newOrder = new Order();
                    newOrder.Customer = customerList[rnd.Next(customerList.Count)];
                    newOrder.CustomerId = newOrder.Customer.Id;

                    newOrder.OrderCreated = day.CurrentDay;

                    //Skapa produkterna i ordern.
                    newOrder.OrderDetails = CreateItemsInOrders(weightedProducts, sum, day);
                    foreach(OrderDetail detail in newOrder.OrderDetails)
                    {
                        detail.Order = newOrder;
                    }
                    orderList.Add(newOrder);

                    foreach(WeightedProducts product in weightedProducts)
                    {
                        product.ProductPicked = false;
                    }

                }

                counter++;
                
            }

        }

        //Skapa produkterna till ordern.
        public ICollection<OrderDetail> CreateItemsInOrders(List<WeightedProducts> weightedProducts, int sum, Day day)
        {
            Collection<OrderDetail> orderDetails = new Collection<OrderDetail>();

            //Slumpa fram antalet unika produkter.
            for (int j = 0; j < rnd.Next(4) + 1; j++)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.Product = GetItem(weightedProducts, sum);

                if(day.DiscountProducts != null)
                {
                    foreach (DiscountProduct discountProduct in day.DiscountProducts)
                    {
                        if (discountProduct.Product == orderDetail.Product)
                        {
                            orderDetail.Product.DiscountProducts.Add(discountProduct);
                            break;
                        }
                    }
                }

                

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
