using Microsoft.EntityFrameworkCore;
using ThirtyThreeWhales.SmallCafe.Models;
using System.Data.Entity.ModelConfiguration;

namespace ThirtyThreeWhales.SmallCafe.Data {
    public class CafeDbContext: Microsoft.EntityFrameworkCore.DbContext {
        public CafeDbContext( DbContextOptions<CafeDbContext> options ) : base( options ) {
        }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientPicture> IngredientPictures { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipePicture> RecipePictures { get; set; }

        protected override void OnModelCreating( ModelBuilder modelBuilder ) {
            modelBuilder.Entity<Ingredient>().ToTable( "Ingredients" );
            modelBuilder.Entity<IngredientPicture>().ToTable( "IngredientPictures" );

            modelBuilder.Entity<Recipe>().ToTable( "Recipes" );
            modelBuilder.Entity<RecipePicture>().ToTable( "RecipePictures" );

        }
    }
}
