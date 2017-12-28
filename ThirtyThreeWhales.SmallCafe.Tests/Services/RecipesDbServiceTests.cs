using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using ThirtyThreeWhales.SmallCafe.Data;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services;
using Xunit;

namespace ThirtyThreeWhales.SmallCafe.Tests.Services {
    public class RecipesDbServiceTests {
        [Fact]
        public void ReadElementById_Invalid_Input() {
            var dbContext = new Mock<IDbContext>();
            var logger = new Mock<ILogger<RecipesDbService>>();

            var rcpDbService = new RecipesDbService( dbContext.Object, logger.Object );

            Assert.Null( rcpDbService.ReadElementById( 0 ) );
            Assert.Null( rcpDbService.ReadElementById( -1 ) );
        }

        [Fact]
        public void ReadElementById_Valid_Input_No_Such_Table() {

            var dbContext = new Mock<IDbContext>();
            var logger = new Mock<ILogger<RecipesDbService>>();

            var rcpDbService = new RecipesDbService( dbContext.Object, logger.Object );

            var result = rcpDbService.ReadElementById( 1 );
            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Once );
            Assert.Null( result );
        }

        [Fact]
        public void ReadElementById_Valid_Input_No_Data_In_Table() {

            var dbContext = new Mock<IDbContext>();
            dbContext.Setup( d => d.Recipes ).Returns<RecipesDbService>( null );
            var logger = new Mock<ILogger<RecipesDbService>>();

            var rcpDbService = new RecipesDbService( dbContext.Object, logger.Object );

            var result = rcpDbService.ReadElementById( 1 );
            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Once );
            Assert.Null( result );
        }

        [Fact]
        public void ReadElementById_Valid_Data_In_Table() {
            var recipe = Utils.CreateMockDbSet( new List<Recipe>
                {
                    new Recipe(){
                        RecipeID = 1,
                        FinishedProduct = 10
                    }
                } );


            var dbContext = new Mock<IDbContext>();
            dbContext.Setup( d => d.Recipes ).Returns( recipe.Object );
            var logger = new Mock<ILogger<RecipesDbService>>();
            var rcpDbService = new RecipesDbService( dbContext.Object, logger.Object );

            var result = rcpDbService.ReadElementById( 1 );

            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Never );

            Assert.True( result.RecipeID == 1 );
            Assert.True( result.FinishedProduct == 10 );
        }

        [Fact]
        public void ReadAllElements_No_Data_In_Table() {
            var dbContext = new Mock<IDbContext>();
            dbContext.Setup( d => d.RecipePictures ).Returns<RecipesDbService>( null );
            var logger = new Mock<ILogger<RecipesDbService>>();

            var rcpDbService = new RecipesDbService( dbContext.Object, logger.Object );

            var result = rcpDbService.ReadAllElements();
            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Once );
            Assert.Null( result );
        }

        [Fact]
        public void ReadAllElements_Valid_Data_In_Table() {
            var recipes = Utils.CreateMockDbSet( new List<Recipe>
                {
                    new Recipe(){
                        RecipeID = 1,
                        FinishedProduct = 10
                    },
                    new Recipe(){
                        RecipeID = 2,
                        FinishedProduct = 20
                    }
                } );


            var dbContext = new Mock<IDbContext>();
            dbContext.Setup( d => d.Recipes ).Returns( recipes.Object );
            var logger = new Mock<ILogger<RecipesDbService>>();
            var rcpDbService = new RecipesDbService( dbContext.Object, logger.Object );

            var results = rcpDbService.ReadAllElements();

            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Never );

            Assert.True( results.Count == 2 );
            Assert.True( results.FirstOrDefault().RecipeID == 1 );
            Assert.True( results.FirstOrDefault().FinishedProduct == 10 );
            Assert.True( results.LastOrDefault().RecipeID == 2 );
            Assert.True( results.LastOrDefault().FinishedProduct == 20 );
        }
    }
}
