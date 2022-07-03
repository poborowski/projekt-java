using Microsoft.IdentityModel.Tokens;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using projekt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var authenticationSetting = new AuthenticationSettings();
// Add services to the container.
builder.Configuration.GetSection("Authentication").Bind(authenticationSetting);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";

}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSetting.JwtIssuer,
        ValidAudience = authenticationSetting.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSetting.JwtKey)),
    };
});
builder.Services.AddRazorPages();
builder.Services.AddOcelot().AddCacheManager(s => s.WithDictionaryHandle());
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Host.ConfigureAppConfiguration(c => c.AddJsonFile($"ocelot.{env}.json"));
var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseAuthentication();
app.UseOcelot().Wait();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();