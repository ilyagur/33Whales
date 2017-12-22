using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Controllers
{
    [Produces( "application/json" )]
    [Route( "api" )]
    public class IngredientPicturesController : Controller {
        private IDbService<IngredientPicture> _dbService;
        public IngredientPicturesController( IDbService<IngredientPicture> dbService ) {
            _dbService = dbService;
        }


        public JsonResult GetPicturesForIngredient() {
            return null;
        }

        [HttpPost]
        [Route( "Ingredients/{ingrId}/Pictures" )]
        public JsonResult AddPictureForIngredient(int ingrId, IFormFile picture ) {
            using (MemoryStream stream = new MemoryStream()) {
                picture.CopyTo( stream );
                var bytes = stream.ToArray();
                _dbService.CreateNewElement(new IngredientPicture() { IngrId = ingrId, Picture = bytes } );
            }
            return null;
        }
    }
}