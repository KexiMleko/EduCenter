using EduCenter.API.Data;
namespace EduCenter.API.Base;
public class BaseRepository
{
    protected readonly DatabaseContext appContext;
    public BaseRepository(DatabaseContext appContext)
    {
        this.appContext = appContext;
    }
    public async Task SaveChangesAsync(CancellationToken ct) =>
        await appContext.SaveChangesAsync(ct);

}
