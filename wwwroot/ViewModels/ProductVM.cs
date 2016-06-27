using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingCartApplication.ViewModels
{
    public class ProductVM
    {
        [Key]
        [DisplayName("Product SKU")]
        public int ProductID { get; set; }
        [DisplayName("Product Description")]
        public string ProductName { get; set; }
        [DisplayName("Item Price")]
        public decimal? Price { get; set; }

        public ProductVM()
        {
            //default constructor
        }

        public ProductVM(int productID, string productName, decimal? price)
        {
            ProductID = productID;
            ProductName = productName;
            Price = price;
        }
    }
}