public interface IBaseRepository
{
    public Task SaveChangesAsync(CancellationToken ct);
}
