using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ThirtyThreeWhales.SmallCafe.Data;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Services {
    public class RecipePictureDbService : BaseDbService<RecipePicture>, IDependentEntityDbService<RecipePicture> {

        public RecipePictureDbService( IDbContext dbContext, ILogger<RecipePictureDbService> logger ) : base( dbContext, logger ) {}

        public IList<RecipePicture> ReadAllElementsByParentElementId( int id ) {

            if ( id <= 0 ) {
                return null;
            }

            IQueryable<RecipePicture> pictures;

            try {
                pictures = _dbContext.RecipePictures.Where( p => p.RecipeID == id );
            } catch ( Exception e ) {
                _logger.LogWarning( "Exception in ReadAllElementsByParentElementId method for RecipePicture", e );
                return null;
            }

            return pictures.ToList();
        }
    }
}
