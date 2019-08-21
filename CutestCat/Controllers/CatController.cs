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
    [Route("api/[controller]")]
    public class CatController : Controller
    {
        private readonly ICatBusiness _catBusiness;

        public CatController(ICatBusiness catBusiness)
        {
            _catBusiness = catBusiness;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<List<Cat>> GetCats()
        {
            var cats = _catBusiness.GetCats();
            return Ok(cats);
        }

        [HttpGet]
        [Route("Vote/Candidates")]
        public ActionResult<List<Cat>> GetCandidates()
        {
            var catsForVote = _catBusiness.GetCandidates();
            return Ok(catsForVote);
        }

        [HttpGet]
        [Route("Vote")]
        public IActionResult Vote(VoteModel model)
        {
            _catBusiness.Vote(model);
            return Ok();
        }
    }
}