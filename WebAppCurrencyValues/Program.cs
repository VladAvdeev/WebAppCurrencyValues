using WebAppCurrencyValues.Services;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAutoMapper(typeof(AutoMappingProfile));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICurrencyService, CurrencyService>();

builder.Services.AddHttpClient<CurrencyService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetConnectionString("CurrencyUrl").ToString());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.Logger.LogInformation("Starting application");
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Default",
        pattern: "{controller=Currency}/{action=GetAllCurrencies}");
});

app.Run();
