using RecipePlannerApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipePlannerApiTests.TestServices.TestMeasurementService {
    public class TestIsValidUnit {
        [Fact]
        public void TestValidUnitReturnsTrue() {
            var service = new MeasurementService();

            var value = service.IsValidUnit("cup");

            Assert.True(value);
        }

        [Fact]
        public void TestInvalidUnitReturnsFalse() {
            var service = new MeasurementService();

            var value = service.IsValidUnit("handful");

            Assert.False(value);
        }
    }
}
