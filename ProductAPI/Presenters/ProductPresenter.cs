using ProductAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Presenters
{
    /// <summary>
    /// Presenter class for performing CRUD operations on a table of Products in the DB
    /// </summary>
    public static class ProductPresenter
    {
        /// <summary>
        /// Method for getting a list of all the existing products in the db
        /// </summary>
        /// <param name="conn">Connection details</param>
        /// <returns></returns>
        public static List<Product> GetAllProducts(SqlConnection conn)
        {
            SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Products", conn);
            sqlCmd.CommandType = CommandType.Text;
            conn.Open();

            SqlDataReader reader = sqlCmd.ExecuteReader();
            List<Product> products = new List<Product>();
            while (reader.Read())
            {
                Product product = new Product();
                product.Id = new Guid(reader["Id"].ToString());
                product.Name = reader["Name"].ToString();
                product.Price = Convert.ToDecimal(reader["Price"]);
                products.Add(product);
            }
            conn.Close();
            return products;
        }

        /// <summary>
        /// Method for retreiving a single product from the database
        /// </summary>
        /// <param name="id">ID of the desired product</param>
        /// <param name="conn">Connection details</param>
        /// <returns></returns>
        public static Product GetSingleProduct(Guid id, SqlConnection conn)
        {
            SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Products WHERE Id = @Id", conn);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@Id", id);
            conn.Open();

            SqlDataReader reader = sqlCmd.ExecuteReader();
            Product product = new Product();
            while (reader.Read())
            {
                product.Id = new Guid(reader["Id"].ToString());
                product.Name = reader["Name"].ToString();
                product.Price = Convert.ToDecimal(reader["Price"]);
            }
            conn.Close();
            return product;
        }

        /// <summary>
        /// Method for creating a product in the database
        /// </summary>
        /// <param name="name">New product name</param>
        /// <param name="price">New product price</param>
        /// <param name="conn">Connection details</param>
        /// <returns></returns>
        public static string CreateProduct(string name, decimal price, SqlConnection conn)
        {
            SqlCommand sqlCmd = new SqlCommand("INSERT INTO Products (Id, Name, Price) Output Inserted.Id VALUES (@Id, @Name, @Price)", conn);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
            sqlCmd.Parameters.AddWithValue("@Name", name);
            sqlCmd.Parameters.AddWithValue("@Price", price);
            conn.Open();

            SqlDataReader reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                Guid _newProductGuid = new Guid(reader["Id"].ToString());
                conn.Close();
                return _newProductGuid.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// Method for updating values of a single product in the database
        /// </summary>
        /// <param name="id">ID of the db item</param>
        /// <param name="name">New name</param>
        /// <param name="price">New price</param>
        /// <param name="conn">Connection details</param>
        public static void UpdateSingleProduct(Guid id, string name, decimal price, SqlConnection conn)
        {
            SqlCommand sqlCmd = new SqlCommand("UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id", conn);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@Id", id);
            sqlCmd.Parameters.AddWithValue("@Name", name);
            sqlCmd.Parameters.AddWithValue("@Price", price);

            conn.Open();
            sqlCmd.ExecuteNonQuery();
            conn.Close();
        }

        /// <summary>
        /// Method for deleting a single product from the database
        /// </summary>
        /// <param name="id">ID of the product to be deleted</param>
        /// <param name="conn">Connection details</param>
        public static void DeleteSingleProduct(Guid id, SqlConnection conn)
        {
            SqlCommand sqlCmd = new SqlCommand("DELETE FROM Products WHERE Id = @Id", conn);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@Id", id);

            conn.Open();
            sqlCmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
