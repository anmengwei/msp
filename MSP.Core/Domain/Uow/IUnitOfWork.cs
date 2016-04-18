using System;

namespace MSP.Core.Domain.Uow
{
    public interface IUnitOfWork:IDisposable
    {
        string Id { get; set; }
        bool IsDisposed { get; set; }
        void Begin();
        void Complete();

        event EventHandler Completed;

        event EventHandler Faild;
    }
}
