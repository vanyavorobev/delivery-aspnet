
using hitsLab.data.repository;
using hitsLab.domain.helpers;
using hitsLab.domain.services;
using hitsLab.presentation.middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IBasketService ,BasketService>();
builder.Services.AddTransient<IDishService, DishService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddScoped<IUserDetailsService, UserDetailsService>();
builder.Services.AddScoped<IDbRepository, DbRepository>();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DeliveryDbContext>(options => options.UseNpgsql(connection));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = TokenConfig.Issuer,
            ValidateAudience = true,
            ValidAudience = TokenConfig.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = TokenConfig.GetSymmetricalSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });
builder.Services.AddControllersWithViews();

builder.Services.AddHostedService<TokenDaemonService>();

var app = builder.Build();

using var serviceScope = app.Services.CreateScope();
var context = serviceScope.ServiceProvider.GetService<DeliveryDbContext>();
context?.Database.Migrate();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<TokenMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();