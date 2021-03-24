using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;


namespace ProjectPartB.Data
{

    abstract class DataConnection : InputUser
    {
        private readonly string connectionString = @"Data Source=DESKTOP-D8C09EJ\SQLEXPRESS;Initial Catalog=PrivateSchoolTesting;Integrated Security=True";

        private DataTable Loader(DataTable dt, string query)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine($"SqlException {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"General exception {e.Message}");
                }
                return dt;
            }
        }

        protected List<T> GetAll<T>(string query) where T : class, new()
        {
            List<T> prlist = new List<T>();
            DataTable dt = new DataTable();
            Loader(dt, query);
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (DataRow row in dt.Rows)
            {
                T obj = new T();
                foreach (DataColumn column in dt.Columns)
                {
                    foreach (PropertyInfo property in properties)
                    {
                        if (column.ColumnName.Equals(property.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            property.SetValue(obj, row[column]);   // Convert.ChangeType(row[column], property.PropertyType));
                            break;
                        }
                    }
                }
                prlist.Add(obj);
            }
            return prlist;
        }
    }
}
