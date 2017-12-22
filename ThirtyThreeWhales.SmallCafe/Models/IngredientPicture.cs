using System.ComponentModel.DataAnnotations;

namespace ThirtyThreeWhales.SmallCafe.Models {
    public class IngredientPicture
    {
        [Key]
        public int IngrPicId { get; set; }
        public int IngrId { get; set; }
        public byte[] Picture { get; set; }
    }
}
