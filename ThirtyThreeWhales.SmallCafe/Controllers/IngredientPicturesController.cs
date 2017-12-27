using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Controllers {
    [Produces( "application/json" )]
    [Route( "api" )]
    public class IngredientPicturesController : Controller {
        private IDependentEntityDbService<IngredientPicture> _dbService;
        private ILogger<IngredientPicturesController> _logger;
        public IngredientPicturesController( IDependentEntityDbService<IngredientPicture> dbService, ILogger<IngredientPicturesController> logger ) {
            _dbService = dbService;
            _logger = logger;
        }

        [HttpGet]
        [Route( "Ingredients/{ingredientID}/Pictures" )]
        public JsonResult ReadPicturesForIngredient( int ingredientID ) {

            if ( ingredientID <= 0 ) {
                return Json( new { errors = "IngredientID is <= 0" } );
            }

            IList<IngredientPicture> ingredientPicture = _dbService.ReadAllElementsByParentElementId( ingredientID );

            if ( ingredientPicture == null ) {
                return Json( null );
            }

            return Json( ingredientPicture.Select( r => new {
                r.IngredientPictureID,
                r.IngredientID,
                Picture = Convert.ToBase64String( r.Picture )
            } ).ToList() );
        }

        [HttpPost]
        [Route( "Ingredients/{ingredientID}/Pictures" )]
        public JsonResult AddPictureForIngredient( int ingredientID, IFormFile picture ) {
            if ( ingredientID <= 0 ) {
                return Json( new { errors = "Ingredient Id is <= 0" } );
            }

            if ( picture == null ) {
                return Json( new { errors = "Ingredient Id is null" } );
            }

            byte[ ] pictureBytes;
            using ( MemoryStream stream = new MemoryStream() ) {
                picture.CopyTo( stream );
                pictureBytes = stream.ToArray();
            }

            IngredientPicture ingredientPicture = _dbService.CreateElement( new IngredientPicture() { IngredientID = ingredientID, Picture = pictureBytes } );

            if ( ingredientPicture == null ) {
                return Json( null );
            }

            return Json( new {
                ingredientPicture.IngredientID,
                ingredientPicture.IngredientPictureID,
                Picture = Convert.ToBase64String( ingredientPicture.Picture )
            } );
        }

        [HttpPut]
        [Route( "Ingredients/{ingredientID}/Pictures/{ingredientPictureID}" )]
        public JsonResult UpdatePictureForIngredient( int ingredientID, int ingredientPictureID, IFormFile picture ) {

            if ( ingredientID <= 0 ) {
                return Json( new { errors = "IngredientID is <= 0" } );
            }

            if ( ingredientPictureID <= 0 ) {
                return Json( new { errors = "IngredientPictureID is <= 0" } );
            }

            if ( picture == null ) {
                return Json( new { errors = "Picture is null" } );
            }

            byte[ ] pictureBytes;
            using ( MemoryStream stream = new MemoryStream() ) {
                picture.CopyTo( stream );
                pictureBytes = stream.ToArray();
            }

            IngredientPicture ingredientPicture = _dbService.UpdateElement( new IngredientPicture() {
                IngredientID = ingredientID,
                IngredientPictureID = ingredientPictureID,
                Picture = pictureBytes
            } );

            if ( ingredientPicture == null ) {
                return Json( null );
            }

            return Json( new {
                ingredientPicture.IngredientID,
                ingredientPicture.IngredientPictureID,
                Picture = Convert.ToBase64String( ingredientPicture.Picture )
            } );
        }

        [HttpDelete]
        [Route( "Ingredients/{ingredientID}/Pictures/{ingredientPictureID}" )]
        public IActionResult DeletePictureForIngredient( int ingredientID, int ingredientPictureID ) {
            List<string> errors = new List<string>();

            if ( ingredientID <= 0 ) {
                return Json( new { errors = "IngredientID is <= 0" } );
            }

            if ( ingredientPictureID <= 0 ) {
                return Json( new { errors = "IngredientPictureID is <= 0" } );
            }

            if ( _dbService.DeleteElement( new IngredientPicture() { IngredientID = ingredientID, IngredientPictureID = ingredientPictureID } ) ) {
                return Ok();
            }

            return StatusCode( 500, "Error while deleting picture for ingredient" );
        }
    }
}