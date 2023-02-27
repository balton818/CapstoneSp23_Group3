namespace RecipePlannerApi.Dao.Interface
{
    public interface IAppDao
    {
        public List<string> getAppCuisines();

        public List<string> getAppDiets();

        public List<string> getAppMealTypes();
    }
}
