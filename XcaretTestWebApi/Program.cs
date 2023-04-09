using Microsoft.OpenApi.Models;
using XcaretTestWebApi.Servicios;
using NLog.Extensions.Logging;
using NLog.Web;
using NLog;
using Microsoft.Extensions.Options;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<IServicio_API, Servicio_API>();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        //app.UseSwagger();
        //app.UseSwaggerUI();

        app.UseSwagger(options =>
        {
            //options.RouteTemplate = "swagger/v1/swagger.json";
            //options.SerializeAsV2 = true;
        });
        app.UseSwaggerUI(options =>
        {
            //options.RoutePrefix = string.Empty;
            //options.SwaggerEndpoint("/swagger/v1/swagger.json", "XcaretTestWebApi");
            //options.RoutePrefix = "swagger/v1";

        });
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    logger.Error(ex);
    throw(ex);
}
finally
{
    NLog.LogManager.Shutdown(); 
}
