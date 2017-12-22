using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirtyThreeWhales.SmallCafe.Data;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Services
{
    public class IngredientPicturesDbService : IDependentEntityDbService<IngredientPicture> {

        private CafeDbContext _dbContext;

        public IngredientPicturesDbService( CafeDbContext dbContext ) {
            _dbContext = dbContext;
        }

        public IList<IngredientPicture> GetSpecificElementsByParentElementId( int parentId, int elementId ) {
            throw new System.NotImplementedException();
        }
        public IngredientPicture CreateNewDependantElement( IngredientPicture element ) {
            _dbContext.Add( element );
            _dbContext.SaveChanges();
            return element;
        }

        public IList<IngredientPicture> GetAllElementsByParentElementId( int id ) {
            Ingredient ingredient = _dbContext.Ingredients
                .Where( i => i.IngredientID == id )
                .Select( a => new Ingredient() {
                    Pictures = a.Pictures
                } ).FirstOrDefault();

            if ( ingredient == null || ingredient.Pictures == null ) {
                return null;
            }

            return ingredient.Pictures.ToList();
        }

        public IngredientPicture UpdateExistingDependantElement( IngredientPicture element ) {
            _dbContext.Update( element );
            _dbContext.SaveChanges();

            return element;
        }

        public void DeleteDependantElement( IngredientPicture element ) {
            _dbContext.Remove( element );
            _dbContext.SaveChanges();
        }
    }
}
