using Backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Backend.Controllers;
using Backend.Repository;
using Backend.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();


builder.Services.AddScoped<IPolicyServices, PolicyServices>();
builder.Services.AddScoped<IAdminService, AdminServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IPaymentServices, PaymentServices>();
builder.Services.AddScoped<ISupportDocumentServices, SupportDocumentServices>();
builder.Services.AddScoped<ITempFormDataServices, TempFormDataServices>();
builder.Services.AddScoped<IVehicleServices, VehicleServices>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IReminderService, ReminderService>();



// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminAndUser", policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole("admin") || context.User.IsInRole("user")));
});


// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true, // Set to true to validate the token's expiration time
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:3000") // Replace with your frontend URL
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials(); // Allow credentials (cookies, authorization headers)
        });
});
// Configure the DbContext
builder.Services.AddDbContext<DriveDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

// Build the app
var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

// Middleware for development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable authentication and authorization middleware
app.UseHttpsRedirection();
app.UseAuthentication(); // This is required for JWT authentication
app.UseAuthorization();

app.MapControllers();



app.Run();
