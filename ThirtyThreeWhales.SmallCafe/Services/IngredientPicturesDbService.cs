using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ThirtyThreeWhales.SmallCafe.Data;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Services {

    /// <summary>
    /// Provides CRUD operations for ingredient pictures
    /// </summary>
    public class IngredientPicturesDbService : BaseDbService<IngredientPicture>, IDependentEntityDbService<IngredientPicture> {

        public IngredientPicturesDbService( IDbContext dbContext, ILogger<IngredientPicturesDbService> logger ) : base( dbContext, logger ) {}

        /// <summary>
        /// Read all pictures for given ingredient
        /// </summary>
        /// <param name="id">Id of ingredient</param>
        /// <returns>
        /// List of pictures
        /// </returns>
        public IList<IngredientPicture> ReadAllElementsByParentElementId( int id ) {

            if ( id <= 0 ) {
                return null;
            }

            IQueryable<IngredientPicture> pictures;

            try {
                pictures = _dbContext.IngredientPictures.Where( p => p.IngredientID == id );
            } catch ( Exception e ) {
                _logger.LogWarning( "Exception in ReadAllElementsByParentElementId method for IngredientPictures", e );
                return null;
            }

            return pictures.ToList();
        }
    }
}
