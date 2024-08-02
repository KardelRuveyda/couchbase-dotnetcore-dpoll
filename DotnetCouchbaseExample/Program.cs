using Couchbase;
using Couchbase.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Couchbase ile baðlantý için gerekli ayarlarý ekleyin
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
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
    // Burada ilk bucket'ý seçiyoruz veya birden fazla bucket'ý destekleyecek þekilde yapýlandýrabilirsiniz
    options.Buckets = buckets;
});


//builder.Services.AddSingleton()
builder.Services.AddSingleton<UserInfoService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
