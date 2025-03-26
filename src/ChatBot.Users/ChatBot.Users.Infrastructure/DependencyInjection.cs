using ChatBot.Users.Application;
using ChatBot.Users.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatBot.Users.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder, string db)
    {
        builder.AddSqlServerDbContext<UsersDbContext>(db, configureDbContextOptions: options =>
        {
            options.UseSqlServer(sqlServerOptions =>
            {
                sqlServerOptions.MigrationsAssembly(typeof(DependencyInjection).Assembly.GetName().Name);
            });
        });

        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
            options.SignIn.RequireConfirmedAccount = true;
        }).AddEntityFrameworkStores<UsersDbContext>().AddDefaultTokenProviders();

        builder.Services.AddScoped<IUserManager, UserManager>();
        builder.Services.AddScoped<ISignInManager, SignInManager>();
        
        return builder;
    }
}