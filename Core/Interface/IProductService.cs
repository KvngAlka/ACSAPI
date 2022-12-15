using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
    }
}
