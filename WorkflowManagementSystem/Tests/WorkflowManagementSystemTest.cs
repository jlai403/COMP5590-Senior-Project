using System;
using System.Configuration;
using NUnit.Framework;
using WebMatrix.WebData;
using WorkflowManagementSystem.Models.DataAccess;
using WorkflowManagementSystem.Models.User;

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

        [TestFixtureTearDown]
        public void TearDownFixture()
        {
            DatabaseManager.Instance.Delete();
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
                DatabaseManager.Instance._testCleanUpListener = new TestCleanUpListener();
                DatabaseManager.Instance.Initialize(new WorkflowManagementSystemDbContext(connectionString));
                
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