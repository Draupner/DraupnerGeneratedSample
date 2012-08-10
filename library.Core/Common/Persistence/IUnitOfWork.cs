using System;

namespace library.Core.Common.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
