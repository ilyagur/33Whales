using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThirtyThreeWhales.SmallCafe.Models {
    public class Ingredient
    {
        [Key]
        public int IngrId { get; set; }
        public string Name { get; set; }
        
        public ICollection<IngredientPicture> Pictures { get; set; }
    }
}
