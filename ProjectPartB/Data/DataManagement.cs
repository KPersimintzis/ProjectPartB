using ProjectPartB.Entities;
using ProjectPartB.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPartB.Data
{
    abstract class DataManagement : DataConnection
    {
        private readonly string connectionString = @"Data Source=DESKTOP-D8C09EJ\SQLEXPRESS;Initial Catalog=PrivateSchoolTesting;Integrated Security=True";

        public void CreateData<T>(T entity, string tableName) where T : class
        {

            PropertyInfo[] properties = typeof(T).GetProperties();
            StringBuilder sb = new StringBuilder();
            StringBuilder svalue = new StringBuilder();
            sb.Append(tableName + "(");

            foreach (PropertyInfo a in properties)
            {
                if (a.GetValue(entity) != null && !a.PropertyType.IsGenericType && a.Name != (typeof(T).Name + "Id")) //den tha perasoun properties poy einai null kai properties poy einai generic
                {
                    sb.Append(a.Name + ",");
                    if (a.PropertyType == typeof(String) || a.PropertyType == typeof(DateTime))
                    {
                        if (a.PropertyType == typeof(DateTime))
                        {
                            DateTime dt = (DateTime)(a.GetValue(entity));
                            string date = dt.ToString("MM-dd-yyyy");
                            svalue.Append($"'{date}',");
                        }
                        else { svalue.Append($"'{a.GetValue(entity)}',"); }
                    }
                    else
                    {
                        svalue.Append($"{a.GetValue(entity)},");
                    }
                }
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")");
            svalue.Remove(svalue.Length - 1, 1);
            Console.WriteLine(sb.ToString());
            Console.WriteLine(svalue.ToString());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string queryString = $"INSERT INTO {sb} " +
                        $"VALUES ({svalue})";

                    using (SqlCommand cmd = new SqlCommand(queryString, conn))
                    {
                        //cmd.Parameters.Add(new SqlParameter("@id", id));
                        //cmd.Parameters.Add(new SqlParameter("@name", name));
                        int rowsInserted = cmd.ExecuteNonQuery();
                        if (rowsInserted > 0)
                        {
                            Console.WriteLine("Insertion Successful");
                            Console.WriteLine($"{rowsInserted} rows inserted Successfully");
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
            }
        }

    }
}
