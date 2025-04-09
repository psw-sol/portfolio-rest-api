namespace Common.Helper
{
    public interface ITransactionalDbContext
    {
        bool HasChanges();
    }

}
