﻿using Microsoft.EntityFrameworkCore;
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
    public class CompositionOfRecipesDbServiceTests {

        [Fact]
        public void ReadAllElementsByParentElementId_Invalid_Input() {

            var dbContext = new Mock<IDbContext>();
            var logger = new Mock<ILogger<CompositionOfRecipesDbService>>();

            var corDbService = new CompositionOfRecipesDbService( dbContext.Object, logger.Object );

            Assert.Null( corDbService.ReadAllElementsByParentElementId( 0 ) );
            Assert.Null( corDbService.ReadAllElementsByParentElementId( -1 ) );
        }

        [Fact]
        public void ReadAllElementsByParentElementId_Valid_Input_No_Such_Table() {

            var dbContext = new Mock<IDbContext>();
            var logger = new Mock<ILogger<CompositionOfRecipesDbService>>();

            var corDbService = new CompositionOfRecipesDbService( dbContext.Object, logger.Object );

            var result = corDbService.ReadAllElementsByParentElementId( 1 );
            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Once );
            Assert.Null( result );
        }

        [Fact]
        public void ReadAllElementsByParentElementId_Valid_Input_No_Data_In_Table() {

            var dbContext = new Mock<IDbContext>();
            dbContext.Setup( d => d.CompositionOfRecipes ).Returns<CompositionOfRecipes>( null );
            var logger = new Mock<ILogger<CompositionOfRecipesDbService>>();

            var corDbService = new CompositionOfRecipesDbService( dbContext.Object, logger.Object );

            var result = corDbService.ReadAllElementsByParentElementId( 1 );
            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Once );
            Assert.Null( result );
        }

        [Fact]
        public void ReadAllElementsByParentElementId_Valid_Data_In_Table() {
            var corMock = Utils.CreateMockDbSet( new List<CompositionOfRecipes>
                {
                    new CompositionOfRecipes(){
                        RecipeID = 1,
                        IngredientID = 1,
                        Ingredient = new Ingredient(),
                        Quantity = 1
                    }
                } );


            var dbContext = new Mock<IDbContext>();
            dbContext.Setup( d => d.CompositionOfRecipes ).Returns( corMock.Object );
            var logger = new Mock<ILogger<CompositionOfRecipesDbService>>();
            var corDbService = new CompositionOfRecipesDbService( dbContext.Object, logger.Object );

            var results = corDbService.ReadAllElementsByParentElementId( 1 );

            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Never );
            Assert.True( results.Count == 1 );

            var result = results.FirstOrDefault();

            Assert.True( result.RecipeID == 1 );
            Assert.True( result.IngredientID == 1 );
            Assert.True( result.Quantity == 1 );
            Assert.NotNull( result.Ingredient );
        }
    }
}
