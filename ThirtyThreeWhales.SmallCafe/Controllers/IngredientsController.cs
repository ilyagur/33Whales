using Microsoft.AspNetCore.Mvc;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Controllers {
    [Produces("application/json")]
    [Route("api")]
    public class IngredientsController : Controller
    {
        private IDbService<Ingredient> _dbService;

        public IngredientsController( IDbService<Ingredient> dbService ) {
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
        public JsonResult DeleteIngredient( int id ) {
            return Json( _dbService.DeleteElement( id ) );
        }
    }
}