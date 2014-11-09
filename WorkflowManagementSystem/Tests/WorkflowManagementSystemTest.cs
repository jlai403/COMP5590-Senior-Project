using System;
using System.Configuration;
using MyEntityFramework.Transaction;
using NUnit.Framework;
using WebMatrix.WebData;

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
            DatabaseManager.Instance.Delete();
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