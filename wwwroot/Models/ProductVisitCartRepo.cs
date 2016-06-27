using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartApplication.Models
{
    public class ProductVisitCartRepo
    {
        ShoppingCart743Entities context = new ShoppingCart743Entities();

        public void RemoveItemFromCart(string sessionID, int productNum)
        {
            var customerCart = (from pv in context.ProductVisits
                                where sessionID == pv.sessionID && pv.productID == productNum
                                select pv);
            foreach(ProductVisit product in customerCart)
            {
                context.ProductVisits.Remove(product);
            }
            context.SaveChanges();
        }

        public void SendCartList(string sessionID, List<ProductVisitCartVM> cart)
        {
            ShoppingCart743Entities context = new ShoppingCart743Entities();

            foreach(ProductVisitCartVM item in cart)
            {
                var customerCart = (from pv in context.ProductVisits
                                    where sessionID == pv.sessionID && pv.productID == item.ProductID
                                    select pv);
                customerCart.FirstOrDefault().qtyOrdered = item.QtyOrdered;
            }

            context.SaveChanges();
        }

        public IEnumerable<ProductVisitCartVM> GetCartList(string sessionID)
        {
            var productVisitCart = (from pv in context.ProductVisits
                                    from p in context.Products
                                    .Where(pvp => pvp.productID == pv.productID && pv.sessionID == sessionID)
                                    select new ProductVisitCartVM
                                    {
                                        SessionID = pv.sessionID,
                                        ProductID = p.productID,
                                        QtyOrdered = pv.qtyOrdered,
                                        Price = p.price,
                                        ProductName = p.productName
                                    });
            return productVisitCart;
        }
    }
}