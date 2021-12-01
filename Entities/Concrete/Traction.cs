using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Traction : IEntity
    {
        public int TractionId { get; set; }
        public string TractionName { get; set; }
    }
}
