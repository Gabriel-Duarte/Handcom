using Handcom.Api.Configuration;
using Handcom.Ioc.IoC;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSwagger(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureAPI(builder.Configuration);
builder.Services.AddCors(options =>
{
    var WebApplication = builder.Configuration["WebApplication"];
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins(WebApplication)
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
