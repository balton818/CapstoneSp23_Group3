namespace RecipePlannerApi.Service.Interface
{
    public interface IAppService
    {
        public List<string> getAppCuisines();

        public List<string> getAppDiets();

        public List<string> getAppMealTypes();
    }
}
