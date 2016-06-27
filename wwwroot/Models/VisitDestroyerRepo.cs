using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartApplication.Models
{
    public class VisitDestroyerRepo
    {
        public void RemoveCartBySessionNoTime(string sessionID)
        {
            ShoppingCart743Entities context = new ShoppingCart743Entities();
            Visit visitToRemove = context.Visits.Find(sessionID);

            //only removes items if the updates were not done within the last hour

            if (visitToRemove != null)
            {
                ProductVisit productVisit = (from pv in context.ProductVisits
                                             where pv.sessionID == sessionID
                                             select pv).FirstOrDefault();

                DateTime updateTime = ((DateTime)productVisit.updated);
                DateTime endTime = ((DateTime)productVisit.updated).AddMinutes(59);

                var productVisitsToRemove = from pv in context.ProductVisits
                                            where pv.sessionID == sessionID
                                            select pv;

                    foreach (var product_Visit in productVisitsToRemove)
                    {
                        context.ProductVisits.Remove(product_Visit);
                        context.Visits.Remove(visitToRemove);
                    }
                    context.SaveChanges();
            }
        }

        public void RemoveCartBySession(string sessionID)
        {
            ShoppingCart743Entities context = new ShoppingCart743Entities();
            Visit visitToRemove = context.Visits.Find(sessionID);

            //only removes items if the updates were not done within the last hour

            if (visitToRemove != null)
            {
                ProductVisit productVisit = (from pv in context.ProductVisits
                                            where pv.sessionID == sessionID
                                            select pv).FirstOrDefault();

                DateTime updateTime = ((DateTime)productVisit.updated);
                DateTime endTime = ((DateTime)productVisit.updated).AddMinutes(59);

                var productVisitsToRemove = from pv in context.ProductVisits
                                            where pv.sessionID == sessionID
                                            select pv;


                if (updateTime.AddMinutes(60) <= DateTime.Now)
                {
                    foreach (var product_Visit in productVisitsToRemove)
                    {
                        context.ProductVisits.Remove(product_Visit);
                        context.Visits.Remove(visitToRemove);
                    }
                    context.SaveChanges();
                }


            }
        }

        public void RemoveCartStartupCheck(string sessionID)
        {
            ShoppingCart743Entities context = new ShoppingCart743Entities();

                Visit visitToRemove = (from p in context.Visits
                                       where p.sessionID == sessionID
                                       select p).FirstOrDefault();

                if (visitToRemove != null)
                {
                    var productVisitsToRemove = from pv in context.ProductVisits
                                                where pv.sessionID == sessionID
                                                select pv;

                    if (productVisitsToRemove != null)
                    {

                        foreach (var productVisit in productVisitsToRemove)
                        {


                            DateTime updateTime = ((DateTime)productVisit.updated);;

                            if ( updateTime.AddMinutes(60) < DateTime.Now )
                            {
                                foreach (var productVisitB in productVisitsToRemove)
                                {
                                    context.ProductVisits.Remove(productVisitB);
                                }

                                context.Visits.Remove(visitToRemove);
                                context.SaveChanges();

                            }
                        }
                    }
                }

            }


        }

    }
