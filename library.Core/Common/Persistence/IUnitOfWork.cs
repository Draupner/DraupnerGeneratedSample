using System;

namespace Library.Core.Common.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
