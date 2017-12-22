using Microsoft.AspNetCore.Mvc;
using System;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Controllers {
    [Produces("application/json")]
    [Route("api")]
    public class IngredientsController : Controller
    {
        private IIndependentEntityDbService<Ingredient> _dbService;

        public IngredientsController( IIndependentEntityDbService<Ingredient> dbService ) {
            _dbService = dbService;
        }

        [HttpGet]
        [Route( "Ingredients" )]
        public JsonResult GetAll() {
            return Json( _dbService.GetAll() );
        }

        [HttpGet]
        [Route( "Ingredients/{id}" )]
        public JsonResult GetIngredientById(int id) {
            return Json( _dbService.GetElementById(id) );
        }

        [HttpPost]
        [Route( "Ingredients" )]
        public JsonResult AddNewIngredient( [FromBody] Ingredient ingredient ) {
            return Json( _dbService.CreateNewElement( ingredient ) );
        }

        [HttpPut]
        [Route( "Ingredients" )]
        public JsonResult UpdateIngredient( [FromBody] Ingredient ingredient ) {
            return Json( _dbService.UpdateElement( ingredient ) );
        }

        [HttpDelete]
        [Route( "Ingredients/{id}" )]
        public IActionResult DeleteIngredient( int id ) {
            try {
                _dbService.DeleteElement( new Ingredient() { IngredientID = id } );
            } catch ( Exception e ) {
                //TODO: logger
                return StatusCode( 500, $"Exception: {e.Message}" );
            }
            return Ok();
        }
    }
}