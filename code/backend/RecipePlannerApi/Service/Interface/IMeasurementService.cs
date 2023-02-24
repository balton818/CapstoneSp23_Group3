namespace RecipePlannerApi.Service.Interface
{
    public interface IMeasurementService
    {
        public int? Convert(decimal quantity, string fromUnit, AppUnit toUnit);
    }
}
