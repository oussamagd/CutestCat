using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CutestCat.Models;
using Microsoft.Extensions.Options;

namespace CutestCat.Repositories.Sql
{
    public class CatSqlRepository : ICatSqlRepository
    {
        private readonly IOptions<ApiConfiguration> _apiConfiguration;
        public CatSqlRepository(IOptions<ApiConfiguration> apiConfiguration)
        {
            _apiConfiguration = apiConfiguration;
        }
        public List<Cat> GetCats()
        {
            using (var conn = new SqlConnection(_apiConfiguration.Value.CatContext))
            using (var command = new SqlCommand("PS_GetCats", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                var result = command.ExecuteReader();

                return new List<Cat>();
            }
        }
    }
}
