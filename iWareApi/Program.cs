using iWareDao.DbContexts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//◊¢≤·“¿¿µœÓ
builder.Services.AddScoped<DbContext, DefaultDbContext>();



builder.Services.AddEndpointsApiExplorer();

//◊¢≤·SwaggerGen
builder.Services.AddSwaggerGen(c =>
{
    string? basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
    string xmlPath = Path.Combine(basePath ?? "", "iWareApi.xml");
    c.IncludeXmlComments(xmlPath);
});

//◊¢≤·øÁ”Ú
builder.Services.AddCors(options => options.AddPolicy("AllowAllOrigins",
    b => b.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(_ => true)));

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

var app = builder.Build();



app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAllOrigins");

app.Run();
