using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirtyThreeWhales.SmallCafe.Data;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Services
{
    public class RecipePictureDbService : IDbService<RecipePicture> {
        private CafeDbContext _dbContext;

        public RecipePictureDbService( CafeDbContext dbContext ) {
            _dbContext = dbContext;
        }
        public RecipePicture CreateNewElement( RecipePicture element ) {
            _dbContext.Add( element );
            _dbContext.SaveChanges();
            return element;
        }

        public bool DeleteElement( int id ) {
            throw new NotImplementedException();
        }

        public IList<RecipePicture> GetAll() {
            throw new NotImplementedException();
        }

        public RecipePicture GetElementById( int id ) {
            throw new NotImplementedException();
        }

        public RecipePicture UpdateElement( RecipePicture element ) {
            throw new NotImplementedException();
        }
    }
}
