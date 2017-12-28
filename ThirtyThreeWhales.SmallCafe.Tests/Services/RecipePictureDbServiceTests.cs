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
    public class RecipePictureDbServiceTests {
        [Fact]
        public void ReadAllElementsByParentElementId_Invalid_Input() {

            var dbContext = new Mock<IDbContext>();
            var logger = new Mock<ILogger<RecipePictureDbService>>();

            var corDbService = new RecipePictureDbService( dbContext.Object, logger.Object );

            Assert.Null( corDbService.ReadAllElementsByParentElementId( 0 ) );
            Assert.Null( corDbService.ReadAllElementsByParentElementId( -1 ) );
        }

        [Fact]
        public void ReadAllElementsByParentElementId_Valid_Input_No_Such_Table() {

            var dbContext = new Mock<IDbContext>();
            var logger = new Mock<ILogger<RecipePictureDbService>>();

            var corDbService = new RecipePictureDbService( dbContext.Object, logger.Object );

            var result = corDbService.ReadAllElementsByParentElementId( 1 );
            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Once );
            Assert.Null( result );
        }

        [Fact]
        public void ReadAllElementsByParentElementId_Valid_Input_No_Data_In_Table() {

            var dbContext = new Mock<IDbContext>();
            dbContext.Setup( d => d.RecipePictures ).Returns<RecipePictureDbService>( null );
            var logger = new Mock<ILogger<RecipePictureDbService>>();

            var corDbService = new RecipePictureDbService( dbContext.Object, logger.Object );

            var result = corDbService.ReadAllElementsByParentElementId( 1 );
            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Once );
            Assert.Null( result );
        }

        [Fact]
        public void ReadAllElementsByParentElementId_Valid_Data_In_Table() {
            var picMock = Utils.CreateMockDbSet( new List<RecipePicture>
                {
                    new RecipePicture(){
                        RecipeID = 1,
                        Recipe = new Recipe(),
                        RecipePictureID = 1
                    }
                } );


            var dbContext = new Mock<IDbContext>();
            dbContext.Setup( d => d.RecipePictures ).Returns( picMock.Object );
            var logger = new Mock<ILogger<RecipePictureDbService>>();
            var picDbService = new RecipePictureDbService( dbContext.Object, logger.Object );

            var results = picDbService.ReadAllElementsByParentElementId( 1 );

            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Never );
            Assert.True( results.Count == 1 );

            var result = results.FirstOrDefault();

            Assert.True( result.RecipeID == 1 );
            Assert.True( result.RecipePictureID == 1 );
            Assert.NotNull( result.Recipe );
        }
    }
}
