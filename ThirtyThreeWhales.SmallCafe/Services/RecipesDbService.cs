using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ThirtyThreeWhales.SmallCafe.Data;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Services {
   
    /// <summary>
    /// Provides CRUD operations for recipes
    /// </summary>
    public class RecipesDbService : BaseDbService<Recipe>, IIndependentEntityDbService<Recipe> {
        public RecipesDbService( IDbContext dbContext, ILogger<RecipesDbService> logger ) : base( dbContext, logger ) { }

        /// <summary>
        /// Read all recipes
        /// </summary>
        /// <returns>
        /// List of recipes
        /// </returns>
        public IList<Recipe> ReadAllElements() {

            IList<Recipe> recipes = new List<Recipe>();

            try {
                recipes = _dbContext.Recipes.ToList();
            } catch ( Exception e ) {
                _logger.LogWarning( "Exception", e );
                return null;
            }

            return recipes;
        }

        /// <summary>
        /// Read recipe with corresponding id
        /// </summary>
        /// <param name="id">Id of recipe</param>
        /// <returns>
        /// Recipe
        /// </returns>
        public Recipe ReadElementById( int id ) {

            if ( id <= 0 ) {
                return null;
            }

            Recipe recipe = new Recipe();
            try {
                recipe = _dbContext.Recipes.FirstOrDefault( r => r.RecipeID == id );
            } catch ( Exception e ) {
                _logger.LogWarning( "Exception", e );
                return null;
            }

            return recipe;
        }
    }
}
