namespace iWareCommon.Entity
{
    public class PageResult<T>
    {
        public int Total { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public List<T> Data { get; set; } = null!;
        public string RangeInfo { get; set; } = null!;
    }
}
