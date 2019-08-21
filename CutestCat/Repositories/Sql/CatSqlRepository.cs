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
            var result = new List<CatSqlObjet>();
            using (var conn = new SqlConnection(_apiConfiguration.Value.CatContext))
            {
                using (var command = new SqlCommand("PS_GetCats", conn) { CommandType = CommandType.StoredProcedure })
                {
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new CatSqlObjet()
                            {
                                Reference = Convert.ToString(reader["Reference"]),
                                Url = Convert.ToString(reader["Url"]),
                                WinVoteCount = Convert.ToInt32(reader["WinVoteCount"]),
                                LostVoteCount = Convert.ToInt32(reader["LostVoteCount"])
                            });
                        }
                    }

                    return result.Select(cat => cat.ToModel()).ToList();
                }
            }
        }

        public void Vote(VoteModel model)
        {
            using (var conn = new SqlConnection(_apiConfiguration.Value.CatContext))
            {
                using (var command = new SqlCommand("PS_InsertVote", conn) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@WinCatReference", model.WinnerCat.Reference);
                    command.Parameters.AddWithValue("@WinCatUrl", model.WinnerCat.Url);
                    command.Parameters.AddWithValue("@LostCatReference", model.LoserCat.Reference);
                    command.Parameters.AddWithValue("@LostCatUrl", model.LoserCat.Url);
                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
