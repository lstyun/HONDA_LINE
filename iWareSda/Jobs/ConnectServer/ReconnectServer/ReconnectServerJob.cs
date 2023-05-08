using Coravel.Invocable;
using iWareSda.Plc.Entity;

namespace iWareSda.Jobs.ConnectServer.ReconnectServer
{

    public class ReconnectServerJob : IInvocable
    {
        private readonly ILogger<ReconnectServerJob> _logger;
 
        private readonly PlcS7Net _plc;


        public ReconnectServerJob(ILogger<ReconnectServerJob> logger, PlcS7Net plc)
        {
            _logger = logger;  
            _plc = plc; 
        }


        public async Task Invoke()
        {
            lock (PlcS7Net._Lock)
            {
                if (PlcS7Net.FailureTimes > 1000)
                {
                    _logger.LogInformation("PLC连接已重置：_FailureTimes={PlcHsl._FailureTimes}", PlcS7Net.FailureTimes);
                    PlcS7Net.FailureTimes = 0;
                    _plc.CloseServer();
                    _plc.ConnectServer();
                }
            }
            await Task.CompletedTask;
        }
    }
}