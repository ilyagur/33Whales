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
    public class RecipePicturesController : Controller {
        private IDependentEntityDbService<RecipePicture> _dbService;
        public RecipePicturesController( IDependentEntityDbService<RecipePicture> dbService ) {
            _dbService = dbService;
        }

        [HttpGet]
        [Route( "Recipes/{recipeID}/Pictures" )]
        public JsonResult ReadPicturesForRecipe( int recipeID ) {

            if ( recipeID <= 0 ) {
                return Json( new { errors = "Recipe Id is <= 0" } );
            }

            IList<RecipePicture> recipePictures = _dbService.ReadAllElementsByParentElementId( recipeID );

            if ( recipePictures == null ) {
                return Json( null );
            }

            return Json( recipePictures.Select( r => new {
                r.RecipePictureID,
                r.RecipeID,
                Picture = Convert.ToBase64String( r.Picture )
            } ).ToList() );
        }

        [HttpPost]
        [Route( "Recipes/{recipeID}/Pictures" )]
        public JsonResult AddPictureForRecipe( int recipeID, IFormFile picture ) {
            if ( recipeID <= 0 ) {
                return Json( new { errors = "RecipeId is <= 0" } );
            }

            if ( picture == null ) {
                return Json( new { errors = "Picture is null" } );
            }

            byte[ ] pictureBytes;
            using ( MemoryStream stream = new MemoryStream() ) {
                picture.CopyTo( stream );
                pictureBytes = stream.ToArray();
            }

            RecipePicture recipePicture = _dbService.CreateElement( new RecipePicture() { RecipeID = recipeID, Picture = pictureBytes } );

            if ( recipePicture == null ) {
                return Json( null );
            }

            return Json( new {
                recipePicture.RecipePictureID,
                recipePicture.RecipeID,
                Picture = Convert.ToBase64String( recipePicture.Picture )
            } );
        }

        [HttpPut]
        [Route( "Recipes/{recipeID}/Pictures/{recipePictureID}" )]
        public JsonResult UpdatePictureForRecipe( int recipeID, int recipePictureID, IFormFile picture ) {

            if ( recipeID <= 0 ) {
                return Json( new { errors = "RecipeId is <= 0" } );
            }

            if ( recipePictureID <= 0 ) {
                return Json( new { errors = "Recipe Picture Id is <= 0" } );
            }

            if ( picture == null ) {
                return Json( new { errors = "Picture is null" } );
            }

            byte[ ] pictureBytes;
            using ( MemoryStream stream = new MemoryStream() ) {
                picture.CopyTo( stream );
                pictureBytes = stream.ToArray();
            }

            RecipePicture recipePicture = _dbService.UpdateElement( new RecipePicture() {
                RecipeID = recipeID,
                RecipePictureID = recipePictureID,
                Picture = pictureBytes
            } );

            if ( recipePicture == null ) {
                return Json( null );
            }

            return Json( new {
                recipePicture.RecipePictureID,
                recipePicture.RecipeID,
                Picture = Convert.ToBase64String( recipePicture.Picture )
            } );
        }

        [HttpDelete]
        [Route( "Recipes/{recipeID}/Pictures/{recipePictureID}" )]
        public IActionResult DeletePictureForRecipe( int recipePictureID ) {

            if ( recipePictureID <= 0 ) {
                return Json( new { errors = "RecipePicture ID is <= 0" } );
            }

            if ( _dbService.DeleteElement( new RecipePicture() { RecipePictureID = recipePictureID } ) ) {
                return Ok();
            }

            return StatusCode( 500, "Error while deleting picture for recipe" );
        }
    }
}