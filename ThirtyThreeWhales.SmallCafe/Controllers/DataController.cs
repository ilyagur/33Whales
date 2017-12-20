using Microsoft.AspNetCore.Mvc;
using ThirtyThreeWhales.SmallCafe.Data;

namespace ThirtyThreeWhales.SmallCafe.Controllers {
    [Produces("application/json")]
    [Route("api/Data")]
    public class DataController : Controller
    {
        private CafeDbContext _dbContext;

        public DataController( CafeDbContext dbContext ) {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route( "Ingredients" )]
        public JsonResult Ingredients() {
            return Json( _dbContext.Ingredients );
        }
    }
}