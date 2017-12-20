using Microsoft.EntityFrameworkCore;
using ThirtyThreeWhales.SmallCafe.Models;
using System.Data.Entity.ModelConfiguration;

namespace ThirtyThreeWhales.SmallCafe.Data {
    public class CafeDbContext: Microsoft.EntityFrameworkCore.DbContext {
        public CafeDbContext( DbContextOptions<CafeDbContext> options ) : base( options ) {
        }

        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<IngredientPictures> IngredientPictures { get; set; }

        protected override void OnModelCreating( ModelBuilder modelBuilder ) {
            modelBuilder.Entity<Ingredients>().ToTable( "Ingredients" );
            //modelBuilder.Entity<IngredientPictures>().ToTable( "IngredientPictures" );
        }
    }
}
