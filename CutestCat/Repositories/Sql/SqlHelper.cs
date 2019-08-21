using System;
using System.ComponentModel;
using System.Data;

namespace CutestCat.Repositories.Sql
{
    public static class SqlHelper
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
    }
}
