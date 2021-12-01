using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class BodyType : IEntity
    {
        public int BodyTypeId { get; set; }
        public string BodyTypeName { get; set; }
    }
}
