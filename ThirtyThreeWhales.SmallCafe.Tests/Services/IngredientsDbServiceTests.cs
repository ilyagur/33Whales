using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Moq;
using System;
using System.Collections.Generic;
using ThirtyThreeWhales.SmallCafe.Data;
using ThirtyThreeWhales.SmallCafe.Models;
using System.Linq;
using Xunit;

namespace ThirtyThreeWhales.SmallCafe.Tests.Services {
    public class IngredientsDbServiceTests {

        [Fact]
        public void ReadElementById_Invalid_Input() {
            var dbContext = new Mock<IDbContext>();
            var logger = new Mock<ILogger<IngredientsDbService>>();

            var ingrDbService = new IngredientsDbService( dbContext.Object, logger.Object );

            Assert.Null( ingrDbService.ReadElementById( 0 ) );
            Assert.Null( ingrDbService.ReadElementById( -1 ) );
        }

        [Fact]
        public void ReadElementById_Valid_Input_No_Such_Table() {

            var dbContext = new Mock<IDbContext>();
            var logger = new Mock<ILogger<IngredientsDbService>>();

            var ingrDbService = new IngredientsDbService( dbContext.Object, logger.Object );

            var result = ingrDbService.ReadElementById( 1 );
            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Once );
            Assert.Null( result );
        }

        [Fact]
        public void ReadElementById_Valid_Input_No_Data_In_Table() {

            var dbContext = new Mock<IDbContext>();
            dbContext.Setup( d => d.RecipePictures ).Returns<IngredientsDbService>( null );
            var logger = new Mock<ILogger<IngredientsDbService>>();

            var ingrDbService = new IngredientsDbService( dbContext.Object, logger.Object );

            var result = ingrDbService.ReadElementById( 1 );
            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Once );
            Assert.Null( result );
        }

        [Fact]
        public void ReadElementById_Valid_Data_In_Table() {
            var ingredient = Utils.CreateMockDbSet( new List<Ingredient>
                {
                    new Ingredient(){
                        IngredientID = 1,
                        Name = "Test"
                    }
                } );


            var dbContext = new Mock<IDbContext>();
            dbContext.Setup( d => d.Ingredients ).Returns( ingredient.Object );
            var logger = new Mock<ILogger<IngredientsDbService>>();
            var ingrDbService = new IngredientsDbService( dbContext.Object, logger.Object );

            var result = ingrDbService.ReadElementById( 1 );

            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Never );

            Assert.True( result.IngredientID == 1 );
            Assert.True( result.Name == "Test" );
        }

        [Fact]
        public void ReadAllElements_No_Data_In_Table() {
            var dbContext = new Mock<IDbContext>();
            dbContext.Setup( d => d.RecipePictures ).Returns<IngredientsDbService>( null );
            var logger = new Mock<ILogger<IngredientsDbService>>();

            var ingrDbService = new IngredientsDbService( dbContext.Object, logger.Object );

            var result = ingrDbService.ReadAllElements();
            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Once );
            Assert.Null( result );
        }

        [Fact]
        public void ReadAllElements_Valid_Data_In_Table() {
            var ingredient = Utils.CreateMockDbSet( new List<Ingredient>
                {
                    new Ingredient(){
                        IngredientID = 1,
                        Name = "Test"
                    },
                    new Ingredient(){
                        IngredientID = 2,
                        Name = "Test2"
                    }
                } );


            var dbContext = new Mock<IDbContext>();
            dbContext.Setup( d => d.Ingredients ).Returns( ingredient.Object );
            var logger = new Mock<ILogger<IngredientsDbService>>();
            var ingrDbService = new IngredientsDbService( dbContext.Object, logger.Object );

            var results = ingrDbService.ReadAllElements();

            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Never );

            Assert.True( results.Count == 2 );
            Assert.True( results.FirstOrDefault().IngredientID == 1 );
            Assert.True( results.FirstOrDefault().Name == "Test" );
            Assert.True( results.LastOrDefault().IngredientID == 2 );
            Assert.True( results.LastOrDefault().Name == "Test2" );
        }
    }
}
