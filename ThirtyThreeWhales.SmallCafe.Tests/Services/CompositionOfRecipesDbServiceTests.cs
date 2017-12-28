using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Moq;
using System;
using ThirtyThreeWhales.SmallCafe.Data;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services;
using Xunit;

namespace ThirtyThreeWhales.SmallCafe.Tests.Services {
    public class CompositionOfRecipesDbServiceTests {

        [Fact]
        public void ReadAllElementsByParentElementId_Invalid_Input() {

            var dbContext = new Mock<CafeDbContext>();
            var logger = new Mock<ILogger<CompositionOfRecipesDbService>>();

            var corDbService = new CompositionOfRecipesDbService(dbContext.Object, logger.Object);

            Assert.Null( corDbService.ReadAllElementsByParentElementId( 0 ) );
            Assert.Null( corDbService.ReadAllElementsByParentElementId( -1 ) );
        }

        [Fact]
        public void ReadAllElementsByParentElementId_Valid_Input_No_Such_Table() {

            var dbContext = new Mock<CafeDbContext>();
            var logger = new Mock<ILogger<CompositionOfRecipesDbService>>();

            var corDbService = new CompositionOfRecipesDbService( dbContext.Object, logger.Object );

            var result = corDbService.ReadAllElementsByParentElementId( 1 );
            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Once );
            Assert.Null( result );
        }

        [Fact]
        public void ReadAllElementsByParentElementId_Valid_Input_No_Data_In_Table() {

            var dbContext = new Mock<CafeDbContext>();
            dbContext.Setup( d => d.CompositionOfRecipes ).Returns<CompositionOfRecipes>(null);
            var logger = new Mock<ILogger<CompositionOfRecipesDbService>>();

            var corDbService = new CompositionOfRecipesDbService( dbContext.Object, logger.Object );

            var result = corDbService.ReadAllElementsByParentElementId( 1 );
            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Once );
            Assert.Null( result );
        }

        [Fact]
        public void ReadAllElementsByParentElementId_Valid_Input_Not_Full_Data_In_Table() {

            var dbContext = new Mock<CafeDbContext>();
            dbContext.Setup( d => d.CompositionOfRecipes ).Returns<CompositionOfRecipes>( new CompositionOfRecipes() );
            var logger = new Mock<ILogger<CompositionOfRecipesDbService>>();

            var corDbService = new CompositionOfRecipesDbService( dbContext.Object, logger.Object );

            var result = corDbService.ReadAllElementsByParentElementId( 1 );
            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Once );
            Assert.Null( result );
        }
    }
}
