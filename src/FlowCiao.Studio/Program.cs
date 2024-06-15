using System.Text.Json.Serialization;
using Asp.Versioning;
using FlowCiao;
using FlowCiao.Persistence.Providers.Rdbms;
using FlowCiao.Persistence.Providers.Rdbms.SqlServer;
using FlowCiao.Studio.Extensions;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
{
	builder.Services.AddControllers().AddJsonOptions(options =>
		options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();

	var server = builder.Configuration.GetConnectionString("FlowCiao");

	//Testing the FlowCiaoDbContext
	//It is not connecting
	//but this works -> builder.Services.AddDbContext<TestDbContext>(u => u.UseSqlServer(server));
	builder.Services.AddDbContext<FlowCiaoDbContext>(u => u.UseSqlServer(server));
	
	builder.Services.AddCors(options =>
	{
		options.AddPolicy("DevelopmentCorsPolicy",
			builder => builder
				.WithOrigins("http://localhost:3000", "http://127.0.0.1:3000")
				.AllowAnyMethod()
				.AllowAnyHeader()
				.AllowCredentials()
		);
	});


	builder.Services.AddApiVersioning(options =>
	{
		options.DefaultApiVersion = new ApiVersion(1);
		options.ReportApiVersions = true;
		options.AssumeDefaultVersionWhenUnspecified = true;
		options.ApiVersionReader = ApiVersionReader.Combine(
			new UrlSegmentApiVersionReader(),
			new HeaderApiVersionReader("X-Api-Version"));
	}).AddApiExplorer(options =>
	{
		options.GroupNameFormat = "'v'V";
		options.SubstituteApiVersionInUrl = true;
	});
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseCors("DevelopmentCorsPolicy");
}


//app.UseFlowCiao();
var logger = app.Services.GetRequiredService<ILogger<Exception>>();
app.ConfigureExceptionHandler(logger);

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();


app.Run();


//This is used for testing the db context to check if my connection string works properly

public class TestDbContext : DbContext
{
	public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }
	public DbSet<TestItems> items { get; set; }
}

public class TestItems
{
	public int Id { get; set; }
	public string Name { get; set; }
}


