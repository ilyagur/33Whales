using System;
using System.Collections.Generic;
using System.Linq;
using ThirtyThreeWhales.SmallCafe.Data;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Services {
    public class RecipesDbService : IDbService<Recipe> {

        private CafeDbContext _dbContext;

        public RecipesDbService( CafeDbContext dbContext ) {
            _dbContext = dbContext;
        }

        public Recipe CreateNewElement( Recipe element ) {
            _dbContext.Add(element);
            _dbContext.SaveChanges();

            return element;
        }

        public bool DeleteElement( int id ) {
            try {
                Recipe recipeToDelete = GetElementById( id );

                if ( recipeToDelete == null ) {
                    return false;
                }

                _dbContext.Remove( recipeToDelete );
                _dbContext.SaveChanges();

            } catch ( Exception ) {
                return false;
                //logger
            }
            return true;
        }

        public IList<Recipe> GetAll() {
            return _dbContext.Recipes.ToList();
        }

        public Recipe GetElementById( int id ) {
            return _dbContext.Recipes.FirstOrDefault( r => r.RcpId == id );
        }

        public Recipe UpdateElement( Recipe element ) {
            _dbContext.Update(element);
            _dbContext.SaveChanges();

            return element;
        }
    }
}
