using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace CutestCat.Repositories.Sql
{
    public class SqlHelper
    {
        public static T DeserializeObject<T>(IDataReader reader) where T : new()
        {
            var result = new T();
            var propertyDescriptorsCollection = TypeDescriptor.GetProperties(result);

            foreach (var property in result.GetType().GetProperties())
            {
                var propertyDescriptor = propertyDescriptorsCollection.Find(property.Name, true);
                if (reader[property.Name] != DBNull.Value)
                {
                    var value = reader[property.Name].ToString();
                    propertyDescriptor.SetValue(result, propertyDescriptor.Converter.ConvertFromInvariantString(value));
                }
            }

            return result;
        }

        public static void SetParameter(SqlCommand command,Dictionary<string, string> parameters)
        {
            if (parameters != null)
            {
                foreach (KeyValuePair<string, string> parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
            }
        }


        public static List<T> GetList<T>(string context, string storedProcedure, Dictionary<string,string> parameters = null) where T : new()
        {
            var result = new List<T>();
            using (var conn = new SqlConnection(context))
            {
                using (var command = new SqlCommand(storedProcedure, conn) { CommandType = CommandType.StoredProcedure })
                {
                    SetParameter(command, parameters);
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(DeserializeObject<T>(reader));
                        }
                    }

                    return result;
                }
            }
        }

        public static void ExecuteProc<T>(string context, string storedProcedure, Dictionary<string, string> parameters = null) where T : new()
        {
            var result = new List<T>();
            using (var conn = new SqlConnection(context))
            {
                using (var command = new SqlCommand(storedProcedure, conn) { CommandType = CommandType.StoredProcedure })
                {
                    SetParameter(command, parameters);
                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
