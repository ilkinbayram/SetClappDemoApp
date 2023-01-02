using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Resources.Dictionary;
using Core.Utilities.Configuration.Helpers.Abstract;
using Core.Utilities.Configuration.Helpers.Concrete;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Service.DependencyResolvers.Autofac;
using SetClappDemoApp.MVC.Utilities.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacBusinessModule());
    });

CustomDictionary.Initialise();

string serverName = builder.Configuration.GetConnectionString("SqlServerName");
string databaseName = builder.Configuration.GetConnectionString("SqlDatabaseName");
string encryptedLocalConnectionString = builder.Configuration.GetConnectionString("SqlLocal");
string decryptedConnectionString = string.Format(CustomCryptor.GetCleanData(encryptedLocalConnectionString), serverName, databaseName);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(decryptedConnectionString);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCors(o => o.AddPolicy("AllowOrigin", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

//services.AddRazorPages();
builder.Services.AddScoped<IConfigurationHelper, ConfigHelper>();

builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule()
});


//                           /\
//________________________  //\\
//     UP IS Services    |   ||
//------------------------   ||
//                           --


//                           --
//________________________   ||
//  Down IS MiddleWares  |   ||
//------------------------  \\//
//                           \/



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.ConfigureCustomExceptionMiddleware();

app.UseCors(options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.SeedData().Run();
