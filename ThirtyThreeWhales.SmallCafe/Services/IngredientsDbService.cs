using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Data {

    /// <summary>
    /// Provides CRUD operations for ingredients
    /// </summary>
    public class IngredientsDbService : BaseDbService<Ingredient>, IIndependentEntityDbService<Ingredient> {
        public IngredientsDbService( IDbContext dbContext, ILogger<IngredientsDbService> logger ) : base( dbContext, logger ) { }

        /// <summary>
        /// Read all ingredients
        /// </summary>
        /// <returns>
        /// List of ingredients
        /// </returns>
        public IList<Ingredient> ReadAllElements() {

            IList<Ingredient> ingredients = new List<Ingredient>();

            try {
                ingredients = _dbContext.Ingredients.ToList();
            } catch ( Exception e ) {
                _logger.LogWarning( "Exception", e );
                return null;
            }

            return ingredients;
        }

        /// <summary>
        /// Read ingredient with corresponding id
        /// </summary>
        /// <param name="id">Id of ingredient</param>
        /// <returns>
        /// Ingredient
        /// </returns>
        public Ingredient ReadElementById( int id ) {

            if ( id <= 0 ) {
                return null;
            }

            Ingredient ingredient = new Ingredient();
            try {
                ingredient = _dbContext.Ingredients.FirstOrDefault( r => r.IngredientID == id );
            } catch ( Exception e ) {
                _logger.LogWarning( "Exception", e );
                return null;
            }

            return ingredient;
        }
    }
}
