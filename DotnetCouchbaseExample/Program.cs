using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using DotnetCouchbaseExample.Filters;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>(optional: true, reloadOnChange: true);

var couchbaseConfig = builder.Configuration.GetSection("Couchbase");
var connectionString = couchbaseConfig["ConnectionString"];
var username = couchbaseConfig["Username"];
var password = couchbaseConfig["Password"];
var buckets = couchbaseConfig.GetSection("Buckets").Get<string[]>();

if (buckets == null || buckets.Length == 0)
{
    throw new InvalidOperationException("No buckets configured.");
}


builder.Services.AddCouchbase(options =>
{
    options.ConnectionString = connectionString;
    options.UserName = username;
    options.Password = password;
    options.Buckets = buckets;
});


builder.Services.AddSingleton<ICouchbaseService, CouchbaseService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.SchemaFilter<CustomSchemaFilter>(); 
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
