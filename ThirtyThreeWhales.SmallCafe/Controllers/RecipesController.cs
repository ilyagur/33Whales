using Microsoft.AspNetCore.Mvc;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Controllers {
    [Produces("application/json")]
    [Route("api")]
    public class RecipesController : Controller
    {
        private IDbService<Recipe> _dbService;

        public RecipesController( IDbService<Recipe> dbService ) {
            _dbService = dbService;
        }

        [HttpGet]
        [Route( "Recipes" )]
        public JsonResult GetAll() {
            return Json( _dbService.GetAll() );
        }

        [HttpGet]
        [Route( "Recipes/{id}" )]
        public JsonResult GetRecipeById( int id ) {
            return Json( _dbService.GetElementById( id ) );
        }

        [HttpPost]
        [Route( "Recipes" )]
        public JsonResult AddNewRecipe( [FromBody] Recipe recipe ) {
            return Json( _dbService.CreateNewElement( recipe ) );
        }

        [HttpPut]
        [Route( "Recipes" )]
        public JsonResult UpdateRecipe( [FromBody] Recipe recipe ) {
            return Json( _dbService.UpdateElement( recipe ) );
        }

        [HttpDelete]
        [Route( "Recipes/{id}" )]
        public JsonResult DeleteRecipe( int id ) {
            return Json( _dbService.DeleteElement( id ) );
        }
    }
}