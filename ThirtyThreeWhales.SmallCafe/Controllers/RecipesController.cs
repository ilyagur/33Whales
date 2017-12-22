using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Controllers {
    [Produces("application/json")]
    [Route("api")]
    public class RecipesController : Controller
    {
        private IIndependentEntityDbService<Recipe> _dbService;
        private ILogger<RecipesController> _logger;

        public RecipesController( IIndependentEntityDbService<Recipe> dbService, ILogger<RecipesController> logger ) {
            _dbService = dbService;
            _logger = logger;
        }

        [HttpGet]
        [Route( "Recipes" )]
        public JsonResult GetAll() {
            List<string> errors = new List<string>();
            IList<Recipe> recipe = new List<Recipe>();

            try {
                recipe = _dbService.GetAll();
            } catch ( Exception e ) {
                string error = $"Exception: {e.Message}" + ( e.InnerException != null ? $" Inner Exception: {e.InnerException.Message}" : "" );
                _logger.LogWarning( error );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            return Json( recipe );
        }

        [HttpGet]
        [Route( "Recipes/{id}" )]
        public JsonResult GetRecipeById( int recipeID ) {
            List<string> errors = new List<string>();

            if ( recipeID <= 0 ) {
                errors.Add( "RecipeID is <= 0" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            Recipe recipe = new Recipe();
            try {
                recipe = _dbService.GetElementById( recipeID );
            } catch ( Exception e ) {
                string error = $"Exception: {e.Message}" + ( e.InnerException != null ? $" Inner Exception: {e.InnerException.Message}" : "" );
                _logger.LogWarning( error );

                errors.Add( error );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            return Json( recipe );
        }

        [HttpPost]
        [Route( "Recipes" )]
        public JsonResult AddNewRecipe( [FromBody] Recipe recipe ) {
            List<string> errors = new List<string>();

            if ( recipe == null ) {
                errors.Add( "Recipe is NULL" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            try {
                recipe = _dbService.CreateNewElement( recipe );
            } catch ( Exception e ) {
                string error = $"Exception: {e.Message}" + ( e.InnerException != null ? $" Inner Exception: {e.InnerException.Message}" : "" );
                _logger.LogWarning( error );

                errors.Add( error );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            return Json( recipe );
        }

        [HttpPut]
        [Route( "Recipes" )]
        public JsonResult UpdateRecipe( [FromBody] Recipe recipe ) {
            List<string> errors = new List<string>();

            if ( recipe == null ) {
                errors.Add( "Recipe is NULL" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            try {
                recipe = _dbService.UpdateElement( recipe );
            } catch ( Exception e ) {
                string error = $"Exception: {e.Message}" + ( e.InnerException != null ? $" Inner Exception: {e.InnerException.Message}" : "" );
                _logger.LogWarning( error );

                errors.Add( error );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            return Json( recipe );
        }

        [HttpDelete]
        [Route( "Recipes/{id}" )]
        public IActionResult DeleteRecipe( int recipeID ) {
            List<string> errors = new List<string>();

            if ( recipeID <= 0 ) {
                errors.Add( "RecipeID is <= 0" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            try {
                _dbService.DeleteElement( new Recipe() { RecipeID = recipeID } );
            } catch ( Exception e ) {
                string error = $"Exception: {e.Message}" + ( e.InnerException != null ? $" Inner Exception: {e.InnerException.Message}" : "" );
                _logger.LogWarning( error );

                return StatusCode( 500, error );
            }
            return Ok();
        }
    }
}