using PersonasApi.Configuration;
using PersonasApi.Data;
using PersonasApi.Data.Repositories;
using PersonasApi.Services;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

// Configurar Supabase Settings
builder.Services.Configure<SupabaseSettings>(
    builder.Configuration.GetSection("Supabase"));

var supabaseSettings = builder.Configuration
    .GetSection("Supabase")
    .Get<SupabaseSettings>();

// Inicializar cliente de Supabase
var options = new SupabaseOptions
{
    AutoConnectRealtime = true
};

builder.Services.AddSingleton(provider => 
{
    var client = new Supabase.Client(
        supabaseSettings!.Url, 
        supabaseSettings.Key, 
        options);
    client.InitializeAsync().Wait();
    return client;
});

builder.Services.AddScoped<SupabaseContext>();

// Registrar Repositorios
builder.Services.AddScoped<PersonaRepository>();
builder.Services.AddScoped<UserRepository>();

// Registrar Services
builder.Services.AddScoped<PersonaService>();
builder.Services.AddScoped<AuthService>();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();