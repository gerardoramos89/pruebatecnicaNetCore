using ECommerce.Api.Helpers;
using ECommerce.Api.Services.Contract;
using ECommerce.Api.Services;
using ECommerce.Api.Domain;
using ECommerce.Api.Infra.Repositories.CacheRepositories.MongoCacheRepositories;
using ECommerce.Api.Infra.Repositories.ReadRepositories.ProductReadRepositories;
using ECommerce.Api.Infra.Repositories.WriteRepositories.ProductWriteRepositories;
using ECommerce.Api.Infra.Repositories;
using ECommerce.Api.Infra.Repositories.ReadRepositories.CustomerReadRepositories;
using ECommerce.Api.Infra.Repositories.ReadRepositories;
using ECommerce.Api.Infra.Repositories.WriteRepositories;
using ECommerce.Api.Infra.Repositories.CacheRepositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container ProductDatabaseSettings.
builder.Services.Configure<ProductDatabaseSettings>(
    builder.Configuration.GetSection("ProductDatabaseSettings"));
// Add services to the container CustomerDatabaseSettings.
builder.Services.Configure<CustomerDatabaseSettings>(
    builder.Configuration.GetSection("CustomerDatabaseSettings"));
// Add services to the container ReservationDatabaseSettings.
builder.Services.Configure<ReservationDatabaseSettings>(
    builder.Configuration.GetSection("ReservationDatabaseSettings"));

#region [ IOC ]

#region [ Application ]

builder.Services.AddScoped<IProductService, ProductService>();

#endregion [Application]

#region [ Cache ]

builder.Services.AddScoped<IMongoCacheRepository, MongoCacheRepository>();
builder.Services.AddScoped<ICustomerCacheRepository, CustomerCacheRepository>();


#endregion [ Cache ]

#region [ Infra - Data ]

builder.Services.AddScoped<IProductReadRepository, ProductReadRepository>();
builder.Services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
builder.Services.AddScoped<IReservationWriteRepository, ReservationWriteRepository>();


#endregion [ Infra - Data EventSourcing ]

#endregion [ IOC ]

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
