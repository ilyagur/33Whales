using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IngredientPicturesController( IDependentEntityDbService<IngredientPicture> dbService ) {
            _dbService = dbService;
        }

        [HttpGet]
        [Route( "Ingredients/{ingredientID}/Pictures" )]
        public JsonResult GetPicturesForIngredient( int ingredientID ) {
            List<string> errors = new List<string>();

            if ( ingredientID <= 0 ) {
                errors.Add( "IngredientID is <= 0" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            IList<IngredientPicture> ingredientPicture = new List<IngredientPicture>();

            try {
                ingredientPicture = _dbService.GetAllElementsByParentElementId( ingredientID );
            } catch ( Exception e ) {
                //TODO: logger
                errors.Add( $"Exception: {e.Message}" );

                if ( e.InnerException != null ) {
                    errors.Add( $"Inner Exception: {e.InnerException.Message}" );
                }
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            if ( ingredientPicture == null ) {
                return Json( null );
            }

            return Json( ingredientPicture.Select( r => new {
                IngredientPictureID = r.IngredientPictureID,
                IngredientID = r.IngredientID,
                Picture = Convert.ToBase64String( r.Picture )
            } ).ToList() );
        }

        [HttpPost]
        [Route( "Ingredients/{ingredientID}/Pictures" )]
        public JsonResult AddPictureForIngredient( int ingredientID, IFormFile picture ) {
            List<string> errors = new List<string>();

            if ( ingredientID <= 0 ) {
                errors.Add( "IngredientID is <= 0" );
            }

            if ( picture == null ) {
                errors.Add( "Picture is null" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            byte[ ] pictureBytes;
            IngredientPicture ingredientPicture = new IngredientPicture();

            using ( MemoryStream stream = new MemoryStream() ) {
                picture.CopyTo( stream );
                pictureBytes = stream.ToArray();
            }

            try {
                ingredientPicture = _dbService.CreateNewDependantElement( new IngredientPicture() { IngredientID = ingredientID, Picture = pictureBytes } );
            } catch ( Exception e ) {
                //TODO: logger
                errors.Add( $"Exception: {e.Message}" );

                if ( e.InnerException != null ) {
                    errors.Add( $"Inner Exception: {e.InnerException.Message}" );
                }
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            return Json( new {
                IngredientID = ingredientPicture.IngredientID,
                IngredientPictureID = ingredientPicture.IngredientPictureID,
                Picture = Convert.ToBase64String( ingredientPicture.Picture )
            } );
        }

        [HttpPut]
        [Route( "Ingredients/{ingredientID}/Pictures/{ingredientPictureID}" )]
        public JsonResult UpdatePictureForIngredient( int ingredientID, int ingredientPictureID, IFormFile picture ) {
            List<string> errors = new List<string>();

            if ( ingredientID <= 0 ) {
                errors.Add( "IngredientID is <= 0" );
            }

            if ( ingredientPictureID <= 0 ) {
                errors.Add( "IngredientPictureID is <= 0" );
            }

            if ( picture == null ) {
                errors.Add( "Picture is null" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            byte[ ] pictureBytes;
            IngredientPicture ingredientPicture = new IngredientPicture();

            using ( MemoryStream stream = new MemoryStream() ) {
                picture.CopyTo( stream );
                pictureBytes = stream.ToArray();
            }

            try {
                ingredientPicture = _dbService.UpdateExistingDependantElement( new IngredientPicture() {
                    IngredientID = ingredientID,
                    IngredientPictureID = ingredientPictureID,
                    Picture = pictureBytes
                } );
            } catch ( Exception e ) {
                //TODO: logger
                errors.Add( $"Exception: {e.Message}" );

                if ( e.InnerException != null ) {
                    errors.Add( $"Inner Exception: {e.InnerException.Message}" );
                }
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            return Json( new {
                IngredientID = ingredientPicture.IngredientID,
                IngredientPictureID = ingredientPicture.IngredientPictureID,
                Picture = Convert.ToBase64String( ingredientPicture.Picture )
            } );
        }

        [HttpDelete]
        [Route( "Ingredients/{ingredientID}/Pictures/{ingredientPictureID}" )]
        public IActionResult DeletePictureForIngredient( int ingredientID, int ingredientPictureID ) {
            List<string> errors = new List<string>();

            if ( ingredientID <= 0 ) {
                errors.Add( "IngredientID is <= 0" );
            }

            if ( ingredientPictureID <= 0 ) {
                errors.Add( "IngredientPictureID is <= 0" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            try {
                _dbService.DeleteDependantElement( new IngredientPicture() { IngredientID = ingredientID, IngredientPictureID = ingredientPictureID } );
            } catch ( Exception e ) {
                //TODO: logger
                return StatusCode( 500, $"Exception: {e.Message}" );
            }
            return Ok();
        }
    }
}