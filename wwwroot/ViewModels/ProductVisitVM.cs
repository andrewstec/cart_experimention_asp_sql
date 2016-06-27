using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShoppingCartApplication.Models
{
    public class ProductVisitVM
    {
		[Key, Column(Order=1)]
        public string SessionID { get; set; }
        [Key, Column(Order = 2)]
        public int? ProductID { get; set; }
        public int? QtyOrdered { get; set; }
        public DateTime? Updated { get; set; }

        public ProductVisitVM()
        {
            //default constructor
        }

        public ProductVisitVM(string sessionID, int productID, int? qtyOrdered, DateTime? updated)
        {
            //overloaded constructor
            SessionID = sessionID;
            ProductID = productID;
            QtyOrdered = qtyOrdered;
            Updated = updated;
        }
    }
}