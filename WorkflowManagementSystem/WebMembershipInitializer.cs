using WebMatrix.WebData;

namespace WorkflowManagementSystem
{
    public static class WebMembershipInitializer
    {

        public static void Initialize()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("WorkflowManagementSystemDbContext", "Users", "Id", "Email", autoCreateTables: true);
            }
        }
    }
}