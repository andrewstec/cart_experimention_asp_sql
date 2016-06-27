using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCartApplication.Models;
using ShoppingCartApplication.BusinessLogic;
using ShoppingCartApplication.ViewModels;

namespace ShoppingCartApplication.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            SessionHelper sessionHelper = new SessionHelper();
            ProductRepo repo = new ProductRepo();
            if (Request.Cookies["ASP.NET_SessionId"] == null)
            {
                sessionHelper.InitializeSession();
            }
            else
            {
                sessionHelper.UpdateSession();
            }
            sessionHelper.InitializeSession();
            string sessionID = System.Web.HttpContext.Current.Session.SessionID;
            ViewBag.SessionID = sessionHelper.SessionID;
            ViewBag.SessionIDTest = sessionID;
            return View(repo.GetAllProducts());
        }

        [HttpGet]
        public ActionResult Add(int id)
        {

            SessionHelper sessionHelper = new SessionHelper();
            ViewBag.SessionID = System.Web.HttpContext.Current.Session.SessionID;

            ProductVisitRepo productVisitRepo = new ProductVisitRepo();
            ProductVisit productVisitFromDB = productVisitRepo.GetCartItem(System.Web.HttpContext.Current.Session.SessionID, id);

            if (productVisitFromDB != null )
            {
                ViewBag.Qty = productVisitFromDB.qtyOrdered;
            }
            else
            {
                ViewBag.Qty = 1;
            }


            ProductRepo productRepo = new ProductRepo();
            RedirectToAction("ViewCart");
            return View(productRepo.GetProduct(id));

        }

        [HttpPost]
        public ActionResult Add(ProductVisit productVisitFromForm)
        {
            SessionHelper sessionHelper = new SessionHelper();
            productVisitFromForm.sessionID = sessionHelper.SessionID; 
            productVisitFromForm.updated = sessionHelper.StartTime;

            ProductVisitRepo productVisitRepo = new ProductVisitRepo();
            if (productVisitFromForm.productID != 0 && productVisitFromForm.sessionID != null) {
                productVisitRepo.AddOrUpdateProductVisit(productVisitFromForm);
            }

            ProductVisit productVisitFromDB = productVisitRepo.GetCartItem(sessionHelper.SessionID, productVisitFromForm.productID);

            ViewBag.Qty = 1;
            if (productVisitFromDB.qtyOrdered != 1)
            {
                ViewBag.Qty = productVisitFromDB.qtyOrdered;
            }

            return RedirectToAction("ViewCart", new { id = sessionHelper.SessionID });

        }

        public ActionResult ViewCart(string id)
        {
            ProductVisitCartRepo productVisitCartRepo = new ProductVisitCartRepo();
            return View(productVisitCartRepo.GetCartList(System.Web.HttpContext.Current.Session.SessionID));
        }

        [HttpPost]
        public ActionResult ViewCart(List<ProductVisitCartVM> cart)
        {
            SessionHelper sessionHelper = new SessionHelper();
            ProductVisitCartRepo productVisitCartRepo = new ProductVisitCartRepo();
            productVisitCartRepo.SendCartList(sessionHelper.SessionID, cart);
            return RedirectToAction("Index", new { id = sessionHelper.SessionID });
        }

        [HttpGet]
        public ActionResult RemoveItem(int id)
        {

            SessionHelper sessionHelper = new SessionHelper();
            ProductVisitCartRepo productVisitCartRepo = new ProductVisitCartRepo();
            productVisitCartRepo.RemoveItemFromCart(sessionHelper.SessionID, id);
            return RedirectToAction("ViewCart", new { id = sessionHelper.SessionID });

        }

        public ActionResult ThankYou()
        {
            SessionHelper sessionHelper = new SessionHelper();
            VisitDestroyerRepo repo = new VisitDestroyerRepo();
            repo.RemoveCartBySessionNoTime(sessionHelper.SessionID);
            return View();
        }
    }
}