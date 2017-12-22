using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Controllers {
    [Produces("application/json")]
    [Route("api")]
    public class RecipesController : BaseIndependentEntityController<Recipe> {
        public RecipesController( IIndependentEntityDbService<Recipe> dbService, ILogger<RecipesController> logger )
            :base( dbService, logger ) {}

        [HttpGet]
        [Route( "Recipes" )]
        public JsonResult GetAllRecipes() {
            return GetAll();
        }

        [HttpGet]
        [Route( "Recipes/{recipeID}" )]
        public JsonResult GetRecipeById( int recipeID ) {
            return GetEntityById( recipeID );
        }

        [HttpPost]
        [Route( "Recipes" )]
        public JsonResult AddNewRecipe( [FromBody] Recipe recipe ) {
            return AddNewEntity( recipe );
        }

        [HttpPut]
        [Route( "Recipes" )]
        public JsonResult UpdateRecipe( [FromBody] Recipe recipe ) {
            return UpdateEntity(recipe);
        }

        [HttpDelete]
        [Route( "Recipes/{recipeID}" )]
        public IActionResult DeleteRecipe( int recipeID ) {
            return DeleteEntity( new Recipe() { RecipeID = recipeID } );
        }
    }
}