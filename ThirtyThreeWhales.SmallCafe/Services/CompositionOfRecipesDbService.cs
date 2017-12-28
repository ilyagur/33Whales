using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ThirtyThreeWhales.SmallCafe.Data;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Services {
    public class CompositionOfRecipesDbService : BaseDbService<CompositionOfRecipes>, IDependentEntityDbService<CompositionOfRecipes> {
        public CompositionOfRecipesDbService( IDbContext dbContext, ILogger<CompositionOfRecipesDbService> logger ) : base(dbContext, logger) {
        }

        public IList<CompositionOfRecipes> ReadAllElementsByParentElementId( int id ) {

            if ( id <= 0 ) {
                return null;
            }

            IQueryable<CompositionOfRecipes> cor;

            try {
                cor = _dbContext.CompositionOfRecipes
                .Where( cr => cr.RecipeID == id )
                .Select( recipes => new CompositionOfRecipes() {
                    RecipeID = recipes.RecipeID,
                    IngredientID = recipes.IngredientID,
                    Quantity = recipes.Quantity,
                    Ingredient = recipes.Ingredient
                } );
            } catch ( Exception e ) {
                _logger.LogWarning( "Exception in ReadAllElementsByParentElementId method for IngredientPictures", e );
                return null;
            }

            return cor.ToList();
        }
    }
}
