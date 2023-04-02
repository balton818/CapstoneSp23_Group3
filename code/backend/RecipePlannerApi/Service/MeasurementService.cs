using RecipePlannerApi.Model;
using RecipePlannerApi.Service.Interface;
using System.Runtime;
using UnitsNet;
using UnitsNet.Units;

namespace RecipePlannerApi.Service
{
    public class MeasurementService: IMeasurementService {
        /// <summary>
        /// The dictionary to convert AppUnit into UnitInfo
        /// </summary>
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

        /// <summary>
        /// Converts the specified quantity to the specified unit.
        /// </summary>
        /// <param name="quantity">The quantity.</param>
        /// <param name="fromUnit">From unit.</param>
        /// <param name="toUnit">To unit.</param>
        /// <returns>System.Nullable&lt;System.Int32&gt;.</returns>
        public int? Convert(decimal quantity, string fromUnit, AppUnit toUnit) {
            if (!AppUnitUnitInfo.ContainsKey(toUnit)) {
                return null;
            }
            fromUnit = this.FormatUnit(fromUnit);
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

        /// <summary>
        /// Converts the specified quantity to a qualifying unit.
        /// </summary>
        /// <param name="quantity">The quantity.</param>
        /// <param name="fromUnit">From unit.</param>
        /// <param name="toUnit">To unit.</param>
        /// <returns>System.Nullable&lt;System.Int32&gt;.</returns>
        public Tuple<int, AppUnit> Convert(decimal quantity, string fromUnit) {
            double result = 0;
            AppUnit unit = AppUnit.NONE;
            var found = false;
            fromUnit = this.FormatUnit(fromUnit);
            foreach (var toUnit in AppUnitUnitInfo) {
                var unitInfo = AppUnitUnitInfo[toUnit.Key];

                var quantityName = AppUnitQuantity[toUnit.Key].Name;

                if (UnitConverter.TryConvertByName(quantity, quantityName, fromUnit, unitInfo.Name, out result)
                || UnitConverter.TryConvertByName(quantity, quantityName, $"Us{fromUnit}", unitInfo.Name, out result)
                || UnitConverter.TryConvertByName(quantity, quantityName, $"UsCustomary{fromUnit}", unitInfo.Name, out result)) {
                    unit = toUnit.Key;
                    found = true;
                    break;
                }
            }

            return found ? Tuple.Create((int)Math.Ceiling(result), unit) : null ;
        }

        /// <summary>
        /// Determines whether the specified unit is valid unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <returns><c>true</c> if the specified unit is valid unit; otherwise, <c>false</c>.</returns>
        public bool IsValidUnit(string unit) {
            if (string.IsNullOrEmpty(unit)) {
                return false;
            }
            unit = FormatUnit(unit);
            return CheckUnit(unit);
        }

        private string FormatUnit(string unit) {
            unit = unit.ToLower();
            var isPlural = unit.Last().ToString().Equals("s");
            unit = isPlural ? unit[..^1] : unit;
            return unit;
        }

        private bool CheckUnit(string unit) {
            double result;
            return UnitConverter.TryConvertByName(1, "Mass", unit, AppUnitUnitInfo[AppUnit.GRAMS].Name, out result)
            || UnitConverter.TryConvertByName(1, "Mass", $"Us{unit}", AppUnitUnitInfo[AppUnit.GRAMS].Name, out result)
            || UnitConverter.TryConvertByName(1, "Mass", $"UsCustomary{unit}", AppUnitUnitInfo[AppUnit.GRAMS].Name, out result)
            || UnitConverter.TryConvertByName(1, "Volume", unit, AppUnitUnitInfo[AppUnit.MILLILITERS].Name, out result)
            || UnitConverter.TryConvertByName(1, "Volume", $"Us{unit}", AppUnitUnitInfo[AppUnit.MILLILITERS].Name, out result)
            || UnitConverter.TryConvertByName(1, "Volume", $"UsCustomary{unit}", AppUnitUnitInfo[AppUnit.MILLILITERS].Name, out result)
            || UnitConverter.TryConvertByAbbreviation(1, "Mass", unit, AppUnitUnitInfo[AppUnit.GRAMS].Name, out result)
            || UnitConverter.TryConvertByAbbreviation(1, "Volume", unit, AppUnitUnitInfo[AppUnit.MILLILITERS].Name, out result);
        }
    }
}
