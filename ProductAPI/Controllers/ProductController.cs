using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductAPI.Models;
using ProductAPI.Presenters;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration configuration;
        SqlConnection connection;

        public ProductController(IConfiguration config)
        {
            configuration = config;
            connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet]
        public List<Product> GetList()
        {
            return ProductPresenter.GetAllProducts(connection);
        }

        [HttpPost]
        public Product GetProductById(Guid id)
        {
            return ProductPresenter.GetSingleProduct(id, connection);
        }

        [HttpPost]
        public string CreateProduct(string name, decimal price)
        {
            return ProductPresenter.CreateProduct(name, price, connection);
        }

        [HttpPost]
        public void UpdateProduct(Guid id, string name, decimal price)
        {
            ProductPresenter.UpdateSingleProduct(id, name, price, connection);
        }

        [HttpPost]
        public void RemoveProduct(Guid id)
        {
            ProductPresenter.DeleteSingleProduct(id, connection);
        }
    }
}
