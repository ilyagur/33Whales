using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThirtyThreeWhales.SmallCafe.Models
{
    public class RecipePicture
    {
        [Key]
        public int RcpPicId { get; set; }
        public int RcpId { get; set; }
        public byte[ ] Picture { get; set; }
    }
}
