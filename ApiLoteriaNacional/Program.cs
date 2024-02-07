using ApiLoteriaNacional.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<SeguridadData>();
builder.Services.AddScoped<MantenimientoData>();
builder.Services.AddScoped<StoreCheckData>();
builder.Services.AddScoped<TombolaData>();

builder.Services.AddCors(
    options => {
        options.AddPolicy(name: "API",
        builder =>
        {
            builder.WithHeaders("*");
            builder.WithOrigins("*");
            builder.WithMethods("*");
        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("API");

app.MapControllers();

app.Run();
