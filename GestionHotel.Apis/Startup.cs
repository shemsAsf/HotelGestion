using DBContext;
using GestionHotel.Apis.Endpoints.Booking;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Services;
using GestionHotel.Api.Data;
using System.Globalization;
using System.Runtime.InteropServices;


public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDbContext<IAuthenticationDbContext, AuthenticationDbContext>(options =>
            options.UseSqlServer("Server=localhost,3306;Database=projetbookingdb;uid=root;Pwd=;"));
        
        services.AddScoped<IUserService, UserService>(); 
        services.AddScoped<SampleInjectionInterface, SampleInjectionImplementation>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();

        app.Use(async (context, next) =>
        {
            await next.Invoke();
        });

        app.UseAuthenticationMiddleware();
        app.UseRouting();
        app.UseEndpoints(endpoint =>
        {
            endpoint.MapBookingsEndpoints();
        });
    }
}