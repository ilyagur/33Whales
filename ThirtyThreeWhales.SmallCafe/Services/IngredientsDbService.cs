﻿using System;
using System.Collections.Generic;
using System.Linq;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Data {
    public class IngredientsDbService: IDbService<Ingredient>
    {
        private CafeDbContext _dbContext;

        public IngredientsDbService(CafeDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IList<Ingredient> GetAll() {
            return _dbContext.Ingredients.ToList();
        }

        public Ingredient GetElementById( int id ) {
            return _dbContext.Ingredients.FirstOrDefault(i => i.IngrId == id);
        }

        public Ingredient CreateNewElement( Ingredient ingredient ) {
            _dbContext.Add(ingredient);
            _dbContext.SaveChanges();

            return ingredient;
        }

        public Ingredient UpdateElement( Ingredient ingredient ) {
            _dbContext.Update(ingredient);
            _dbContext.SaveChanges();

            return ingredient;
        }

        public bool DeleteElement( int id ) {
            try {
                Ingredient ingredientToDelete = GetElementById( id );

                if ( ingredientToDelete == null ) {
                    return false;
                }

                _dbContext.Remove( ingredientToDelete );
                _dbContext.SaveChanges();

            } catch ( Exception ) {
                return false;
                //logger
            }
            return true;
        }
    }
}