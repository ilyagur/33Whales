using System.Collections.Generic;

namespace ThirtyThreeWhales.SmallCafe.Services.Interfaces {
    /// <summary>
    /// Provides CRUD methods for dependant entities
    /// </summary>
    public interface IDependentEntityDbService<T>
    {
        /// <summary>
        /// Create a new entity in data base
        /// <summary>
        /// <param name="element">The entity to add</param>
        /// <returns>
        /// Newly created entity
        /// </returns>
        T CreateElement( T element );

        /// <summary>
        /// Read all dependant entities selected by parent ID
        /// </summary>
        /// <param name="id">Parent ID</param>
        /// <returns>
        /// List of entities
        /// </returns>    
        IList<T> ReadAllElementsByParentElementId( int id );

        /// <summary>
        /// Updating existing entity in data base
        /// </summary>
        /// <param name="element">The entity to update</param>
        /// <returns>
        /// Newly updated entity
        /// </returns>    
        T UpdateElement( T element );

        /// <summary>
        /// Delete an entity from data base
        /// </summary>
        /// <param name="element">The entity to delete</param>
        /// <returns>
        /// Bool value that indicates are there any mistakes occurred
        /// </returns>     
        bool DeleteElement( T element );

    }
}
