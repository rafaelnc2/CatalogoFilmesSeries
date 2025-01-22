namespace CatalogoFilesSeries.Api.Extensions;

public static class MediatorExtensions
{
    public static void AddMediator(this IServiceCollection services) => 
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ApplicationAssembly>());
}