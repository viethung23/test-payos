using Net.payOS;

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();

PayOS payOS = new PayOS(configuration["Environment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find environment"),
                    configuration["Environment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find environment"),
                    configuration["Environment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find environment"));

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddSingleton(payOS);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMvc();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3003", "https://8efd-42-118-137-199.ngrok-free.app").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        });
});
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
