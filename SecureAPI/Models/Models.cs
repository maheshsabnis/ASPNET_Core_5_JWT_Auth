using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureAPI.Models
{
    
    public class ProductList : List<Product>
    {
        public ProductList()
        {
            Add(new Product { ProductId=1, ProductName ="P1",Price=11000});
            Add(new Product { ProductId = 2, ProductName = "P2", Price = 12000 });
        }
    }
}
