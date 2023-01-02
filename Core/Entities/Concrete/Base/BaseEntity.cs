using Core.Entities.Abstract;

namespace Core.Base.Entities.Concrete.Base
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
        public string Created_by { get; set; }
        public string Modified_by { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Modified_at { get; set; }
        public bool IsActive { get; set; }
    }
}
