using RecipePlannerApi.Service;

namespace RecipePlannerApiTests.TestServices.TestMeasurementService {
    public class TestConvert {
        [Fact]
        public void TestCupsConvertsToMilliliters() {
            var service = new MeasurementService();

            var amount = service.Convert(5, "cup", RecipePlannerApi.Model.AppUnit.MILLILITERS);

            Assert.Equal(1183, amount);
        }

        [Fact]
        public void TestPintsConvertsToMilliliters() {
            var service = new MeasurementService();

            var amount = service.Convert(5, "pint", RecipePlannerApi.Model.AppUnit.MILLILITERS);

            Assert.Equal(2366, amount);
        }

        [Fact]
        public void TestUnitDoesNotConvertToNone() {
            var service = new MeasurementService();

            var amount = service.Convert(5, "pinch", RecipePlannerApi.Model.AppUnit.NONE);

            Assert.Null(amount);
        }
    }
}
