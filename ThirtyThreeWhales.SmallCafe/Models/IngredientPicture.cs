using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThirtyThreeWhales.SmallCafe.Models
{
    public class IngredientPicture
    {
        public int IngrPicId { get; set; }

        [Key]
        public int IngrId { get; set; }
        public byte[] Picture { get; set; }
    }
}
