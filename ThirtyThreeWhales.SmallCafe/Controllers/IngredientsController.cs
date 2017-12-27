using Microsoft.AspNetCore.Mvc;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Controllers {
    [Produces("application/json")]
    [Route("api")]
    public class IngredientsController : Controller {
        private IIndependentEntityDbService<Ingredient> _dbService;
        public IngredientsController( IIndependentEntityDbService<Ingredient> dbService){
            _dbService = dbService;
        }

        [HttpGet]
        [Route( "Ingredients" )]
        public JsonResult ReadAllIngredients() {
            return Json( _dbService.ReadAllElements() );
        }

        [HttpGet]
        [Route( "Ingredients/{ingredientID}" )]
        public JsonResult ReadIngredientById(int ingredientID) {
            if ( ingredientID <= 0 ) {
                return Json( new { errors = "Ingredient ID is <= 0" } );
            }

            return Json( _dbService.ReadElementById( ingredientID ) );
        }

        [HttpPost]
        [Route( "Ingredients" )]
        public JsonResult AddNewIngredient( [FromBody] Ingredient ingredient ) {
            if ( ingredient == null ) {
                return Json( new { errors = "Ingredient is NULL" } );
            }

            return Json( _dbService.CreateElement( ingredient ) );
        }

        [HttpPut]
        [Route( "Ingredients" )]
        public JsonResult UpdateIngredient( [FromBody] Ingredient ingredient ) {
            if ( ingredient == null ) {
                return Json( new { errors = "Ingredient is NULL" } );
            }

            if ( ingredient.IngredientID <= 0 ) {
                return Json( new { errors = "Ingredient Id cannot be <= 0" } );
            }

            return Json( _dbService.UpdateElement( ingredient ) );
        }

        [HttpDelete]
        [Route( "Ingredients/{ingredientID}" )]
        public IActionResult DeleteIngredient( int ingredientID ) {
            if ( ingredientID <= 0 ) {
                return Json( new { errors = "Ingredient Id cannot be <= 0" } );
            }

            if ( _dbService.DeleteElement( new Ingredient() { IngredientID = ingredientID } ) ) {
                return Ok();
            }

            return StatusCode( 503, "Error while deleting ingredient" );
        }
    }
}