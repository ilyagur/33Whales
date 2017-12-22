using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Controllers {
    [Produces("application/json")]
    [Route("api/")]
    public class CompositionOfRecipesController : Controller
    {
        private IDependentEntityDbService<CompositionOfRecipes> _dbService;

        public CompositionOfRecipesController(IDependentEntityDbService<CompositionOfRecipes> dbService) {
            _dbService = dbService;
        }

        [HttpGet]
        [Route( "Recipes/{recipeID}/Composition" )]
        public JsonResult GetCompositionOfRecipe( int recipeID ) {
            List<string> errors = new List<string>();

            if ( recipeID <= 0 ) {
                errors.Add( "RecipeId is <= 0" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            IList<CompositionOfRecipes> compositionOfRecipes = new List<CompositionOfRecipes>();

            try {
                compositionOfRecipes = _dbService.GetAllElementsByParentElementId( recipeID );
            } catch ( Exception e ) {
                //TODO: logger
                errors.Add( $"Exception: {e.Message}" );

                if ( e.InnerException != null ) {
                    errors.Add( $"Inner Exception: {e.InnerException.Message}" );
                }
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            if ( compositionOfRecipes == null ) {
                return Json( null );
            }

            return Json( compositionOfRecipes.Select( c => new {
                RecipeID = c.RecipeID,
                IngredientID = c.IngredientID,
                Quantity = c.Quantity,
                Ingredients = c.Ingredients
            } ).ToList() );
        }

        [HttpPost]
        [Route( "Recipes/{recipeID}/Composition/{ingredientID}" )]
        public JsonResult AddIngredientToComposition( int recipeID, int ingredientID, [FromBody] CompositionOfRecipes compositionOfRecipes ) {
            List<string> errors = new List<string>();

            if ( recipeID <= 0 ) {
                errors.Add( "RecipeId is <= 0" );
            }

            if ( ingredientID <= 0 ) {
                errors.Add( "IngredientID is <= 0" );
            }

            if ( compositionOfRecipes == null ) {
                errors.Add( "CompositionOfRecipes is NULL" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            CompositionOfRecipes cor = new CompositionOfRecipes();

            try {
                cor = _dbService.CreateNewDependantElement(new CompositionOfRecipes() { RecipeID = recipeID, IngredientID = ingredientID, Quantity = compositionOfRecipes.Quantity } );
            } catch ( Exception e ) {
                //TODO: logger
                errors.Add( $"Exception: {e.Message}" );

                if ( e.InnerException != null ) {
                    errors.Add( $"Inner Exception: {e.InnerException.Message}" );
                }
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            return Json( cor );
        }

        [HttpPut]
        [Route( "Recipes/{recipeID}/Composition/{ingredientID}" )]
        public JsonResult UpdateIngredientInComposition( int recipeID, int ingredientID, [FromBody] CompositionOfRecipes compositionOfRecipes ) {
            List<string> errors = new List<string>();

            if ( recipeID <= 0 ) {
                errors.Add( "RecipeId is <= 0" );
            }

            if ( ingredientID <= 0 ) {
                errors.Add( "IngredientID is <= 0" );
            }

            if ( compositionOfRecipes == null ) {
                errors.Add( "CompositionOfRecipes is NULL" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            CompositionOfRecipes cor = new CompositionOfRecipes();

            try {
                cor = _dbService.UpdateExistingDependantElement( new CompositionOfRecipes() { RecipeID = recipeID, IngredientID = ingredientID, Quantity = compositionOfRecipes.Quantity } );
            } catch ( Exception e ) {
                //TODO: logger
                errors.Add( $"Exception: {e.Message}" );

                if ( e.InnerException != null ) {
                    errors.Add( $"Inner Exception: {e.InnerException.Message}" );
                }
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            return Json( cor );
        }

        [HttpDelete]
        [Route( "Recipes/{recipeID}/Composition/{ingredientID}" )]
        public IActionResult DeleteIngredientForComposition( int recipeID, int ingredientID ) {
            List<string> errors = new List<string>();

            if ( ingredientID <= 0 ) {
                errors.Add( "RecipeID is <= 0" );
            }

            if ( ingredientID <= 0 ) {
                errors.Add( "IngredientID is <= 0" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            try {
                _dbService.DeleteDependantElement( new CompositionOfRecipes() { RecipeID = recipeID, IngredientID = ingredientID } );
            } catch ( Exception e ) {
                //TODO: logger
                return StatusCode( 500, $"Exception: {e.Message}" );
            }
            return Ok();
        }
    }
}