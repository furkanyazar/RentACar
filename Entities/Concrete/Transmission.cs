using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Transmission : IEntity
    {
        public int TransmissionId { get; set; }
        public string TransmissionName { get; set; }
    }
}
