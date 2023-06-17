using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.ObjectPool;
using Microsoft.OpenApi.Models;
using SBToolsService.ServiceInterfaces;
using SBToolsService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "Test UI",             
        License = new OpenApiLicense
        {
            Name = "Test UI",
            Url = new Uri("https://localhost:44366/sbt2.html")
        },
    }) ;
});

builder.Services.AddScoped<ITokenService, TokenService>();

//var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: MyAllowSpecificOrigins,
//                      pb  =>
//                      {
//                          pb.AllowAnyOrigin().AllowAnyMethod();
//                         // policy.WithOrigins("http://example.com",
//                       //                       "http://www.contoso.com");
//                      });
//});

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



//app.UseCors(MyAllowSpecificOrigins);
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



//app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        // Set cache control headers to not cache JS files
        //if (ctx.File.Name.EndsWith(".js", StringComparison.OrdinalIgnoreCase))
        string[] extensions = { ".js", ".css" };

        if(extensions.Any(x => ctx.File.Name.EndsWith(x, StringComparison.OrdinalIgnoreCase)))
        {
            ctx.Context.Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");
            ctx.Context.Response.Headers.Append("Pragma", "no-cache");
            ctx.Context.Response.Headers.Append("Expires", "0");
        }
    }
});

app.Run();
