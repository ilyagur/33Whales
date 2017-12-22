using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThirtyThreeWhales.SmallCafe.Models {
    public class Ingredient
    {
        [Key]
        public int IngredientID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<IngredientPicture> Pictures { get; set; }
    }
}
