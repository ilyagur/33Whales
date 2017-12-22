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
    [Produces("application/json")]
    [Route("api")]
    public class RecipePicturesController : Controller
    {
        private IDependentEntityDbService<RecipePicture> _dbService;
        private ILogger<RecipePicturesController> _logger;
        public RecipePicturesController(IDependentEntityDbService<RecipePicture> dbService, ILogger<RecipePicturesController> logger ) {
            _dbService = dbService;
            _logger = logger;
        }

        [HttpGet]
        [Route( "Recipes/{recipeID}/Pictures" )]
        public JsonResult GetPicturesForRecipe(int recipeID ) {
            List<string> errors = new List<string>();

            if ( recipeID <= 0 ) {
                errors.Add( "RecipeId is <= 0" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            IList<RecipePicture> recipePictures = new List<RecipePicture>();

            try {
                recipePictures = _dbService.GetAllElementsByParentElementId( recipeID );
            } catch ( Exception e ) {
                string error = $"Exception: {e.Message}" + ( e.InnerException != null ? $" Inner Exception: {e.InnerException.Message}" : "" );
                _logger.LogWarning( error );

                errors.Add( error );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            if ( recipePictures == null ) {
                return Json( null );
            }

            return Json( recipePictures.Select( r => new {
                RecipePictureID = r.RecipePictureID,
                RecipeID = r.RecipeID,
                Picture = Convert.ToBase64String( r.Picture )
            } ).ToList() );
        }

        [HttpPost]
        [Route( "Recipes/{recipeID}/Pictures" )]
        public JsonResult AddPictureForRecipe( int recipeID, IFormFile picture ) {
            List<string> errors = new List<string>();

            if ( recipeID <= 0 ) {
                errors.Add( "RecipeId is <= 0" );
            }

            if ( picture == null ) {
                errors.Add( "Picture is null" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            byte[ ] pictureBytes;
            RecipePicture recipePicture = new RecipePicture();

            using ( MemoryStream stream = new MemoryStream() ) {
                picture.CopyTo( stream );
                pictureBytes = stream.ToArray();
            }

            try {
                recipePicture = _dbService.CreateNewDependantElement( new RecipePicture() { RecipeID = recipeID, Picture = pictureBytes } );
            } catch ( Exception e ) {
                string error = $"Exception: {e.Message}" + ( e.InnerException != null ? $" Inner Exception: {e.InnerException.Message}" : "" );
                _logger.LogWarning( error );

                errors.Add( error );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            return Json( new {
                RecipePictureID = recipePicture.RecipePictureID,
                RecipeID = recipePicture.RecipeID,
                Picture = Convert.ToBase64String( recipePicture.Picture)
            } );
        }

        [HttpPut]
        [Route( "Recipes/{recipeID}/Pictures/{recipePictureID}" )]
        public JsonResult UpdatePictureForRecipe( int recipeID, int recipePictureID, IFormFile picture ) {
            List<string> errors = new List<string>();

            if ( recipeID <= 0 ) {
                errors.Add( "RecipeId is <= 0" );
            }

            if ( recipePictureID <= 0 ) {
                errors.Add( "RecipePictureId is <= 0" );
            }

            if ( picture == null ) {
                errors.Add( "Picture is null" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            byte[ ] pictureBytes;
            RecipePicture recipePicture = new RecipePicture();

            using ( MemoryStream stream = new MemoryStream() ) {
                picture.CopyTo( stream );
                pictureBytes = stream.ToArray();
            }

            try {
                recipePicture = _dbService.UpdateExistingDependantElement( new RecipePicture() {
                    RecipeID = recipeID,
                    RecipePictureID = recipePictureID,
                    Picture = pictureBytes
                } );
            } catch ( Exception e ) {
                string error = $"Exception: {e.Message}" + ( e.InnerException != null ? $" Inner Exception: {e.InnerException.Message}" : "" );
                _logger.LogWarning( error );

                errors.Add( error );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            return Json( new {
                RecipePictureID = recipePicture.RecipePictureID,
                RecipeID = recipePicture.RecipeID,
                Picture = Convert.ToBase64String( recipePicture.Picture )
            } );
        }

        [HttpDelete]
        [Route( "Recipes/{recipeID}/Pictures/{recipePictureID}" )]
        public IActionResult DeletePictureForRecipe( int recipeID, int recipePictureID ) {
            List<string> errors = new List<string>();

            if ( recipeID <= 0 ) {
                errors.Add( "RecipeID is <= 0" );
            }

            if ( recipePictureID <= 0 ) {
                errors.Add( "RecipePictureID is <= 0" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            try {
                _dbService.DeleteDependantElement(new RecipePicture(){ RecipeID = recipeID, RecipePictureID = recipePictureID } );
            } catch ( Exception e ) {
                string error = $"Exception: {e.Message}" + ( e.InnerException != null ? $" Inner Exception: {e.InnerException.Message}" : "" );
                _logger.LogWarning( error );

                return StatusCode( 500, error );
            }
            return Ok();
        }
    }
}