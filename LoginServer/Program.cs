using LoginServer.Data;
using Common.Helpers;
using LoginServer.Services;
using Microsoft.EntityFrameworkCore;
using Common.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MemberDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MemberDb"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MemberDb"))));


builder.Services.AddControllers().AddMvcOptions(options =>
{
    options.InputFormatters.Insert(0, new ProtoBufInputFormatter());
    options.OutputFormatters.Insert(0, new ProtoBufOutputFormatter());

    options.Filters.Add<ProtobufResponseFilter>();
    options.Filters.Add<ProtobufExceptionFilter>();
});

builder.Services.AddGrpc();
builder.Services.AddSingleton<JwtService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapGrpcService<AuthGrpcService>();
app.UseHttpsRedirection();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();

