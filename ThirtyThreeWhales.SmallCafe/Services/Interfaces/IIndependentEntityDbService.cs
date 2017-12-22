using System.Collections.Generic;

namespace ThirtyThreeWhales.SmallCafe.Services.Interfaces {
    public interface IIndependentEntityDbService<T>
    {
        IList<T> GetAll();
        T GetElementById( int id );
        T CreateNewElement( T element );
        T UpdateElement(T element);
        void DeleteElement( T element );
    }
}
