using RecipePlannerApi.Model;

namespace RecipePlannerApi.Service.Interface
{
    /// <summary>
    /// Interface IMeasurementService
    /// </summary>
    public interface IMeasurementService
    {
        /// <summary>
        /// Converts the specified quantity to the specified unit.
        /// </summary>
        /// <param name="quantity">The quantity.</param>
        /// <param name="fromUnit">From unit.</param>
        /// <param name="toUnit">To unit.</param>
        /// <returns>System.Nullable&lt;System.Int32&gt;.</returns>
        public int? Convert(decimal quantity, string fromUnit, AppUnit toUnit);
        /// <summary>
        /// Determines whether the specified unit is valid unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <returns><c>true</c> if the specified unit is valid unit; otherwise, <c>false</c>.</returns>
        bool IsValidUnit(string unit);
    }
}
