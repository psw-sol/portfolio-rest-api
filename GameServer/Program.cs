using GameServer.Data;
using Common.Helpers;
using GameServer.Middleware;
using GameServer.Services;
using Microsoft.EntityFrameworkCore;
using Shared.Grpc;
using GameServer.Helpers;
using Common.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpcClient<Auth.AuthClient>(o =>
{
    o.Address = new Uri(builder.Configuration["Grpc:LoginServer"]);
});
builder.Services.AddScoped<AuthGrpcClient>();

builder.Services.AddControllers().AddMvcOptions(options =>
{
    options.InputFormatters.Insert(0, new ProtoBufInputFormatter());
    options.OutputFormatters.Insert(0, new ProtoBufOutputFormatter());
    options.Filters.Add<ProtobufResponseFilter>();
    options.Filters.Add<ProtobufExceptionFilter>();
});

builder.Services.AddScoped<IScopedDbContextAccessor, ScopedDbContextAccessor>();
builder.Services.AddDbContext<GlobalDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("GlobalDb"),
                     ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("GlobalDb"))));

builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<DbContextResolverMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();