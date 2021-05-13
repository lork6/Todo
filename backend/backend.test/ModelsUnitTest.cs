using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using backend.Test;

namespace backend.test
{
    [TestClass]
    public class ModelsUnitTest
    {
        [TestMethod]
        public void IsTodoItemModelExist()
        {
            using (var dbConn = TestDbHelper.CreateConnection())
            {
                var dbContext = TestDbHelper.CreateDbContext(dbConn);

                var entityType = dbContext.Model.FindEntityType(typeof(Models.TodoItem));
                Assert.IsNotNull(entityType, "TodoItem entitas nem ismert a DbContext-ben");
            }
        }
    }
}
