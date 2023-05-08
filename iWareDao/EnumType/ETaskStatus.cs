using System.ComponentModel;

namespace iWareDao.EnumType
{
    /// <summary>
    /// 任务状态
    /// </summary>
    public enum ETaskStatus
    {
        /// <summary>
        /// 初始状态
        /// </summary>
        [Description("初始状态")]
        INIT = 0,

        /// <summary>
        /// 执行中
        /// </summary>
        [Description("执行中")]
        DOING = 1,

        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        DONE = 2,

        /// <summary>
        /// 已完成
        /// </summary>
        [Description("称重中")]
        WEIGHING = 3,

        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已称重")]
        WEIGHED = 4,

        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已檢驗")]
        CHECKED = 5,

        


        /// <summary>
        /// 已取消
        /// </summary>
        [Description("已取消")]
        CANCELED = 99



    }
}