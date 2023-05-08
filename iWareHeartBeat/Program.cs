// See https://aka.ms/new-console-template for more information
using RestSharp;
using SharpConfig;

Console.WriteLine("SDA心跳监控后台启动....");
var config = Configuration.LoadFromFile("config.ini")["config"];
var ip = config["ip"].StringValue;

while (true)
{
    try
    {
        var client = new RestClient(ip);
        var request = new RestRequest($"/heartbeat", Method.Get);
        var response = client.Execute(request);
        Console.WriteLine(response.Content);
        Thread.Sleep(1000);
    }
    catch (Exception)
    {
        Console.WriteLine("正在重启服务.....");
    }
}