namespace MSP.Core.Domain.Entities
{
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
        bool IsTransient();
    }
}
