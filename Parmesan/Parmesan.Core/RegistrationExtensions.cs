using Curds.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Parmesan
{
    using Abstraction;
    using Implementation;
    using Domain;
    using Persistence.Abstraction;
    using Persistence.Implementation;
    using Security.Abstraction;
    using Security.Implementation;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddParmesanCore(this IServiceCollection services) => services
            .AddPersistence()
            .AddTransient<IScopeResolver, ScopeResolver>()
            .AddTransient<IClaimsPrincipalFactory, ClaimsPrinicpalFactory>()
            .AddTransient<IAuthenticationVerifier, AuthenticationVerifier>()
            .AddTransient<IPasswordHasher, Pbkdf2PasswordHasher>();

        private static IServiceCollection AddPersistence(this IServiceCollection services) => services
            .AddCurdsPersistence()
            .ConfigureParmesanDataModel()
            .AddTransient<IParmesanDatabase, SqlParmesanDatabase>()
            .AddTransient<IClientRepository, SqlClientRepository>()
            .AddTransient<IUserRepository, SqlUserRepository>();

        private static IServiceCollection ConfigureParmesanDataModel(this IServiceCollection services) => services
            .ConfigureDefaultSchema(nameof(Parmesan))
            .ConfigureEntity<PasswordAuthentication>()
                .HasKey(password => password.UserID)
                .RegisterEntity();
    }
}
