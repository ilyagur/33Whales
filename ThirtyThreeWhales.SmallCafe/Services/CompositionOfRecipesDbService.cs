using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ThirtyThreeWhales.SmallCafe.Data;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Services {
    public class CompositionOfRecipesDbService : BaseDbService<CompositionOfRecipes>, IDependentEntityDbService<CompositionOfRecipes> {
        public CompositionOfRecipesDbService( CafeDbContext dbContext, ILogger<CompositionOfRecipesDbService> logger ) : base(dbContext, logger) {
        }

        public IList<CompositionOfRecipes> ReadAllElementsByParentElementId( int id ) {

            IQueryable<CompositionOfRecipes> cor;

            try {
                cor = _dbContext.CompositionOfRecipes
                .Where( cr => cr.RecipeID == id )
                .Select( recipes => new CompositionOfRecipes() {
                    RecipeID = recipes.RecipeID,
                    IngredientID = recipes.IngredientID,
                    Quantity = recipes.Quantity,
                    Ingredients = recipes.Ingredients
                } );
            } catch ( Exception e ) {
                _logger.LogWarning( "Exception in ReadAllElementsByParentElementId method for IngredientPictures", e );
                return null;
            }

            if ( cor == null ) {
                return null;
            }

            return cor.ToList();
        }
    }
}
