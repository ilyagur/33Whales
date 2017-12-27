using System.Collections.Generic;

namespace ThirtyThreeWhales.SmallCafe.Services.Interfaces {
    public interface IDependentEntityDbService<T>
    {
        T CreateElement( T element );
        IList<T> ReadAllElementsByParentElementId( int id );
        T UpdateElement( T element );
        bool DeleteElement( T element );

    }
}
