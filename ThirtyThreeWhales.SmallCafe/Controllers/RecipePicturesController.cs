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
    [Produces("application/json")]
    [Route("api")]
    public class RecipePicturesController : Controller
    {
        private IDbService<RecipePicture> _dbService;
        public RecipePicturesController( IDbService<RecipePicture> dbService ) {
            _dbService = dbService;
        }


        public JsonResult GetPicturesForRecipe() {
            return null;
        }

        [HttpPost]
        [Route( "Recipes/{rcpId}/Pictures" )]
        public JsonResult AddPictureForIngredient( int rcpId, IFormFile picture ) {
            using ( MemoryStream stream = new MemoryStream() ) {
                picture.CopyTo( stream );
                var bytes = stream.ToArray();
                _dbService.CreateNewElement( new RecipePicture() { RcpId = rcpId, Picture = bytes } );
            }
            return null;
        }
    }
}