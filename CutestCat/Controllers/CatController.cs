using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CutestCat.Business;
using CutestCat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CutestCat.Controllers
{

    public class CatController : Controller
    {
        private readonly ICatBusiness _catBusiness;

        public CatController(ICatBusiness catBusiness)
        {
            _catBusiness = catBusiness;
        }

        public IActionResult CatsToVote()
        {
            var cats = _catBusiness.GetCats();
            return View(cats);
        }
        public Task<Tuple<Cat,Cat>> Vote()
        {
            var catsForVote = _catBusiness.GetCatsForVoteAsync();
            return catsForVote;
        }

        [HttpPost]
        public async Task<IActionResult> Vote([FromBody]VoteResultModel model)
        {
            await _catBusiness.SendVote(model);
            return Ok("OK");
        }
    }
}