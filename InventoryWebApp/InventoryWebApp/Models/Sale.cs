using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebApp.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public int QuantityPurchased { get; set; }
        public decimal ProductPricePerUnit { get; set; }
        public decimal TotalPrice { get; set; }

        public string UserId { get; set; }
        public int ProductId { get; set; }
    }
}