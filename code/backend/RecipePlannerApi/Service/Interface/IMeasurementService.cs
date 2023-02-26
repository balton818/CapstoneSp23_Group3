using RecipePlannerApi.Model;

namespace RecipePlannerApi.Service.Interface
{
    public interface IMeasurementService
    {
        public int? Convert(decimal quantity, string fromUnit, AppUnit toUnit);
        bool IsValidUnit(string unit);
    }
}
