using Coravel;
using iWareDao.DbContexts;
using iWareDao.EnumType;
using iWareSda.Jobs.ConnectServer.ReconnectServer;
using iWareSda.Jobs.HeartBeat.SendHeartBeat;
using iWareSda.Plc.Entity;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScheduler();

builder.Services.AddSingleton<DbHeartBeat>();


builder.Services.AddSingleton<PlcS7Net>();

builder.Services.AddScoped<DbContext, DefaultDbContext>();

builder.Services.AddSingleton<ReconnectServerJob>();

builder.Services.AddSingleton<SendHeartBeatJob>();

builder.Services.AddCors(options => options.AddPolicy("AllowAllOrigins",
    b => b.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(_ => true)));


builder.Logging.AddNLog("Cfg/NLog.config");

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();



app.MapGet("/connectServer", (PlcS7Net _plc) =>
{
    return _plc.ConnectServer();
}).WithName("connectServer");


app.MapGet("/closeServer", (PlcS7Net _plc) =>
{
    return _plc.CloseServer();
}).WithName("closeServer");


app.MapGet("/writeInt16", (PlcS7Net _plc, string db, short v) =>
{
    return _plc.Write(db, v);
}).WithName("writeInt16");


app.MapGet("/readInt16", (PlcS7Net _plc, string db) =>
{
    return _plc.ReadInt16(db);
}).WithName("readInt16");

app.MapGet("/writeInt32", (PlcS7Net _plc, string db, int v) =>
{
    return _plc.Write(db, v);
}).WithName("writeInt32");


app.MapGet("/readInt32", (PlcS7Net _plc, string db) =>
{
    return _plc.ReadInt32(db);
}).WithName("readInt32");


app.MapGet("/writeInt64", (PlcS7Net _plc, string db, long v) =>
{
    return _plc.Write(db, v);
}).WithName("writeInt64");


app.MapGet("/readInt64", (PlcS7Net _plc, string db) =>
{
    return _plc.ReadInt64(db);
}).WithName("readInt64");



app.MapGet("/writeBool", (PlcS7Net _plc, string db, bool v) =>
{
    return _plc.Write(db, v);
}).WithName("writeBool");


app.MapGet("/readBool", (PlcS7Net _plc, string db) =>
{
    return _plc.ReadBool(db);
}).WithName("readBool");


app.MapGet("/writeDouble", (PlcS7Net _plc, string db, double v) =>
{
    return _plc.Write(db, v);
}).WithName("writeDouble");


app.MapGet("/readDouble", (PlcS7Net _plc, string db) =>
{
    return _plc.ReadDouble(db);
}).WithName("readDouble");


app.MapGet("/writeString", (PlcS7Net _plc, string db, string v) =>
{
    return _plc.Write(db, v);
}).WithName("writeString");


app.MapGet("/readString", (PlcS7Net _plc, string db, int len) =>
{
    return _plc.ReadString(db, len);
}).WithName("readString");

app.MapGet("/heartbeat", (PlcS7Net _plc) =>
{
    return _plc.Heartbeat();
}).WithName("heartbeat");


app.MapGet("/error", () =>
{
    return "³ö´íÀ²";
}).WithName("error");

app.Services.UseScheduler(scheduler => {
    scheduler.Schedule<ReconnectServerJob>().EverySeconds(15);
    scheduler.Schedule<SendHeartBeatJob>().EverySeconds(1);
});

app.UseStatusCodePagesWithReExecute("/error");

app.UseCors("AllowAllOrigins");


app.Run();

