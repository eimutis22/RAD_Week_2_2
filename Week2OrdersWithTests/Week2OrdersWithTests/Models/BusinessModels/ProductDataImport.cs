using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week2OrdersWithTests.Models.BusinessModels
{
    public class ProductDataImport
    {
        public string Description { get; set; }
        public float UnitPrice { get; set; }
        public int StockOnHand { get; set; }
    }
}