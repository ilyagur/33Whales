using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Controllers {
    [Produces("application/json")]
    [Route("api")]
    public class IngredientsController : Controller
    {
        private IIndependentEntityDbService<Ingredient> _dbService;
        private ILogger<IngredientsController> _logger;
        public IngredientsController( IIndependentEntityDbService<Ingredient> dbService, ILogger<IngredientsController> logger ) {
            _dbService = dbService;
            _logger = logger;
        }

        [HttpGet]
        [Route( "Ingredients" )]
        public JsonResult GetAll() {
            List<string> errors = new List<string>();
            IList<Ingredient> ingredients = new List<Ingredient>();

            try {
                ingredients = _dbService.GetAll();
            } catch ( Exception e ) {
                string error = $"Exception: {e.Message}" + ( e.InnerException != null ? $" Inner Exception: {e.InnerException.Message}" : "" );
                _logger.LogWarning( error );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            return Json( ingredients );
        }

        [HttpGet]
        [Route( "Ingredients/{ingredientID}" )]
        public JsonResult GetIngredientById(int ingredientID) {
            List<string> errors = new List<string>();

            if ( ingredientID <= 0 ) {
                errors.Add( "IngredientID is <= 0" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            Ingredient ingredient = new Ingredient();
            try {
                ingredient = _dbService.GetElementById( ingredientID );
            } catch ( Exception e ) {
                string error = $"Exception: {e.Message}" + ( e.InnerException != null ? $" Inner Exception: {e.InnerException.Message}" : "" );
                _logger.LogWarning( error );

                errors.Add( error );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            return Json( ingredient );
        }

        [HttpPost]
        [Route( "Ingredients" )]
        public JsonResult AddNewIngredient( [FromBody] Ingredient ingredient ) {
            List<string> errors = new List<string>();

            if ( ingredient == null ) {
                errors.Add( "Ingredient is NULL" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            try {
                ingredient = _dbService.CreateNewElement( ingredient );
            } catch ( Exception e ) {
                string error = $"Exception: {e.Message}" + ( e.InnerException != null ? $" Inner Exception: {e.InnerException.Message}" : "" );
                _logger.LogWarning( error );

                errors.Add( error );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            return Json( ingredient );
        }

        [HttpPut]
        [Route( "Ingredients" )]
        public JsonResult UpdateIngredient( [FromBody] Ingredient ingredient ) {
            List<string> errors = new List<string>();

            if ( ingredient == null ) {
                errors.Add( "Ingredient is NULL" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            try {
                ingredient = _dbService.UpdateElement( ingredient );
            } catch ( Exception e ) {
                string error = $"Exception: {e.Message}" + ( e.InnerException != null ? $" Inner Exception: {e.InnerException.Message}" : "" );
                _logger.LogWarning( error );

                errors.Add( error );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            return Json( ingredient );
        }

        [HttpDelete]
        [Route( "Ingredients/{id}" )]
        public IActionResult DeleteIngredient( int ingredientID ) {
            List<string> errors = new List<string>();

            if ( ingredientID <= 0 ) {
                errors.Add( "IngredientID is <= 0" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            try {
                _dbService.DeleteElement( new Ingredient() { IngredientID = ingredientID } );
            } catch ( Exception e ) {
                string error = $"Exception: {e.Message}" + ( e.InnerException != null ? $" Inner Exception: {e.InnerException.Message}" : "" );
                _logger.LogWarning( error );

                return StatusCode( 500, error );
            }
            return Ok();
        }
    }
}