using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PopcornBackend.MailHelper;
using PopcornBackend.Models;
using PopcornBackend.Services;
using System.Text;
using Serilog.Events;
using Serilog;
using Serilog.Formatting.Compact;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft",LogEventLevel.Warning)
    .WriteTo.File(new CompactJsonFormatter(),"./Log/Log.txt", rollingInterval : RollingInterval.Day)
    .CreateLogger();

Log.Logger.Information("Logging is working fine");

var builder = WebApplication.CreateBuilder(args);

//adding service to the container
builder.Host.UseSerilog();

builder.Services.AddDbContext<MajorProjectDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connection") ?? throw new InvalidOperationException("Connection string 'WebApiCodeFirstContext' not found.")));




builder.Services.AddCors(options => {
    options.AddPolicy("Cors", p => {
        p.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


//SERVICE DEPENDENCIES
builder.Services.AddScoped<IAuth, AuthService>();
builder.Services.AddScoped<ISongService, SongService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<ITvShowService,TvShowService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddEndpointsApiExplorer();


//JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "localhost",
            ValidAudience = "localhost",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ClockSkew = TimeSpan.FromHours(1)
        };
    });

//JWT Authorization
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
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
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddSwaggerGen();

//email service adding to pipeline
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddAuthorization();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Cors");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
