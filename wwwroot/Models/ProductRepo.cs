using ShoppingCartApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartApplication.Models
{
    public class ProductRepo
    {
        public ProductVM GetProduct(int productID)
        {
            ShoppingCart743Entities context = new ShoppingCart743Entities();
            ProductVM productVM = (from p in context.Products
                                   where p.productID == productID
                                   select new ProductVM
                                   {
                                       ProductID = p.productID,
                                       ProductName = p.productName,
                                       Price = p.price
                                   }).FirstOrDefault();
            return productVM;
        }

        public IEnumerable<ProductVM> GetAllProducts()
        {
            ShoppingCart743Entities db = new ShoppingCart743Entities();
            IEnumerable<ProductVM> products = from p in db.Products
                             select new ProductVM
                             {
                                 ProductID = p.productID, 
                                 ProductName = p.productName,
                                 Price = p.price,
                             };
            return products; 
        }

    }
}