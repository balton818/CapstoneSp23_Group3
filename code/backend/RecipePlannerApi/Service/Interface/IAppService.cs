namespace RecipePlannerApi.Service.Interface
{
    /// <summary>
    /// Interface IAppService
    /// </summary>
    public interface IAppService
    {
        /// <summary>
        /// Gets the application's cuisines.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> getAppCuisines();

        /// <summary>
        /// Gets the application's diets.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> getAppDiets();

        /// <summary>
        /// Gets the application's meal types.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> getAppMealTypes();
    }
}
