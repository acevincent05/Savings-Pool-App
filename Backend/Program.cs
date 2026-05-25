var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// builder.Services.AddControllers().AddJsonOptions(options =>
//     options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddOpenApi();

// builder.Services.AddDbContext<StudentContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

app.Run();

