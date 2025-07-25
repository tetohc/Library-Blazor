using Library.Components;
using Library.Data;
using Library.Services;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

namespace Library
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddMudServices();

            builder.Services.AddTransient<SeedDB>();
            builder.Services.AddScoped<LoginService>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddDbContext<ContextDb>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnectionString"));
            });

            var app = builder.Build();

            #region Inicializar una base de datos con datos de prueba

            SeedData(app);
            static void SeedData(WebApplication app)
            {
                IServiceScopeFactory? scopeFactory = app.Services.GetService<IServiceScopeFactory>();
                using IServiceScope scope = scopeFactory.CreateScope();
                SeedDB? service = scope.ServiceProvider.GetService<SeedDB>();
                service!.SeedAsync().Wait();
            }

            #endregion Inicializar una base de datos con datos de prueba

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
