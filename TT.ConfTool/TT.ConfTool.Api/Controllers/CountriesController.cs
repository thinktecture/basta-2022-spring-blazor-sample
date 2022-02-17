using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TT.ConfTool.Api.Controllers
{
    [Authorize("api")]
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var countries = new List<string>(){
                "Germany",
                "Austria",
                "Switzerland",
                "Belgium",
                "Netherlands",
                "USA",
                "England"
            };

            return countries;
        }

    }
}
