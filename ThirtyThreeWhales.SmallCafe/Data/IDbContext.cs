using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ThirtyThreeWhales.SmallCafe.Models;

namespace ThirtyThreeWhales.SmallCafe.Data {
    public interface IDbContext
    {
        DbSet<Ingredient> Ingredients { get; set; }
        DbSet<IngredientPicture> IngredientPictures { get; set; }
        DbSet<Recipe> Recipes { get; set; }
        DbSet<RecipePicture> RecipePictures { get; set; }
        DbSet<CompositionOfRecipes> CompositionOfRecipes { get; set; }
        EntityEntry Add( object entity );
        EntityEntry Update( object entity );
        EntityEntry Remove( object entity );
        int SaveChanges();
    }
}
