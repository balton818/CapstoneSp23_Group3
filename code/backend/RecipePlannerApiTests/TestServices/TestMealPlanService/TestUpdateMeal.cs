using Moq;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service;

namespace RecipePlannerApiTests.TestServices.TestMealPlanService {
    public class TestUpdateMeal {
        [Fact]
        public void TestValidRequest() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.UpdateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);
            var meal = new Meal {
                MealId = 1,
                DayOfWeek = DayOfWeek.Monday,
                MealType = MealType.BREAKFAST,
                Recipe = new Recipe {
                    ApiId = 40,
                    Image = "a",
                    ImageType = "a",
                    Title = "a"
                }
            };
            var result = service.UpdateMeal(meal);

            Assert.NotNull(result);
        }

        [Fact]
        public void TestNullRequest() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.UpdateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);

            Assert.Throws<ArgumentNullException>(() => service.GetMealPlan(null));
        }

        [Fact]
        public void TestMealIdZero() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.UpdateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);
            var meal = new Meal {
                MealId = 0,
                DayOfWeek = DayOfWeek.Monday,
                MealType = MealType.BREAKFAST,
                Recipe = new Recipe {
                    ApiId = 40,
                    Image = "a",
                    ImageType = "a",
                    Title = "a"
                }
            };
            Assert.Throws<ArgumentException>(() => service.UpdateMeal(meal));
        }

        [Fact]
        public void TestDayOfWeekInvalid() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.UpdateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);
            var meal = new Meal {
                MealId = 10,
                DayOfWeek = (DayOfWeek)8,
                MealType = MealType.BREAKFAST,
                Recipe = new Recipe {
                    ApiId = 40,
                    Image = "a",
                    ImageType = "a",
                    Title = "a"
                }
            };
            Assert.Throws<ArgumentException>(() => service.UpdateMeal(meal));
        }

        [Fact]
        public void TestNullRecipe() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.UpdateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);
            var meal = new Meal {
                MealId = 1,
                DayOfWeek = DayOfWeek.Monday,
                MealType = MealType.BREAKFAST,
                Recipe = null
            };
            Assert.Throws<ArgumentNullException>(() => service.UpdateMeal(meal));
        }

        [Fact]
        public void TestRecipeApiIdZero() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.UpdateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);
            var meal = new Meal {
                MealId = 10,
                DayOfWeek = (DayOfWeek)8,
                MealType = MealType.BREAKFAST,
                Recipe = new Recipe {
                    ApiId = 0,
                    Image = "a",
                    ImageType = "a",
                    Title = "a"
                }
            };
            Assert.Throws<ArgumentException>(() => service.UpdateMeal(meal));
        }

        [Fact]
        public void TestRecipeImageNull() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.UpdateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);
            var meal = new Meal {
                MealId = 10,
                DayOfWeek = (DayOfWeek)8,
                MealType = MealType.BREAKFAST,
                Recipe = new Recipe {
                    ApiId = 10,
                    Image = null,
                    ImageType = "a",
                    Title = "a"
                }
            };
            Assert.Throws<ArgumentException>(() => service.UpdateMeal(meal));
        }

        [Fact]
        public void TestRecipeImageEmpty() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.UpdateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);
            var meal = new Meal {
                MealId = 10,
                DayOfWeek = (DayOfWeek)8,
                MealType = MealType.BREAKFAST,
                Recipe = new Recipe {
                    ApiId = 10,
                    Image = "",
                    ImageType = "a",
                    Title = "a"
                }
            };
            Assert.Throws<ArgumentException>(() => service.UpdateMeal(meal));
        }

        [Fact]
        public void TestRecipeImageTypeNull() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.UpdateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);
            var meal = new Meal {
                MealId = 10,
                DayOfWeek = (DayOfWeek)8,
                MealType = MealType.BREAKFAST,
                Recipe = new Recipe {
                    ApiId = 10,
                    Image = "a",
                    ImageType = null,
                    Title = "a"
                }
            };
            Assert.Throws<ArgumentException>(() => service.UpdateMeal(meal));
        }

        [Fact]
        public void TestRecipeImageTypeEmpty() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.UpdateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);
            var meal = new Meal {
                MealId = 10,
                DayOfWeek = (DayOfWeek)8,
                MealType = MealType.BREAKFAST,
                Recipe = new Recipe {
                    ApiId = 10,
                    Image = "a",
                    ImageType = "",
                    Title = "a"
                }
            };
            Assert.Throws<ArgumentException>(() => service.UpdateMeal(meal));
        }

        [Fact]
        public void TestRecipeTitleNull() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.UpdateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);
            var meal = new Meal {
                MealId = 10,
                DayOfWeek = (DayOfWeek)8,
                MealType = MealType.BREAKFAST,
                Recipe = new Recipe {
                    ApiId = 10,
                    Image = "a",
                    ImageType = "a",
                    Title = null
                }
            };
            Assert.Throws<ArgumentException>(() => service.UpdateMeal(meal));
        }

        [Fact]
        public void TestRecipeTitleEmpty() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.UpdateMeal(It.IsAny<Meal>())).Returns(new Meal());
            var service = new MealPlanService(mealPlanDao.Object);
            var meal = new Meal {
                MealId = 10,
                DayOfWeek = (DayOfWeek)8,
                MealType = MealType.BREAKFAST,
                Recipe = new Recipe {
                    ApiId = 10,
                    Image = "a",
                    ImageType = "a",
                    Title = ""
                }
            };
            Assert.Throws<ArgumentException>(() => service.UpdateMeal(meal));
        }
    }
}
