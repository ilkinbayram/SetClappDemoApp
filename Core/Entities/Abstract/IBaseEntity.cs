namespace Core.Entities.Abstract
{
    public interface IBaseEntity : IEntity
    {
        string Created_by { get; set; }
        string Modified_by { get; set; }
        DateTime? Created_at { get; set; }
        DateTime? Modified_at { get; set; }
    }
}
