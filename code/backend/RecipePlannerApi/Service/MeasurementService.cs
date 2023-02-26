using RecipePlannerApi.Model;
using RecipePlannerApi.Service.Interface;
using System.Runtime;
using UnitsNet;
using UnitsNet.Units;

namespace RecipePlannerApi.Service
{
    public class MeasurementService: IMeasurementService {
        public static readonly Dictionary<AppUnit, UnitInfo> AppUnitUnitInfo = new Dictionary<AppUnit, UnitInfo>() {
            { AppUnit.MILLILITERS,  Quantity.GetUnitInfo(VolumeUnit.Milliliter) },
            { AppUnit.GRAMS,  Quantity.GetUnitInfo(MassUnit.Gram) },
            { AppUnit.OUNCES,  Quantity.GetUnitInfo(MassUnit.Ounce) },
            { AppUnit.FLUID_OUNCES, Quantity.GetUnitInfo(VolumeUnit.UsOunce)}
        };

        private static readonly Dictionary<AppUnit, QuantityInfo> AppUnitQuantity = new Dictionary<AppUnit, QuantityInfo>() {
            { AppUnit.MILLILITERS,  Volume.Info },
            { AppUnit.GRAMS,  Mass.Info },
            { AppUnit.OUNCES,  Mass.Info },
            { AppUnit.FLUID_OUNCES, Volume.Info}
        };

        public int? Convert(decimal quantity, string fromUnit, AppUnit toUnit) {
            if (!AppUnitUnitInfo.ContainsKey(toUnit)) {
                return null;
            }
            var unitInfo = AppUnitUnitInfo[toUnit];

            var quantityName = AppUnitQuantity[toUnit].Name;

            double result;
            if (UnitConverter.TryConvertByName(quantity, quantityName, fromUnit, unitInfo.Name, out result)
            || UnitConverter.TryConvertByName(quantity, quantityName, $"Us{fromUnit}", unitInfo.Name, out result)
            || UnitConverter.TryConvertByName(quantity, quantityName, $"UsCustomary{fromUnit}", unitInfo.Name, out result)) {
                return (int) Math.Ceiling(result);
            } else {
                return null;
            }
        }

        public bool IsValidUnit(string unit) {
            double result;
            return UnitConverter.TryConvertByName(1, "Mass", unit, AppUnitUnitInfo[AppUnit.GRAMS].Name, out result)
            || UnitConverter.TryConvertByName(1, "Mass", $"Us{unit}", AppUnitUnitInfo[AppUnit.GRAMS].Name, out result)
            || UnitConverter.TryConvertByName(1, "Mass", $"UsCustomary{unit}", AppUnitUnitInfo[AppUnit.GRAMS].Name, out result)
            || UnitConverter.TryConvertByName(1, "Volume", unit, AppUnitUnitInfo[AppUnit.MILLILITERS].Name, out result)
            || UnitConverter.TryConvertByName(1, "Volume", $"Us{unit}", AppUnitUnitInfo[AppUnit.MILLILITERS].Name, out result)
            || UnitConverter.TryConvertByName(1, "Volume", $"UsCustomary{unit}", AppUnitUnitInfo[AppUnit.MILLILITERS].Name, out result);
        }
    }
}
