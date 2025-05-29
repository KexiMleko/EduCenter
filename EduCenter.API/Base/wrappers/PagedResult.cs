public class PagedResult<T>
{
    public int StartIndex { get; set; }
    public int Limit { get; set; }
    public int TotalCount { get; set; }
    public List<T> Items { get; set; } = new List<T>();

    public int Count => Items.Count;
}
