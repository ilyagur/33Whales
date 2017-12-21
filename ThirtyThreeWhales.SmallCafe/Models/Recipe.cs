using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThirtyThreeWhales.SmallCafe.Models
{
    public class Recipe
    {
        [Key]
        public int RcpId { get; set; }
        public string Name { get; set; }

        public int FinishedProduct { get; set; }
        public ICollection<IngredientPicture> Pictures { get; set; }
    }
}
