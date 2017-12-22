using System.Collections.Generic;
using System.Linq;
using ThirtyThreeWhales.SmallCafe.Data;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Services {
    public class RecipePictureDbService : IDependentEntityDbService<RecipePicture> {
        private CafeDbContext _dbContext;

        public RecipePictureDbService( CafeDbContext dbContext ) {
            _dbContext = dbContext;
        }

        public IList<RecipePicture> GetSpecificElementsByParentElementId( int parentId, int elementId ) {
            throw new System.NotImplementedException();
        }
        public RecipePicture CreateNewDependantElement( RecipePicture element ) {
            _dbContext.Add( element );
            _dbContext.SaveChanges();
            return element;
        }

        public IList<RecipePicture> GetAllElementsByParentElementId( int id ) {
            var recipe = _dbContext.Recipes
                .Where( r => r.RecipeID == id )
                .Select( a => new Recipe() {
                                                Pictures = a.Pictures
                                            } ).FirstOrDefault();

            if ( recipe == null || recipe.Pictures == null ) {
                return null;
            }

            return recipe.Pictures.ToList();
        }

        public RecipePicture UpdateExistingDependantElement( RecipePicture element ) {
            _dbContext.Update(element);
            _dbContext.SaveChanges();

            return element;
        }

        public void DeleteDependantElement( RecipePicture element ) {
            _dbContext.Remove( element );
            _dbContext.SaveChanges();
        }
    }
}
