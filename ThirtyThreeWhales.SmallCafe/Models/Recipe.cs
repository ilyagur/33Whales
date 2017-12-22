using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThirtyThreeWhales.SmallCafe.Models {
    public class Recipe
    {
        [Key]
        public int RecipeID { get; set; }
        public string Name { get; set; }
        public int FinishedProduct { get; set; }
        public virtual ICollection<RecipePicture> Pictures { get; set; }
        public virtual ICollection<CompositionOfRecipes> CompositionOfRecipes { get; set; }
    }
}
