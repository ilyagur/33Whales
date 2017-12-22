using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThirtyThreeWhales.SmallCafe.Models {
    public class RecipePicture
    {
        [Key]
        public int RecipePictureID { get; set; }
        public int RecipeID { get; set; }
        public byte[ ] Picture { get; set; }
        [ForeignKey( "RecipeID" )]
        public virtual Recipe Recipe { get; set; }
    }
}
