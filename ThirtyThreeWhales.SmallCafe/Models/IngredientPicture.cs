using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThirtyThreeWhales.SmallCafe.Models {
    public class IngredientPicture
    {
        [Key]
        public int IngredientPictureID { get; set; }
        public int IngredientID { get; set; }
        public byte[] Picture { get; set; }
        [ForeignKey( "IngredientID" )]
        public virtual Ingredient Ingredient { get; set; }
    }
}
