using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.EventSource;
using Serilog;
using VillaApp.Models.Data;
using VillaApp.Models.Repository;
using VillaApp.Models.Repository.IRepository;

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

			
			builder.Host.UseSerilog();

			builder.Services.AddControllers(options =>
			{
				options.ReturnHttpNotAcceptable=true;
			}).AddNewtonsoftJson();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<AppDbContext>();

			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddAutoMapper(typeof(MappingConfig));
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
		}
	}
}
