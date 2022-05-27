using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Webstore_Admin.Models;

namespace Webstore_Admin.ViewModel
{
    public class CustomerDetailsViewModel
    {
        public Customer customer { get; set; }

        public string distance { get; set; }

    }
}