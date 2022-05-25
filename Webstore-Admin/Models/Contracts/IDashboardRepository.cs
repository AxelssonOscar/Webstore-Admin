﻿using System;
using System.Collections.Generic;
using Webstore_Admin.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Webstore_Admin.Models.Contracts
{
    public interface IDashboardRepository
    {
        Task <IEnumerable<Product>> LowStockAsync();

    }
}