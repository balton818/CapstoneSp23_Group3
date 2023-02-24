using RecipePlannerApi.Service.Interface;
using UnitsNet;
using UnitsNet.Units;

namespace RecipePlannerApi.Service
{
    public enum AppUnit { NONE = 0, MILLILITERS = 1, GRAMS = 2, OUNCES = 3 }
    public class MeasurementService: IMeasurementService {
        private static readonly Dictionary<AppUnit, UnitInfo> AppUnitUnitInfo = new Dictionary<AppUnit, UnitInfo>() {
            { AppUnit.MILLILITERS,  Quantity.GetUnitInfo(VolumeUnit.Milliliter) },
            { AppUnit.GRAMS,  Quantity.GetUnitInfo(MassUnit.Gram) },
            { AppUnit.OUNCES,  Quantity.GetUnitInfo(MassUnit.Ounce) }
        };

        public int? Convert(decimal quantity, string fromUnit, AppUnit toUnit) {
            var unitInfo = AppUnitUnitInfo[toUnit];
            if (unitInfo == null) {
                return null;
            }
            var q = Quantity.Infos;

            if (UnitParser.Default.TryParse(fromUnit, unitInfo.Value.GetType(), out Enum fromEnum)) {
                var value = UnitConverter.Convert(quantity, fromEnum, unitInfo.Value);
                return (int) Math.Ceiling(value);
            }

            return null;
        }
    }
}
