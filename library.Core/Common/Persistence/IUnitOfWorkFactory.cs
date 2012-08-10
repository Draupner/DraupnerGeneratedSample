namespace library.Core.Common.Persistence
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
