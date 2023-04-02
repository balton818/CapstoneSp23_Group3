using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Dao {

    [ExcludeFromCodeCoverage]
    public static class Connection {

        public static readonly string ConnectionString = "Server=tcp:mysqlserver33.database.windows.net,1433;Initial Catalog=capstone;Persist Security Info=False;User ID=azureuser;Password=Group32023;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static readonly string SpoonacularApiKey = "a6357910c31647db95aab67ff34a1dc1";
    }
}
