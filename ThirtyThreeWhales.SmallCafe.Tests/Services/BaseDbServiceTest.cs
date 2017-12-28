using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Moq;
using System;
using ThirtyThreeWhales.SmallCafe.Data;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services;
using Xunit;


namespace ThirtyThreeWhales.SmallCafe.Tests.Services {
    public class BaseDbServiceTest {
        [Fact]
        public void CreateElement_Valid() {

            var dbContext = new Mock<CafeDbContext>();
            var logger = new Mock<ILogger<BaseDbService<Recipe>>>();

            BaseDbService<Recipe> recipe = new BaseDbService<Recipe>( dbContext.Object, logger.Object );

            var result = recipe.CreateElement( new Recipe() );

            dbContext.Verify( c => c.Add( It.IsAny<Recipe>() ), Times.Once );
            dbContext.Verify( c => c.SaveChanges(), Times.Once );
            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Never );
            Assert.NotNull( result );
        }

        [Fact]
        public void CreateElement_Add_Throw_Exception() {

            var dbContext = new Mock<CafeDbContext>();
            dbContext.Setup( d => d.Add( It.IsAny<Recipe>() ) ).Throws(new Exception());

            var logger = new Mock<ILogger<BaseDbService<Recipe>>>();

            BaseDbService<Recipe> recipe = new BaseDbService<Recipe>( dbContext.Object, logger.Object );

            var result = recipe.CreateElement( new Recipe() );

            dbContext.Verify( a => a.Add( It.IsAny<Recipe>() ), Times.Once );
            dbContext.Verify( a => a.SaveChanges(), Times.Never );
            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Once );
            Assert.Null( result );
        }

        [Fact]
        public void CreateElement_SaveChanges_Throw_Exception() {

            var dbContext = new Mock<CafeDbContext>();
            dbContext.Setup( d => d.SaveChanges() ).Throws( new Exception() );

            var logger = new Mock<ILogger<BaseDbService<Recipe>>>();

            BaseDbService<Recipe> recipe = new BaseDbService<Recipe>( dbContext.Object, logger.Object );

            var result = recipe.CreateElement( new Recipe() );

            dbContext.Verify( a => a.Add( It.IsAny<Recipe>() ), Times.Once );
            dbContext.Verify( a => a.SaveChanges(), Times.Once );
            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Once );
            Assert.Null( result );
        }

        [Fact]
        public void UpdateElement_Valid() {

            var dbContext = new Mock<CafeDbContext>();
            var logger = new Mock<ILogger<BaseDbService<Recipe>>>();

            BaseDbService<Recipe> recipe = new BaseDbService<Recipe>( dbContext.Object, logger.Object );

            var result = recipe.UpdateElement( new Recipe() );

            dbContext.Verify( c => c.Update( It.IsAny<Recipe>() ), Times.Once );
            dbContext.Verify( c => c.SaveChanges(), Times.Once );
            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Never );
            Assert.NotNull( result );
        }

        [Fact]
        public void UpdateElement_Add_Throw_Exception() {

            var dbContext = new Mock<CafeDbContext>();
            dbContext.Setup( d => d.Update( It.IsAny<Recipe>() ) ).Throws( new Exception() );

            var logger = new Mock<ILogger<BaseDbService<Recipe>>>();

            BaseDbService<Recipe> recipe = new BaseDbService<Recipe>( dbContext.Object, logger.Object );

            var result = recipe.UpdateElement( new Recipe() );

            dbContext.Verify( a => a.Update( It.IsAny<Recipe>() ), Times.Once );
            dbContext.Verify( a => a.SaveChanges(), Times.Never );
            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Once );
            Assert.Null( result );
        }

        [Fact]
        public void UpdateElement_SaveChanges_Throw_Exception() {

            var dbContext = new Mock<CafeDbContext>();
            dbContext.Setup( d => d.SaveChanges() ).Throws( new Exception() );

            var logger = new Mock<ILogger<BaseDbService<Recipe>>>();

            BaseDbService<Recipe> recipe = new BaseDbService<Recipe>( dbContext.Object, logger.Object );

            var result = recipe.UpdateElement( new Recipe() );

            dbContext.Verify( a => a.Update( It.IsAny<Recipe>() ), Times.Once );
            dbContext.Verify( a => a.SaveChanges(), Times.Once );
            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Once );
            Assert.Null( result );
        }

        [Fact]
        public void DeleteElement_Valid() {

            var dbContext = new Mock<CafeDbContext>();
            var logger = new Mock<ILogger<BaseDbService<Recipe>>>();

            BaseDbService<Recipe> recipe = new BaseDbService<Recipe>( dbContext.Object, logger.Object );

            var result = recipe.DeleteElement( new Recipe() );

            dbContext.Verify( c => c.Remove( It.IsAny<Recipe>() ), Times.Once );
            dbContext.Verify( c => c.SaveChanges(), Times.Once );
            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Never );
            Assert.True( result );
        }

        [Fact]
        public void DeleteElement_Add_Throw_Exception() {

            var dbContext = new Mock<CafeDbContext>();
            dbContext.Setup( d => d.Remove( It.IsAny<Recipe>() ) ).Throws( new Exception() );

            var logger = new Mock<ILogger<BaseDbService<Recipe>>>();

            BaseDbService<Recipe> recipe = new BaseDbService<Recipe>( dbContext.Object, logger.Object );

            var result = recipe.DeleteElement( new Recipe() );

            dbContext.Verify( a => a.Remove( It.IsAny<Recipe>() ), Times.Once );
            dbContext.Verify( a => a.SaveChanges(), Times.Never );
            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Once );
            Assert.False( result );
        }

        [Fact]
        public void DeleteElement_SaveChanges_Throw_Exception() {

            var dbContext = new Mock<CafeDbContext>();
            dbContext.Setup( d => d.SaveChanges() ).Throws( new Exception() );

            var logger = new Mock<ILogger<BaseDbService<Recipe>>>();

            BaseDbService<Recipe> recipe = new BaseDbService<Recipe>( dbContext.Object, logger.Object );

            var result = recipe.DeleteElement( new Recipe() );

            dbContext.Verify( a => a.Remove( It.IsAny<Recipe>() ), Times.Once );
            dbContext.Verify( a => a.SaveChanges(), Times.Once );
            logger.Verify( l => l.Log( It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>() ), Times.Once );
            Assert.False( result );
        }
    }
}
