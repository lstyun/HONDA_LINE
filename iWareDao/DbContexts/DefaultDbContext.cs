using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace iWareDao.DbContexts;

public class DefaultDbContext : DbContext
{
    private readonly string _connectionString;

    public DefaultDbContext(IConfiguration config)
    {
        //数据库链接字符串
        var connectionString = config["ConnectionStrings:DefaultConnection"];
        _connectionString = string.IsNullOrWhiteSpace(connectionString) ? "" : connectionString;
    }
    //实例表对象
    //public virtual DbSet<T> T { get; set; } = null!;

    //使用Sql服务
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(_connectionString);

    //配置表字段约束
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
    }
}