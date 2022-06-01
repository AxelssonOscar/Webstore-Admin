using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore_Admin.Models;

namespace Webstore_Admin.ViewModel
{
    public class OrderDetailsViewModel
    {
        public Order Order { get; set; }
        public string Distance { get; set; }
    }
}
