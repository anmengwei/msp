namespace MSP.Core.Domain.Uow
{
    public interface ICurrentUnitOfWorkProvider
    {
        IUnitOfWork Current { get; set; }
    }
}
