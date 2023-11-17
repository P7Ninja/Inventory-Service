using InventoryService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var serverVersion = new MySqlServerVersion(new Version(8, 0, 35));
builder.Services.AddDbContextPool<InventoryServiceContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("InventoryServiceDb"), serverVersion);
});

// allows browsers to access the api. Can be deleted later when api gateway is set up
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "All",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(settings =>
    {
        var gatewayUrl = Environment.GetEnvironmentVariable("GATEWAY_URL");
        if (gatewayUrl != null)
        {
            settings.PreSerializeFilters.Add((swagger, httpReq) =>
            {
                swagger.Servers.Clear();
                var serverUrl = $"{httpReq.Scheme}://{gatewayUrl}";
                swagger.Servers.Add(new OpenApiServer { Url = serverUrl });
            });
        }
    });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// allows browsers to access the api. Can be deleted later when api gateway is set up
app.UseCors("All");

app.UseAuthorization();

app.MapControllers();

app.Run();
