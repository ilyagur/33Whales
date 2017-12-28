using Microsoft.Extensions.Logging;
using System;
using ThirtyThreeWhales.SmallCafe.Data;

namespace ThirtyThreeWhales.SmallCafe.Services {
    public class BaseDbService<T> where T : class {
        protected IDbContext _dbContext;
        protected ILogger<BaseDbService<T>> _logger;
        public BaseDbService( IDbContext dbContext, ILogger<BaseDbService<T>> logger ) {
            _dbContext = dbContext;
            _logger = logger;
        }

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
