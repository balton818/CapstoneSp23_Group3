using Moq;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service;

namespace RecipePlannerApiTests.TestServices.TestMealPlanService {
    public class TestAddMeal {
        [Fact]
        public void TestValidRequest() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.CreateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);
            var meal = new Meal {
                MealPlanId = 1,
                DayOfWeek = DayOfWeek.Monday,
                MealType = MealType.BREAKFAST,
                Recipe = new Recipe {
                    ApiId = 40,
                    Image = "a",
                    ImageType = "a",
                    Title = "a"
                }
            };
            var result = service.AddMeal(meal);

            Assert.NotNull(result);
        }

        [Fact]
        public void TestNullRequest() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.CreateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);

            Assert.Throws<ArgumentNullException>(() => service.GetMealPlan(null));
        }

        [Fact]
        public void TestMealPlanIdZero() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.CreateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);
            var meal = new Meal {
                MealPlanId = 0,
                DayOfWeek = DayOfWeek.Monday,
                MealType = MealType.BREAKFAST,
                Recipe = new Recipe {
                    ApiId = 40,
                    Image = "a",
                    ImageType = "a",
                    Title = "a"
                }
            };
            Assert.Throws<ArgumentException>(() => service.AddMeal(meal));
        }

        [Fact]
        public void TestDayOfWeekInvalid() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.CreateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);
            var meal = new Meal {
                MealPlanId = 1,
                DayOfWeek = (DayOfWeek)8,
                MealType = MealType.BREAKFAST,
                Recipe = new Recipe {
                    ApiId = 40,
                    Image = "a",
                    ImageType = "a",
                    Title = "a"
                }
            };
            Assert.Throws<ArgumentException>(() => service.AddMeal(meal));
        }

        [Fact]
        public void TestRecipeApiIdZero() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.CreateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);
            var meal = new Meal {
                MealPlanId = 1,
                DayOfWeek = (DayOfWeek)8,
                MealType = MealType.BREAKFAST,
                Recipe = new Recipe {
                    ApiId = 0,
                    Image = "a",
                    ImageType = "a",
                    Title = "a"
                }
            };
            Assert.Throws<ArgumentException>(() => service.AddMeal(meal));
        }

        [Fact]
        public void TestRecipeImageNull() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.CreateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);
            var meal = new Meal {
                MealPlanId = 1,
                DayOfWeek = (DayOfWeek)8,
                MealType = MealType.BREAKFAST,
                Recipe = new Recipe {
                    ApiId = 40,
                    Image = null,
                    ImageType = "a",
                    Title = "a"
                }
            };
            Assert.Throws<ArgumentException>(() => service.AddMeal(meal));
        }

        [Fact]
        public void TestRecipeImageEmpty() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.CreateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);
            var meal = new Meal {
                MealPlanId = 10,
                DayOfWeek = (DayOfWeek)8,
                MealType = MealType.BREAKFAST,
                Recipe = new Recipe {
                    ApiId = 40,
                    Image = "",
                    ImageType = "a",
                    Title = "a"
                }
            };
            Assert.Throws<ArgumentException>(() => service.AddMeal(meal));
        }

        [Fact]
        public void TestRecipeImageTypeNull() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.CreateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);
            var meal = new Meal {
                MealPlanId = 10,
                DayOfWeek = (DayOfWeek)8,
                MealType = MealType.BREAKFAST,
                Recipe = new Recipe {
                    ApiId = 40,
                    Image = "a",
                    ImageType = null,
                    Title = "a"
                }
            };
            Assert.Throws<ArgumentException>(() => service.AddMeal(meal));
        }

        [Fact]
        public void TestRecipeImageTypeEmpty() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.CreateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);
            var meal = new Meal {
                MealPlanId = 10,
                DayOfWeek = (DayOfWeek)8,
                MealType = MealType.BREAKFAST,
                Recipe = new Recipe {
                    ApiId = 10,
                    Image = "a",
                    ImageType = "",
                    Title = "a"
                }
            };
            Assert.Throws<ArgumentException>(() => service.AddMeal(meal));
        }

        [Fact]
        public void TestRecipeTitleNull() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.CreateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);
            var meal = new Meal {
                MealPlanId = 10,
                DayOfWeek = (DayOfWeek)8,
                MealType = MealType.BREAKFAST,
                Recipe = new Recipe {
                    ApiId = 10,
                    Image = "a",
                    ImageType = "a",
                    Title = null
                }
            };
            Assert.Throws<ArgumentException>(() => service.AddMeal(meal));
        }

        [Fact]
        public void TestRecipeTitleEmpty() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.CreateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);
            var meal = new Meal {
                MealPlanId = 10,
                DayOfWeek = (DayOfWeek)8,
                MealType = MealType.BREAKFAST,
                Recipe = new Recipe {
                    ApiId = 10,
                    Image = "a",
                    ImageType = "a",
                    Title = ""
                }
            };
            Assert.Throws<ArgumentException>(() => service.AddMeal(meal));
        }
    }
}
