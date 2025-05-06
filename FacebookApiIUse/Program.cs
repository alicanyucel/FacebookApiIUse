using FacebookApiIUse.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSingleton<FacebookService>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}
app.UseAuthorization();
app.MapControllers();
app.Run();
