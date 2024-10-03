using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.EventSource;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using VillaApp.Models.Data;
using VillaApp.Models.Repository;
using VillaApp.Models.Repository.IRepository;
using VillaApp.Options;

namespace VillaApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			Log.Logger = new LoggerConfiguration().MinimumLevel.Warning()
				.WriteTo.File("log/villaLogs.txt", rollingInterval: RollingInterval.Year).CreateLogger();

			//builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT"));
			
			builder.Host.UseSerilog();

			builder.Services.AddControllers()
				.AddNewtonsoftJson();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(options =>
			{
				
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
					Name = "Authorization", 
					In = ParameterLocation.Header, 
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer" 
				});

				
				options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer" 
							},
							Scheme = "Bearer", 
							Name = "Bearer",
							In = ParameterLocation.Header
						},
						new List<string>()
					}
				});
			});



			builder.Services.AddResponseCaching();

			builder.Services.AddDbContext<AppDbContext>();
			builder.Services.AddScoped<IVillasRepository,VillaRepository>();
			builder.Services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddAutoMapper(typeof(MappingConfig));

			//Authentication JWT
			var jwtOptions = builder.Configuration.GetSection("JWT").Get<JwtOptions>();
			builder.Services.AddSingleton(jwtOptions);

			builder.Services.AddAuthentication()
				.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
				Options =>
				{
					Options.SaveToken = true;
					Options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidIssuer = jwtOptions.issuer,
						ValidateAudience = true,
						ValidAudience = jwtOptions.audience,
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.signkey))
					};
				});



			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
