using Dapper;
using HW_17.Models.Access;
using HW_17.Models.SQL;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Net;
using System.Reflection;

namespace HW_17.Data
{
    internal class ProductContext
    {
        public IEnumerable<Product> GetAllProducts(OleDbConnection db) =>
            db.Query<Product>("SELECT ID, Email, ProductCode, ProductName FROM Product");

        public void AddProductToTable(string conStr, Product product)
        {
            using (OleDbConnection db = new OleDbConnection(conStr))
            {
                string sqlQuerry = "INSERT INTO Product(Email, ProductCode, ProductName) " +
                                   "VALUES (@email, @productCode,@productName)";

                OleDbCommand cmd = new OleDbCommand(sqlQuerry, db);
                cmd.Parameters.AddWithValue("@email", product.Email);
                cmd.Parameters.AddWithValue("@productCode", product.ProductCode);
                cmd.Parameters.AddWithValue("@productName", product.ProductName);

                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }


        public void UpdateProduct(string conStr, Product product)
        {
            using (OleDbConnection db = new OleDbConnection(conStr))
            {
                string sqlQuerry = "UPDATE Product " +
                                   "SET Email = @email, ProductCode = @productCode, ProductName = @productName " +
                                   "WHERE ID = @id";

                OleDbCommand cmd = new OleDbCommand(sqlQuerry, db);
                cmd.Parameters.AddWithValue("@email", product.Email);
                cmd.Parameters.AddWithValue("@productCode", product.ProductCode);
                cmd.Parameters.AddWithValue("@productName", product.ProductName);
                cmd.Parameters.AddWithValue("@id", product.ID);

                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }
            

        public void RemoveProduct(string conStr, int id)
        {

            using (OleDbConnection db = new OleDbConnection(conStr))
            {
                string sqlQuerry = "DELETE FROM Product " +
                                   "WHERE ID = @id";

                OleDbCommand cmd = new OleDbCommand(sqlQuerry, db);
                cmd.Parameters.AddWithValue("@id", id);

                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }
                        

        public void RemoveAllDataProduct(OleDbConnection db) => db.Query<Product>($"DELETE FROM Product");
    }
}
