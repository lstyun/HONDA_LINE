using System.ComponentModel.DataAnnotations;

namespace iWareDao.Entity;

public class BaseEntity
{
    /// <summary>
    /// /// 主键
    /// /// </summary>
    [Key]
    //[Column("id")]
    public virtual long Id { get; set; }
}