﻿using CutestCat.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace CutestCat.Repositories.Sql
{
    public class CatSqlRepository : ICatSqlRepository
    {
        private readonly string  Cat_Context;

        public CatSqlRepository(IOptions<ApiConfiguration> apiConfiguration)
        {
            Cat_Context = apiConfiguration.Value.CatContext;
        }

        public List<Cat> GetCats()
        {
            return SqlHelper.GetList<CatSqlObjet>(Cat_Context, "PS_GetCats")
                            .Select(cat => cat.ToModel())
                            .OrderByDescending(cat => cat.Score)
                            .ToList();
        }

        public void Vote(VoteModel model)
        {
            var parameters = new Dictionary<string, string>()
            {
                {"@WinCatReference" , model.WinnerCat.Reference},
                {"@WinCatUrl" , model.WinnerCat.Url},
                {"@LostCatReference" , model.LoserCat.Reference},
                {"@LostCatUrl" , model.LoserCat.Url},
            };
            SqlHelper.ExecuteProc<CatSqlObjet>(Cat_Context, "PS_InsertVote", parameters);
        }
    }
}
