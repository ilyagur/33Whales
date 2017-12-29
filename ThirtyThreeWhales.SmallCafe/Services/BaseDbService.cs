using Microsoft.Extensions.Logging;
using System;
using ThirtyThreeWhales.SmallCafe.Data;

namespace ThirtyThreeWhales.SmallCafe.Services {
    //
    // Summary:
    //     Base class for all data base services. Provide common methods such as:
    //     CreateElement, UpdateElement, DeleteElement
    public class BaseDbService<T> where T : class {
        protected IDbContext _dbContext;
        protected ILogger<BaseDbService<T>> _logger;
        public BaseDbService( IDbContext dbContext, ILogger<BaseDbService<T>> logger ) {
            _dbContext = dbContext;
            _logger = logger;
        }

        //
        // Summary:
        //     Create a new entity to data base
        //
        // Parameters:
        //   element:
        //     The entity to add.
        //
        // Returns:
        //     Newly created entity
        public virtual T CreateElement( T element ) {
            try {
                _dbContext.Add( element );
                _dbContext.SaveChanges();
            } catch ( Exception e ) {
                _logger.LogWarning( $"Exception in CreateElement method for {typeof(T)}", e );
                return null;
            }

            return element;
        }

        //
        // Summary:
        //     Updating existing entity in data base
        //
        // Parameters:
        //   element:
        //     The entity to update.
        //
        // Returns:
        //     Newly updated entity
        public virtual T UpdateElement( T element ) {
            try {
                _dbContext.Update( element );
                _dbContext.SaveChanges();
            } catch ( Exception e ) {
                _logger.LogWarning( $"Exception in UpdateElement method for {typeof( T )}", e );
                return null;
            }

            return element;
        }

        //
        // Summary:
        //     Delete an entity from data base
        //
        // Parameters:
        //   element:
        //     The entity to delete.
        //
        // Returns:
        //     bool value that indicates are there any mistakes occurred
        public virtual bool DeleteElement( T element ) {
            try {
                _dbContext.Remove( element );
                _dbContext.SaveChanges();
            } catch ( Exception e ) {
                _logger.LogWarning( $"Exception in DeleteElement method for {typeof( T )}", e );
                return false;
            }

            return true;
        }
    }
}
