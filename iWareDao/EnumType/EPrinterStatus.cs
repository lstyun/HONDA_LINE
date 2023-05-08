using System.ComponentModel;

namespace iWareDao.EnumType
{
    public enum EPrinterStatus
    {
        /// <summary>
        /// 脱机
        /// </summary>
        [Description("脱机")]
        OUT = 1,

        /// <summary>
        /// 工作中
        /// </summary>
        [Description("工作中")]
        BUSY = 2,

        /// <summary>
        /// 空闲
        /// </summary>
        [Description("空闲")]
        FREE = 3

    }
}
