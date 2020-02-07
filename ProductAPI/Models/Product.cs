using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Models
{
    /// <summary>
    /// Product definition
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Product ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Product Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Product price
        /// </summary>
        public decimal Price { get; set; }
    }
}
