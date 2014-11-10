using System;
using System.Configuration;
using NUnit.Framework;
using WebMatrix.WebData;
using WorkflowManagementSystem.Models.DataAccess;

namespace WorkflowManagementSystem.Tests
{
    [TestFixture]
    public class WorkflowManagementSystemTest
    {
        [SetUp]
        public void SetUp()
        {
           
        }

        [TearDown]
        public void TearDown()
        {
            DatabaseManager.Instance.CleanUp();
        }
    }

    [SetUpFixture]
    public class TestAssemblyFixture
    {
        [SetUp]
        public void SetupAssembly()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["TestDbContext"].ConnectionString;
                var workflowManagementSystemDbContext = new WorkflowManagementSystemDbContext(connectionString);
                workflowManagementSystemDbContext._testCleanUpInitializer = new TestCleanUpInitializer();
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