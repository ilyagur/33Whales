using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThirtyThreeWhales.SmallCafe.Models {
    public class Ingredients
    {
        [Key]
        public int IngrId { get; set; }
        public string Name { get; set; }
        public decimal PricePerUnit { get; set; }
        public string Units { get; set; }
        //public ICollection<IngredientPictures> Pictures { get; set; }
    }
}
