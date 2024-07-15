using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Katalitica_API.Resources;
using Identity;
using Katalitica_API.Identity;
using Katalitica_API.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ConfigurationAD>(c =>
{
    c.Port = configuration.GetSection("AD:port").Get<int>();

    c.Zone = configuration.GetSection("AD:zone").Value;
    c.Domain = configuration.GetSection("AD:domain").Value;
    c.Subdomain = configuration.GetSection("AD:subdomain").Value;

    c.Username = configuration.GetSection("AD:username").Value;
    c.Password = configuration.GetSection("AD:password").Value;

    // connection string with port doesn't work on GNU/Linux and Mac OS
    //c.LDAPserver = $"{c.Subdomain}.{c.Domain}.{c.Zone}:{c.Port}";
    c.LDAPserver = $"{c.Subdomain}.{c.Domain}.{c.Zone}";
    // that depends on how it is in your LDAP server
    //c.LDAPQueryBase = $"DC={c.Subdomain},DC={c.Domain},DC={c.Zone}";
    c.LDAPQueryBase = $"DC={c.Domain},DC={c.Zone}";

    c.Crew = new StringBuilder()
        .Append($"CN={configuration.GetSection("AD:crew").Value},")
        // check which CN (Users or Groups) your LDAP server has the groups in
        .Append($"CN=Users,{c.LDAPQueryBase}")
        .ToString();
    c.Managers = new StringBuilder()
        .Append($"CN={configuration.GetSection("AD:managers").Value},")
        // check which CN (Users or Groups) your LDAP server has the groups in
        .Append($"CN=Users,{c.LDAPQueryBase}")
        .ToString();

});


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:81").AllowAnyHeader()
                                .AllowAnyMethod();
                          policy.WithOrigins("https://localhost:3001").AllowAnyHeader()
                                .AllowAnyMethod();
                          policy.WithOrigins("https://localhost:81").AllowAnyHeader()
                                .AllowAnyMethod();
                          policy.WithOrigins("http://201.131.21.31").AllowAnyHeader()
                                .AllowAnyMethod();
                          policy.WithOrigins("https://201.131.21.31").AllowAnyHeader()
                                .AllowAnyMethod();
                          policy.AllowAnyOrigin();
                      });
});

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole(); // Adds console logging
    logging.AddDebug();   // Adds debug logging
    // Add other logging providers if needed
});
builder.Services.AddScoped<LdapService>();

builder.Services.AddSingleton<AuthService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/* builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    //API Validation config for jwt
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

    };
});
 */
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
    });



builder.Services.AddDbContext<DBDatos>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
    options.HttpsPort = 443; // Default port for HTTPS
});


builder.WebHost.UseSetting("https_port", "443");

var app = builder.Build();
app.Use(async (context, next) =>
{
    
    context.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:81");
    context.Response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,PUT,DELETE,OPTIONS");
    context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");
    await next.Invoke();
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(myAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();