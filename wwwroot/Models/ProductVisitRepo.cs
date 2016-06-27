using ShoppingCartApplication.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartApplication.Models
{
    public class ProductVisitRepo
    {
        ShoppingCart743Entities context = new ShoppingCart743Entities();
        
        public IEnumerable<ProductVisit> GetCart(string sessionID)
        {
            ShoppingCart743Entities context = new ShoppingCart743Entities();
            var cartContents = from pv in context.ProductVisits
                                          where sessionID == pv.sessionID
                                          select pv;
            return cartContents;
            
        }

        public ProductVisit GetCartItem(string sessionID, int productID)
        {
            ShoppingCart743Entities context = new ShoppingCart743Entities();
            ProductVisit cartItem = (from pv in context.ProductVisits
                               where sessionID == pv.sessionID && pv.productID == productID
                               select pv).FirstOrDefault();
            return cartItem;
        }

        public void AddOrUpdateProductVisit(ProductVisit formProductVisit)
        {
            ShoppingCart743Entities context = new ShoppingCart743Entities();

            ProductVisit dbProductVisit = (from pv in context.ProductVisits
                                           where formProductVisit.sessionID == pv.sessionID && formProductVisit.productID == pv.productID
                                           select pv).FirstOrDefault();
            

            if (dbProductVisit != null) {
                dbProductVisit.productID = formProductVisit.productID;
                dbProductVisit.qtyOrdered = formProductVisit.qtyOrdered;
                dbProductVisit.sessionID = formProductVisit.sessionID;
                dbProductVisit.updated = formProductVisit.updated;

                //update the time for all of the session items
                SessionHelper sessionHelper = new SessionHelper();
                var dbProductVisits = (from pv in context.ProductVisits
                                      where formProductVisit.sessionID == pv.sessionID
                                      select pv);
                foreach (ProductVisit productVisit in dbProductVisits)
                {
                    productVisit.updated = sessionHelper.EndTime;
                }

                context.SaveChanges();
            }
            else
            {
                SessionHelper sessionHelper = new SessionHelper();
                Visit visit = new Visit();
                visit.started = sessionHelper.StartTime;
                visit.sessionID = sessionHelper.SessionID;

                Visit dbVisit = (from pv in context.Visits
                                               where formProductVisit.sessionID == pv.sessionID
                                               select pv).FirstOrDefault();
                if (dbVisit == null)
                {
                    context.Visits.Add(visit);
                }

                context.ProductVisits.Add(formProductVisit);

                //update the session time for all of the sessions-- if the product, session ID combo doesn't exist
                var dbProductVisits = (from pv in context.ProductVisits
                                       where formProductVisit.sessionID == pv.sessionID
                                       select pv);
                foreach (ProductVisit productVisit in dbProductVisits)
                {
                    productVisit.updated = sessionHelper.StartTime;
                }


                context.SaveChanges();
            }
        }
    }
}