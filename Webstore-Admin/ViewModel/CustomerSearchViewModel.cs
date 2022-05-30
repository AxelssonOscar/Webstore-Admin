using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Webstore_Admin.Models;
using Webstore_Admin.Models.Helpers;

namespace Webstore_Admin.ViewModel
{
    public class CustomerSearchViewModel
    {
        public PaginatedList<Customer> customers { get; set; }
        public int sum { get; set; }

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }


    }
}