using eSya.FixedAsset.WebAPI.Utility;
using eSya.FixedAsset.WebAPI.Filters;
using Microsoft.Extensions.Configuration;
using DL_FixedAsset = eSya.FixedAsset.DL.Entities;
using eSya.FixedAsset.IF;
using eSya.FixedAsset.DL.Repository;
using Microsoft.Extensions.Localization;
using eSya.FixedAsset.DL.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

DL_FixedAsset.eSyaEnterprise._connString = builder.Configuration.GetConnectionString("dbConn_eSyaEnterprise");

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ApikeyAuthAttribute>();
});

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<HttpAuthAttribute>();
});

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<CultureAuthAttribute>();
});
//Localization

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
                   //new CultureInfo(name:"en-IN"),
                    new CultureInfo(name:"en-US"),
                    new CultureInfo(name:"ar-EG"),
                };
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(culture: supportedCultures[0], uiCulture: supportedCultures[0]);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

});

builder.Services.AddLocalization();
//localization


//for cross origin support
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
});


builder.Services.AddScoped<ICommonDataRepository, CommonDataRepository>();
builder.Services.AddSingleton<IAssetGroupRepository, AssetGroupRepository>();
builder.Services.AddSingleton<IAssetDepreciationRepository, AssetDepreciationRepository>();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthorization();

//Localization

var supportedCultures = new[] { /*"en-IN", */ "en-US", "ar-EG" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);
//Localization



app.MapControllers();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=values}/{action=Get}/{id?}");

app.Run();
