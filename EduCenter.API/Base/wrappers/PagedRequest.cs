public class PagedRequest<T>
{
    public int StartIndex { get; set; }
    public int Limit { get; set; }
    public T? Filters { get; set; }
}
