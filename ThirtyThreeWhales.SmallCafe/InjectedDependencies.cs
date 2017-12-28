using Microsoft.Extensions.DependencyInjection;
using ThirtyThreeWhales.SmallCafe.Data;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe {
    public static class InjectedDependencies {
        public static void AddDependencies( this IServiceCollection services ) {
            services.AddScoped<IIndependentEntityDbService<Ingredient>, IngredientsDbService>();
            services.AddScoped<IIndependentEntityDbService<Recipe>, RecipesDbService>();
            services.AddScoped<IDependentEntityDbService<IngredientPicture>, IngredientPicturesDbService>();
            services.AddScoped<IDependentEntityDbService<RecipePicture>, RecipePictureDbService>();
            services.AddScoped<IDependentEntityDbService<CompositionOfRecipes>, CompositionOfRecipesDbService>();
            services.AddScoped<IDbContext, CafeDbContext>();
        }
    }
}
