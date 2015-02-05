using System;
using System.Configuration;
using NUnit.Framework;
using WebMatrix.WebData;
using WorkflowManagementSystem.Models.DataAccess;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Tests
{
    [TestFixture]
    public class WorkflowManagementSystemTest
    {
        [SetUp]
        public void SetUp()
        {
            SecurityManager._webSecurity = new TestWebSecurity();
        }

        [TearDown]
        public void TearDown()
        {
            DatabaseManager.Instance.CleanUp();
        }
    }

    [SetUpFixture]
    public class TestSetUpFixture
    {
        [SetUp]
        public void SetupAssembly()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["TestDbContext"].ConnectionString;
                
                var workflowManagementSystemDbContext = new WorkflowManagementSystemDbContext(connectionString);
                DatabaseManager.Instance._testCleanUpListener = new TestCleanUpListener();
                DatabaseManager.Instance._newUnitOfWorkCreationStrategy = new TestUnitOfWorkCreationStategy();

                DatabaseManager.Instance.Delete(workflowManagementSystemDbContext);
                DatabaseManager.Instance.Initialize(workflowManagementSystemDbContext);

                if (!WebSecurity.Initialized)
                {
                    WebSecurity.InitializeDatabaseConnection("TestDbContext", "Users", "Id", "Email", autoCreateTables: true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine(e.StackTrace);
                throw e;
            }

        }
    }
}