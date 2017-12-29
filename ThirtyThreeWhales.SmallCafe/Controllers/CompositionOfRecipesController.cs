using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Controllers {
    [Produces( "application/json" )]
    [Route( "api/" )]
    public class CompositionOfRecipesController : Controller {
        private IDependentEntityDbService<CompositionOfRecipes> _dbService;

        public CompositionOfRecipesController( IDependentEntityDbService<CompositionOfRecipes> dbService ) {
            _dbService = dbService;
        }

        [HttpGet]
        [Route( "Recipes/{recipeID}/Composition" )]
        public JsonResult ReadCompositionOfRecipe( int recipeID ) {
            List<string> errors = new List<string>();

            if ( recipeID <= 0 ) {
                return Json( new { errors = "RecipeId is <= 0" } );
            }

            IList<CompositionOfRecipes> compositionOfRecipes = _dbService.ReadAllElementsByParentElementId( recipeID );

            if ( compositionOfRecipes == null ) {
                return Json( null );
            }

            return Json( compositionOfRecipes.Select( c => new {
                c.RecipeID,
                c.IngredientID,
                c.Quantity,
                c.Ingredient
            } ).ToList() );
        }

        [HttpPost]
        [Route( "Recipes/{recipeID}/Composition/{ingredientID}" )]
        public JsonResult AddIngredientToComposition( int recipeID, int ingredientID, [FromBody] CompositionOfRecipes compositionOfRecipes ) {

            if ( recipeID <= 0 ) {
                return Json( new { errors = "Recipe Id is <= 0" } );
            }

            if ( ingredientID <= 0 ) {
                return Json( new { errors = "Ingredient ID is <= 0" } );
            }

            if ( compositionOfRecipes == null ) {
                return Json( new { errors = "Composition Of Recipes is NULL" } );
            }

            CompositionOfRecipes cor = _dbService.CreateElement(
                new CompositionOfRecipes() {
                    RecipeID = recipeID,
                    IngredientID = ingredientID,
                    Quantity = compositionOfRecipes.Quantity
                } );

            if ( cor == null ) {
                return null;
            }

            return Json( cor );
        }

        [HttpPut]
        [Route( "Recipes/{recipeID}/Composition/{ingredientID}" )]
        public JsonResult UpdateIngredientInComposition( int recipeID, int ingredientID, [FromBody] CompositionOfRecipes compositionOfRecipes ) {
            List<string> errors = new List<string>();

            if ( recipeID <= 0 ) {
                return Json( new { errors = "RecipeId is <= 0" } );
            }

            if ( ingredientID <= 0 ) {
                return Json( new { errors = "IngredientID is <= 0" } );
            }

            if ( compositionOfRecipes == null ) {
                return Json( new { errors = "CompositionOfRecipes is NULL" } );
            }

            CompositionOfRecipes cor = _dbService.UpdateElement(
                new CompositionOfRecipes() {
                    RecipeID = recipeID,
                    IngredientID = ingredientID,
                    Quantity = compositionOfRecipes.Quantity
                } );

            if ( cor == null ) {
                return null;
            }

            return Json( cor );
        }

        [HttpDelete]
        [Route( "Recipes/{recipeID}/Composition/{ingredientID}" )]
        public IActionResult DeleteIngredientForComposition( int recipeID, int ingredientID ) {

            if ( ingredientID <= 0 ) {
                return Json( new { errors = "RecipeID is <= 0" } );
            }

            if ( ingredientID <= 0 ) {
                return Json( new { errors = "IngredientID is <= 0" } );
            }

            if ( _dbService.DeleteElement( new CompositionOfRecipes() { RecipeID = recipeID, IngredientID = ingredientID } ) ) {
                return Ok();
            }

            return StatusCode( 500, "Error while deleting ingredient from composition" );
        }
    }
}