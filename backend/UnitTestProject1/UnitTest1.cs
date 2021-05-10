using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly Todo[] TestTodoItem = new[]
        {
            new TodoItem("1",0,0,new DateTime(),"test"),
            new TodoItem("2",0,1,new DateTime(),"test2"),
            new TodoItem("3",0,2,new DateTime(),"test3"),
            
        };
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
