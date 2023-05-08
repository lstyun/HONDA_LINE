using System.ComponentModel;

namespace iWareDao.EnumType
{
    /// <summary>
    /// 粗加工CNC名称
    /// </summary>
    public enum ERoughCncName
    {
        /// <summary>
        /// CNC1粗加工
        /// </summary>
        [Description("粗加工CNC1")]
        ROUGH_CNC1 = 11,

        /// <summary>
        /// CNC2粗加工
        /// </summary>
        [Description("粗加工CNC2")]
        ROUGH_CNC2 = 12
    }
}