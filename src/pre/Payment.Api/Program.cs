using Payment.Api.Services;
using Payment.Application.Features.Commands;
using Payment.Application.Interface;
using Payment.Persistence.Persist;
using Payment.Service.VnPay.Config;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Version = "v1",
        Title = "Payment service api",
        Description = "Sample .NET payment api",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
        {
            Name = "Huu Thang",
            Url = new Uri("https://www.facebook.com/huu.thang.77377/")
        }
    });
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var path = Path.Combine(AppContext.BaseDirectory, xmlFileName);
    options.IncludeXmlComments(path);
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISqlService, SqlService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IConnectionService, ConnectionService>();

builder.Services.AddMediatR(r =>
{
    r.RegisterServicesFromAssembly(typeof(CreateMerchant).Assembly);
});

builder.Services.Configure<VnpayConfig>(
                builder.Configuration.GetSection(VnpayConfig.ConfigName));

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
