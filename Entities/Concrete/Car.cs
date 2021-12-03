using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Car : IEntity
    {
        public int CarId { get; set; }
        public int CompanyId { get; set; }
        public int ModelId { get; set; }
        public int FuelId { get; set; }
        public int TransmissionId { get; set; }
        public int BodyTypeId { get; set; }
        public int TractionId { get; set; }
        public short Seats { get; set; }
        public short EngineSize { get; set; }
        public short ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
    }
}
