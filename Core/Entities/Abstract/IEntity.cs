namespace Core.Entities.Abstract
{
    public interface IEntity
    {
        int Id { get; set; }
        public bool IsActive { get; set; }
    }
}
