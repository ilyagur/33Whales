using System.Collections.Generic;

namespace ThirtyThreeWhales.SmallCafe.Services.Interfaces {
    public interface IIndependentEntityDbService<T>
    {
        T CreateElement( T element );
        IList<T> ReadAllElements();
        T ReadElementById( int id );
        T UpdateElement(T element);
        bool DeleteElement( T element );
    }
}
