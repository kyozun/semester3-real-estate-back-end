using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using semester3_real_estate_back_end.Data;
using semester3_real_estate_back_end.Interfaces;
using semester3_real_estate_back_end.Models;
using semester3_real_estate_back_end.Repository;
using semester3_real_estate_back_end.Services;
using semester4.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add Swagger Authorize Button
builder.Services.AddSwaggerGen(option =>
{
    option.EnableAnnotations();
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Semester 3 - Real Estate", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Nhập access token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("ApiCorsPolicy",
        builder => { builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); });
});

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add Data Seed
builder.Services.AddScoped<DataSeeder>();


// builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
// {
//     options.SuppressModelStateInvalidFilter = true;
// });


builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // Bỏ giá trị null ở Kết quả trả về
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Add Identity
builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    // options.Password.RequireDigit = true;
    // options.Password.RequiredLength = 8;
    // options.Password.RequireLowercase = true;
}).AddEntityFrameworkStores<DataContext>();


// Add Session
builder.Services.AddSession(options =>
{
    // Thời gian hết hạn = 10s
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
});
builder.Services.AddDistributedMemoryCache();


// Add Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = options.DefaultChallengeScheme = options.DefaultForbidScheme =
        options.DefaultScheme = options.DefaultSignInScheme =
            options.DefaultSignOutScheme =
                JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]!))
    };
});

//Add Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EditCommentPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    options.AddPolicy("EditCoverArtPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
});


// Repository
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IPropertyTypeRepository, PropertyTypeRepository>();
builder.Services.AddScoped<IDirectionRepository, DirectionRepository>();
builder.Services.AddScoped<IJuridicalRepository, JuridicalRepository>();
builder.Services.AddScoped<IProvinceRepository, ProvinceRepository>();
builder.Services.AddScoped<IDistrictRepository, DistrictRepository>();
builder.Services.AddScoped<IWardRepository, WardRepository>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IFileService, FileService>();

// Add BlobStorageService
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();

// Add BlobServiceClient
builder.Services.AddSingleton(x =>
    new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorage")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

// Add session
app.UseSession();

// Add core
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


// Run data seeder
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    var filePath = "semester3_data.xlsx";
    await seeder.SeedFromExcel(filePath);
}

// Setup static file from wwwroot folder
app.UseStaticFiles();

app.MapControllers();

app.Run();