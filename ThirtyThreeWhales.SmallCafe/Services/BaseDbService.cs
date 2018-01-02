using Microsoft.Extensions.Logging;
using System;
using ThirtyThreeWhales.SmallCafe.Data;

namespace ThirtyThreeWhales.SmallCafe.Services {

    /// <summary>
    /// Base class for all data base services. Provide common methods such as:
    /// CreateElement, UpdateElement, DeleteElement
    /// </summary>
    public class BaseDbService<T> where T : class {
        protected IDbContext _dbContext;
        protected ILogger<BaseDbService<T>> _logger;
        public BaseDbService( IDbContext dbContext, ILogger<BaseDbService<T>> logger ) {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Create a new entity in data base
        /// <summary>
        /// <param name="element">The entity to add.</param>
        /// <returns>
        /// Newly created entity
        /// </returns>
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

        /// <summary>
        /// Updating existing entity in data base
        /// </summary>
        /// <param name="element">The entity to update.</param>
        /// <returns>
        /// Newly updated entity
        /// </returns>     
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

        /// <summary>
        /// Delete an entity from data base
        /// </summary>
        /// <param name="element">The entity to delete</param>
        /// <returns>
        /// Bool value that indicates are there any mistakes occurred
        /// </returns>     
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
