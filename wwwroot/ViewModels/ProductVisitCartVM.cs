using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShoppingCartApplication.Models
{
    public class ProductVisitCartVM
    {
		[Key]
        public string SessionID { get; set; }
        [DisplayName("Product ID")]
        public int? ProductID { get; set; }
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [DisplayName("Quantity Ordered")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a quantity greater than zero.")]
        public int? QtyOrdered { get; set; }
        [DisplayName("Price")]
        public decimal? Price { get; set; }

        public ProductVisitCartVM()
        {
            //default constructor
        }

        public ProductVisitCartVM(string sessionID, string productName, int productID, int? qtyOrdered, decimal? price)
        {
            //overloaded constructor
            SessionID = sessionID;
            ProductID = productID;
            ProductName = productName;
            QtyOrdered = qtyOrdered;
            Price = price;
        }
    }
}