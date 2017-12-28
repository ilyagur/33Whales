using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirtyThreeWhales.SmallCafe.Tests {
    public class Utils {
        public static Mock<DbSet<T>> CreateMockDbSet<T>( IList<T> data ) where T: class {

            IQueryable queryableData = data.AsQueryable();

            var corMock = new Mock<DbSet<T>>();
            corMock.As<IQueryable<T>>().Setup( m => m.Provider ).Returns( queryableData.Provider );
            corMock.As<IQueryable<T>>().Setup( m => m.Expression ).Returns( queryableData.Expression );
            corMock.As<IQueryable<T>>().Setup( m => m.ElementType ).Returns( queryableData.ElementType );
            //corMock.As<IQueryable<T>>().Setup( m => m.GetEnumerator() ).Returns( queryableData.GetEnumerator() );

            return corMock;
        }
    }
}
