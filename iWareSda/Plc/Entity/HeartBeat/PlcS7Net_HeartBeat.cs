using HslCommunication;
using iWareDao.EnumType;

namespace iWareSda.Plc.Entity
{
    public partial class PlcS7Net
    {
        public OperateResult WriteWHeartBeat(bool flag, ETaskType taskType)
        {     
            string dbWHeartBeat = "";
            switch (taskType)
            {
                case ETaskType.SUPER_PLUS:
                    dbWHeartBeat = _dbHeartBeat.WHeartBeat;
                    break;
                
            }
            return Write(dbWHeartBeat, flag);
        }
    }
}
