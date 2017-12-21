using Microsoft.Extensions.DependencyInjection;
using ThirtyThreeWhales.SmallCafe.Data;
using ThirtyThreeWhales.SmallCafe.Models;
using ThirtyThreeWhales.SmallCafe.Services.Interfaces;

namespace ThirtyThreeWhales.SmallCafe {
    public static class InjectedDependencies {
        public static void AddDependencies( this IServiceCollection services ) {
            services.AddScoped<IDbService<Ingredient>, IngredientsDbService>();
        }
    }
}
