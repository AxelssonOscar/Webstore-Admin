using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Webstore_Admin.Models;
using System.Text;


namespace Webstore_Admin.Data.SeedData
{

    public class City
    {
        public string Name { get; set; }
        public int Population { get; set; }
        public double Weight { get; set; }
    }

    public class CustomerCreator
    {
        public List<Customer> customerList;
        private List<City> cities;
        private Dictionary<string, List<string>> streets;
        private Random rnd = new Random();

        public void LoadData()
        {
            customerList = new List<Customer>();
            cities = new List<City>();
            streets = new Dictionary<string, List<string>>();
            string[] firstnamesInput = File.ReadAllLines("./Data/SeedData/Firstname.txt", Encoding.GetEncoding("iso-8859-1"));
            string[] lastnamesInput = File.ReadAllLines("./Data/SeedData/Lastname.txt", Encoding.GetEncoding("iso-8859-1"));
            string[] citiesInput = File.ReadAllLines("./Data/SeedData/Cities.txt", Encoding.GetEncoding("iso-8859-1"));
            string[] streetsInput = File.ReadAllLines("./Data/SeedData/Streets.txt", Encoding.GetEncoding("iso-8859-1"));

            //Splittar strängen från stad-input så att staden och befolkningen hamnar i egna variabler.
            for(int i = 0; i < citiesInput.Length; i++)
            {
                string[] cityInput = citiesInput[i].Split("\t");
                string[] cityPopulation = cityInput[1].Split(" ");

                City city = new City();
                city.Name = cityInput[0];
                if (cityPopulation.Length > 1)
                {
                    city.Population = int.Parse(cityPopulation[0] + cityPopulation[1]);
                }
                else
                    city.Population = int.Parse(cityPopulation[0]);

                cities.Add(city);
            }

            //Splittar strängen från gat-input så att staden blir nyckeln i dictionaryn med tillhörande gator.
            for(int i = 0; i < streetsInput.Length; i++)
            {
                string[] streetInput = streetsInput[i].Split("\t");
                if(streets.ContainsKey(streetInput[0]))
                {
                    streets[streetInput[0]].Add(streetInput[1]);
                }
                else
                {
                    List<string> list = new List<string>();
                    list.Add(streetInput[1]);
                    streets[streetInput[0]] = list;
                }
            }

            int totalPop = 0;
            //Summera populationen
            foreach(City city in cities)
            {
                totalPop += city.Population;
            }

            //Beräkna vägningen för städernas storlek. Svar i %.
            foreach(City city in cities)
            {
                city.Weight = (city.Population / (double)totalPop) * 100;
                Debug.WriteLine(city.Name + " " + city.Weight);
            }

            //Skapa kunderna från datan som laddats in.
            //Slumpade namn, vägda men slumpade städer samt städernas gator.
            for(int i = 0; i < 1000; i++)
            {
                string name = firstnamesInput[rnd.Next(firstnamesInput.Length)] + " " + lastnamesInput[rnd.Next(lastnamesInput.Length)];
                string email = string.Join("", name.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries)) + "@hotmail.com";

                Customer newCustomer = new Customer();
                newCustomer.Name = name;
                newCustomer.Email = email;
                newCustomer.City = GetCity().Name;
                newCustomer.ZipCode = "1";
                newCustomer.Address = streets[newCustomer.City][rnd.Next(streets[newCustomer.City].Count)] + " " + rnd.Next(100);

                customerList.Add(newCustomer);
            }

        }

        public City GetCity()
        {
            double r = rnd.NextDouble() * 100;
            double sum = 0;

            foreach(City city in cities)
            {
                sum += city.Weight;
                if(sum >= r)
                {
                    return city;
                }
            }
            return null;
        }


        public void PrintData()
        {
            foreach(Customer customer in customerList)
            {
                Debug.WriteLine(customer.Name + ", " + customer.Email + ", " + customer.City);
            }
        }




    }
}
