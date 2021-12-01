using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Fuel : IEntity
    {
        public int FuelId { get; set; }
        public string FuelName { get; set; }
    }
}
