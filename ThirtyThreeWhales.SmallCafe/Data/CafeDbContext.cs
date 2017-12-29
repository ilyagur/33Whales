using Microsoft.EntityFrameworkCore;
using ThirtyThreeWhales.SmallCafe.Models;

namespace ThirtyThreeWhales.SmallCafe.Data {
    public class CafeDbContext: DbContext, IDbContext {
        public CafeDbContext() { }
        public CafeDbContext( DbContextOptions<CafeDbContext> options ) : base( options ) {
        }

        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<IngredientPicture> IngredientPictures { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<RecipePicture> RecipePictures { get; set; }
        public virtual DbSet<CompositionOfRecipes> CompositionOfRecipes { get; set; }


        protected override void OnModelCreating( ModelBuilder modelBuilder ) {
            modelBuilder.Entity<Ingredient>().ToTable( "Ingredients" );
            modelBuilder.Entity<IngredientPicture>().ToTable( "IngredientPictures" );

            modelBuilder.Entity<Recipe>().ToTable( "Recipes" );
            modelBuilder.Entity<RecipePicture>().ToTable( "RecipePictures" );

            modelBuilder.Entity<CompositionOfRecipes>().ToTable( "CompositionOfRecipes" ).HasKey(k => new { k.IngredientID, k.RecipeID } );
        }
    }
}
