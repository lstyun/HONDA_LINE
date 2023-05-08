using Coravel.Invocable;
using iWareDao.EnumType;
using iWareSda.Plc.Entity;

namespace iWareSda.Jobs.HeartBeat.SendHeartBeat
{
    
    public class SendHeartBeatJob : IInvocable
    {
        private readonly ILogger<SendHeartBeatJob> _logger;
 
        private readonly PlcS7Net _plc;

        private static readonly object _lock = new();


        public SendHeartBeatJob(ILogger<SendHeartBeatJob> logger, PlcS7Net plc)
        {
            _logger = logger;  
            _plc = plc; 
        }


        public async Task Invoke()
        {
            lock (_lock) 
            { 
                
                try
                {
                    _plc.WriteWHeartBeat(PlcS7Net.HeartFlag, ETaskType.SUPER_PLUS);
                    PlcS7Net.HeartFlag = !PlcS7Net.HeartFlag;
                    _logger.LogError("向plc发送心跳成功：_flag={PlcS7Net.HeartFlag}", PlcS7Net.HeartFlag);
                }
                catch (Exception ex)
                {
                    _logger.LogError("向plc发送心跳发生异常啦：{ex.Message}", ex.Message);
                }
            
            
            }
            await Task.CompletedTask;
            

            
        }
    }
}