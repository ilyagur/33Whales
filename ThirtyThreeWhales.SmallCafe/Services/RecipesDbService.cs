using System;
using System.Collections.Generic;
using System.Linq;
using ThirtyThreeWhales.SmallCafe.Data;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Services {
    public class RecipesDbService : IIndependentEntityDbService<Recipe> {

        private CafeDbContext _dbContext;

        public RecipesDbService( CafeDbContext dbContext ) {
            _dbContext = dbContext;
        }

        public Recipe CreateNewElement( Recipe element ) {
            _dbContext.Add(element);
            _dbContext.SaveChanges();

            return element;
        }

        public void DeleteElement( Recipe element ) {
            _dbContext.Remove( element );
            _dbContext.SaveChanges();
        }

        public IList<Recipe> GetAll() {
            return _dbContext.Recipes.ToList();
        }

        public Recipe GetElementById( int id ) {
            return _dbContext.Recipes.FirstOrDefault( r => r.RecipeID == id );
        }

        public Recipe UpdateElement( Recipe element ) {
            _dbContext.Update(element);
            _dbContext.SaveChanges();

            return element;
        }
    }
}
