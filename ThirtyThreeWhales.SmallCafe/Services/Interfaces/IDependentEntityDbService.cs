using System.Collections.Generic;

namespace ThirtyThreeWhales.SmallCafe.Services.Interfaces {
    public interface IDependentEntityDbService<T>
    {
        IList<T> GetAllElementsByParentElementId( int id );
        IList<T> GetSpecificElementsByParentElementId( int parentId, int elementId );
        T CreateNewDependantElement( T element );
        T UpdateExistingDependantElement( T element );
        void DeleteDependantElement( T element );

    }
}
