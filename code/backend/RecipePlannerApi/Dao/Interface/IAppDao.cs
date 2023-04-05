namespace RecipePlannerApi.Dao.Interface
{
    /// <summary>
    /// Interface IAppDao
    /// </summary>
    public interface IAppDao
    {
        /// <summary>
        /// Gets the list application cuisines from the database.
        /// </summary>
        /// <returns>List of cuisines</returns>
        public List<string> getAppCuisines();

        /// <summary>
        /// Gets the list application diets from the database.
        /// </summary>
        /// <returns>List of diets</returns>
        public List<string> getAppDiets();

        /// <summary>
        /// Gets the list application meal types from the database.
        /// </summary>
        /// <returns>List of meal types</returns>
        public List<string> getAppMealTypes();
    }
}
