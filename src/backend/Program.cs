using backend.Infrastructure.Data.DbContext.master;
using backend.Infrastructure.Data.DbContext.slave;
using backend.Infrastructure.Repository;
using backend.Services.CMS.FeedBack;
using backend.Services.CMS.Menu;
using backend.Services.CMS.News;
using backend.Services.SM;
using backend.Services.SM.News;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using uc.api.cms.Helper;
using uc.api.cms.Infrastructure.Authentication;
using uc.api.cms.Infrastructure.Swagger;
using UC.Core.Common;
using UC.Core.Models.Ums;

var builder = WebApplication.CreateBuilder(args);
string AllowOrigins = "TrustedOrigins";
string allowedHosts = "*";
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowOrigins,
    builder =>
    {
        builder.WithOrigins(allowedHosts).AllowAnyHeader().AllowAnyMethod();
    });
});
var config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json").Build();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "uc.api.cms",
        Version = "v1"
    });
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
    c.CustomSchemaIds(i => i.FullName);
    c.SchemaFilter<SwaggerExcludeFilter>();
});
builder.Services.AddScoped<DbSession>();
builder.Services.AddTransient<UnitOfWork>();

builder.Services.AddScoped<DbReportSession>();
builder.Services.AddTransient<UnitOfWorkReport>();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUserProvider, UserProvider>();
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddScoped(typeof(IRepositoryWrapper), typeof(RepositoryWrapper));
builder.Services.AddScoped<ISMNewsServices, SMNewsServices>();
builder.Services.AddScoped<ICMSNewsServices, CMSNewsServices>();
builder.Services.AddScoped<IMenuServices, MenuServices>();
builder.Services.AddScoped<IAccountServices,AccountServices>();
builder.Services.AddScoped<IFeedBackServices, FeedBackServices>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
SMSecurityHelper.CreateKey(config);
var jwtTokenConfig = config.GetSection("IdentityServerAuthentication").Get<IdentityServerAuthentication>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = jwtTokenConfig.RequireHttpsMetadata;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtTokenConfig.Issuer,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
        ValidAudience = jwtTokenConfig.Audience,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddSingleton<IJwtAuthManager>(provider => new JwtAuthManager(jwtTokenConfig));
var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(AllowOrigins);
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
string root = Path.Combine(Directory.GetCurrentDirectory(), "static");
if (!File.Exists(root))
{
    Directory.CreateDirectory(root);
}

app.Run();
