namespace iWareCommon.Entity
{
    public class RestResult
    {
        public string resCode { get; set; } = null!;
        public object data { get; set; } = null!;
        public string? msg { get; set; }
    }
}
