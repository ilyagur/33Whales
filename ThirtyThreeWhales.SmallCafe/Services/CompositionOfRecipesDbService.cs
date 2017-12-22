using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirtyThreeWhales.SmallCafe.Data;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Services
{
    public class CompositionOfRecipesDbService : IDependentEntityDbService<CompositionOfRecipes> {

        private CafeDbContext _dbContext;
        public CompositionOfRecipesDbService( CafeDbContext dbContext ) {
            _dbContext = dbContext;
        }
        public CompositionOfRecipes CreateNewDependantElement( CompositionOfRecipes element ) {
            _dbContext.Add( element );
            _dbContext.SaveChanges();
            return element;
        }

        public void DeleteDependantElement( CompositionOfRecipes element ) {
            _dbContext.Remove( element );
            _dbContext.SaveChanges();
        }

        public IList<CompositionOfRecipes> GetAllElementsByParentElementId( int id ) {
            List<CompositionOfRecipes> compositionOfRecipes = _dbContext.CompositionOfRecipes
                .Where( cr => cr.RecipeID == id )
                .Select(cor => new CompositionOfRecipes() {
                    RecipeID = cor.RecipeID,
                    IngredientID = cor.IngredientID,
                    Quantity = cor.Quantity,
                    Ingredients = cor.Ingredients
                } ).ToList();

            if ( compositionOfRecipes == null ) {
                return null;
            }

            return compositionOfRecipes.ToList();
        }

        public IList<CompositionOfRecipes> GetSpecificElementsByParentElementId( int parentId, int elementId ) {
            throw new NotImplementedException();
        }

        public CompositionOfRecipes UpdateExistingDependantElement( CompositionOfRecipes element ) {
            _dbContext.Update( element );
            _dbContext.SaveChanges();

            return element;
        }
    }
}
