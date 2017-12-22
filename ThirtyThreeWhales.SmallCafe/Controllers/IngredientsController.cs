using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Controllers {
    [Produces("application/json")]
    [Route("api")]
    public class IngredientsController : BaseIndependentEntityController<Ingredient> {
        public IngredientsController( IIndependentEntityDbService<Ingredient> dbService, ILogger<IngredientsController> logger )
            : base(dbService, logger){}

        [HttpGet]
        [Route( "Ingredients" )]
        public JsonResult GetAllIngredients() {
            return GetAll();
        }

        [HttpGet]
        [Route( "Ingredients/{ingredientID}" )]
        public JsonResult GetIngredientById(int ingredientID) {
            return GetEntityById( ingredientID );
        }

        [HttpPost]
        [Route( "Ingredients" )]
        public JsonResult AddNewIngredient( [FromBody] Ingredient ingredient ) {
            return AddNewEntity( ingredient );
        }

        [HttpPut]
        [Route( "Ingredients" )]
        public JsonResult UpdateIngredient( [FromBody] Ingredient ingredient ) {
            return UpdateEntity( ingredient );
        }

        [HttpDelete]
        [Route( "Ingredients/{ingredientID}" )]
        public IActionResult DeleteIngredient( int ingredientID ) {
            return DeleteEntity( new Ingredient() { IngredientID = ingredientID } );
        }
    }
}