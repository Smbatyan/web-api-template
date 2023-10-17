using System.Reflection;
using FluentValidation;

namespace WebApi.Extensions;

public static class MediatRExtension
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        Assembly assembly = Assembly.GetAssembly(typeof(Application.AssemblyReference));
        services.AddMediatR(x => x.RegisterServicesFromAssembly(assembly!));
        
        services.AddValidatorsFromAssembly(assembly);
        
        return services;
    }
}