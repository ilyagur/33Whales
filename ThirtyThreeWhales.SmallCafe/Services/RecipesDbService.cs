using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ThirtyThreeWhales.SmallCafe.Data;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Services {
    public class RecipesDbService : BaseDbService<Recipe>, IIndependentEntityDbService<Recipe> {
        public RecipesDbService( CafeDbContext dbContext, ILogger<RecipesDbService> logger ) : base( dbContext, logger ) { }

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

        public Recipe ReadElementById( int id ) {

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
