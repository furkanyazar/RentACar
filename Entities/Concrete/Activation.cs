using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Activation : IEntity
    {
        public int ActivationId { get; set; }
        public int UserId { get; set; }
        public string ActivationCode { get; set; }
    }
}
