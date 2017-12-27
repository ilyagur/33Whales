using Microsoft.AspNetCore.Mvc;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Controllers {
    [Produces( "application/json" )]
    [Route( "api" )]
    public class RecipesController : Controller {
        private IIndependentEntityDbService<Recipe> _dbService;
        public RecipesController( IIndependentEntityDbService<Recipe> dbService ) {
            _dbService = dbService;
        }

        [HttpGet]
        [Route( "Recipes" )]
        public JsonResult ReadAllRecipes() {
            return Json( _dbService.ReadAllElements() );
        }

        [HttpGet]
        [Route( "Recipes/{recipeID}" )]
        public JsonResult ReadRecipeById( int recipeID ) {
            if ( recipeID <= 0 ) {
                return Json( new { errors = "Recipe ID is <= 0" } );
            }

            return Json( _dbService.ReadElementById( recipeID ) );
        }

        [HttpPost]
        [Route( "Recipes" )]
        public JsonResult AddNewRecipe( [FromBody] Recipe recipe ) {
            if ( recipe == null ) {
                return Json( new { errors = "Recipe is NULL" } );
            }

            return Json( _dbService.CreateElement( recipe ) );
        }

        [HttpPut]
        [Route( "Recipes" )]
        public JsonResult UpdateRecipe( [FromBody] Recipe recipe ) {
            if ( recipe == null ) {
                return Json( new { errors = "Recipe is NULL" } );
            }

            if ( recipe.RecipeID <= 0 ) {
                return Json( new { errors = "Recipe Id cannot be <= 0" } );
            }

            return Json( _dbService.UpdateElement( recipe ) );
        }

        [HttpDelete]
        [Route( "Recipes/{recipeID}" )]
        public IActionResult DeleteRecipe( int recipeID ) {

            if ( recipeID <= 0 ) {
                return Json( new { errors = "Recipe Id cannot be <= 0" } );
            }

            if ( _dbService.DeleteElement( new Recipe() { RecipeID = recipeID } ) ) {
                return Ok();
            }

            return StatusCode( 503, "Error while deleting recipe" );
        }
    }
}