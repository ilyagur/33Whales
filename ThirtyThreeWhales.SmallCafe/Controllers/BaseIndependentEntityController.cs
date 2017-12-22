using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe.Controllers
{
    [Produces("application/json")]
    [Route("api/BaseIndependentEntity")]
    public class BaseIndependentEntityController<T> : Controller where T: new()
    {
        private IIndependentEntityDbService<T> _dbService;
        private ILogger _logger;
        protected BaseIndependentEntityController( IIndependentEntityDbService<T> dbService, ILogger logger ) {
            _dbService = dbService;
            _logger = logger;
        }

        protected JsonResult GetAll() {
            List<string> errors = new List<string>();
            IList<T> entity = new List<T>();

            try {
                entity = _dbService.GetAll();
            } catch ( Exception e ) {
                string error = $"Exception: {e.Message}" + ( e.InnerException != null ? $" Inner Exception: {e.InnerException.Message}" : "" );
                _logger.LogWarning( error );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            return Json( entity );
        }

        protected JsonResult GetEntityById( int entityID ) {
            List<string> errors = new List<string>();

            if ( entityID <= 0 ) {
                errors.Add( "EntityID is <= 0" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            T entity = new T();
            try {
                entity = _dbService.GetElementById( entityID );
            } catch ( Exception e ) {
                string error = $"Exception: {e.Message}" + ( e.InnerException != null ? $" Inner Exception: {e.InnerException.Message}" : "" );
                _logger.LogWarning( error );

                errors.Add( error );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            return Json( entity );
        }

        protected JsonResult AddNewEntity( T entity ) {
            List<string> errors = new List<string>();

            if ( entity == null ) {
                errors.Add( "Entity is NULL" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            try {
                entity = _dbService.CreateNewElement( entity );
            } catch ( Exception e ) {
                string error = $"Exception: {e.Message}" + ( e.InnerException != null ? $" Inner Exception: {e.InnerException.Message}" : "" );
                _logger.LogWarning( error );

                errors.Add( error );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            return Json( entity );
        }

        protected JsonResult UpdateEntity( T entity ) {
            List<string> errors = new List<string>();

            if ( entity == null ) {
                errors.Add( "Entity is NULL" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            try {
                entity = _dbService.UpdateElement( entity );
            } catch ( Exception e ) {
                string error = $"Exception: {e.Message}" + ( e.InnerException != null ? $" Inner Exception: {e.InnerException.Message}" : "" );
                _logger.LogWarning( error );

                errors.Add( error );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            return Json( entity );
        }

        protected IActionResult DeleteEntity( T entity ) {
            List<string> errors = new List<string>();

            if ( entity == null ) {
                errors.Add( "Entity is NULL" );
            }

            if ( errors.Count > 0 ) {
                return Json( new { errors = errors } );
            }

            try {
                _dbService.DeleteElement( entity );
            } catch ( Exception e ) {
                string error = $"Exception: {e.Message}" + ( e.InnerException != null ? $" Inner Exception: {e.InnerException.Message}" : "" );
                _logger.LogWarning( error );

                return StatusCode( 500, error );
            }
            return Ok();
        }
    }
}