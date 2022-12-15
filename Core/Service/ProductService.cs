using Core.Interface;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    internal class ProductService : IProductService
    {
        public Task<List<Product>> GetAllProducts()
        {
            throw new NotImplementedException();
        }
    }
}
