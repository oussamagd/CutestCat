using CutestCat.Business;
using CutestCat.Models;
using CutestCat.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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
        public ActionResult<List<CadidateCatViewModel>> GetCandidates()
        {
            var catsForVote = _catBusiness.GetCandidates();
            return Ok(catsForVote.Select(cat => new CadidateCatViewModel(cat)));
        }

        [HttpPost]
        [Route("Vote")]
        public IActionResult Vote([FromBody]VoteViewModel model)
        {
            _catBusiness.Vote(model.ToModel());
            return NoContent();
        }
    }
}