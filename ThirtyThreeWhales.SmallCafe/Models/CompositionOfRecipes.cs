using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThirtyThreeWhales.SmallCafe.Models
{
    public class CompositionOfRecipes
    {
        public int IngredientID { get; set; }
        public int RecipeID { get; set; }
        public decimal Quantity { get; set; }
        public virtual Ingredient Ingredients { get; set; }
        public virtual Recipe Recipes { get; set; }
    }
}
