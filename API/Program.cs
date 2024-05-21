using Core.Interfaces;
using Infrastructure.Services.Managers;
using Infrastructure.Services;
using System.Text.Json.Serialization;
using API.Utility;
using Infrastructure.SharedKernel.Validators;
using Infrastructure.SharedKernel.Mappers;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        services.AddSingleton<MemoryStorageService>();
        services.AddSingleton<BookingValidator>();
        services.AddSingleton<BookingMappers>();
        services.AddTransient<IHotelService, HotelService>();
        services.AddTransient<IFlightService, FlightService>();
        services.AddTransient<IManagerFactory, ManagerFactory>();
        services.AddTransient<HotelManager>();
        services.AddTransient<HotelFlightManager>();
        services.AddTransient<LastMinuteHotelManager>();


        services.AddMemoryCache();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/error");
            app.UseHsts();
        }

        // Other middleware configurations...

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}