using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirtyThreeWhales.SmallCafe.Data;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Services
{
    public class IngredientPicturesDbService : IDbService<IngredientPicture> {

        private CafeDbContext _dbContext;

        public IngredientPicturesDbService( CafeDbContext dbContext ) {
            _dbContext = dbContext;
        }

        public IngredientPicture CreateNewElement( IngredientPicture element ) {
            _dbContext.Add(element);
            _dbContext.SaveChanges();
            return element;
        }

        public bool DeleteElement( int id ) {
            throw new NotImplementedException();
        }

        public IList<IngredientPicture> GetAll() {
            throw new NotImplementedException();
        }

        public IngredientPicture GetElementById( int id ) {
            throw new NotImplementedException();
        }

        public IngredientPicture UpdateElement( IngredientPicture element ) {
            throw new NotImplementedException();
        }
    }
}
