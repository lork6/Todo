using backend.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace backend.Test
{
    [TestClass]
    class RestUnitTest
    {
        private static readonly TodoItem[] todoItems = new[]
        {
            new TodoItem("1", 0, 0,new System.DateTime(), "00"),
            new TodoItem("2", 0, 1,new System.DateTime(), "00"),
            new TodoItem("3", 0, 2,new System.DateTime(), "00"),
        };
        [TestMethod]
        public async Task GetAllTodoItems()
        {
            using (var testScope = TestRestHelper.Create())
            {
                testScope.AddSeedEntities(todoItems);
                var client = testScope.CreateClient();
                var response = await client.GetAsync("api/TodoItems");

                response.EnsureSuccessStatusCode();
                var actual = await response.Content.ReadFromJsonAsync<TodoItem[]>();

                Assert.IsNotNull(actual);
                CollectionAssert.AreEquivalent(todoItems, actual);
            }
        }
        [TestMethod]
        public async Task GetAllTodoItems_DoneReturnNone()
        {
            using (var testScope = TestRestHelper.Create())
            {
                testScope.AddSeedEntities(todoItems);
                var client = testScope.CreateClient();
                var response = await client.GetAsync("api/TodoItems/done");

                response.EnsureSuccessStatusCode();
                var actual = await response.Content.ReadFromJsonAsync<TodoItem[]>();

                Assert.IsNotNull(actual);
                Assert.AreEqual(0, actual.Length);
            }
        }
        [TestMethod]
        public async Task GetOneTodoItemsSoccess()
        {
            using (var testScope = TestRestHelper.Create())
            {
                testScope.AddSeedEntities(todoItems);
                var client = testScope.CreateClient();

                foreach (var expected in testScope.GetDbTableContent<TodoItem>())
                {
                    var response = await client.GetAsync($"api/TodoItems/{expected.Id}");

                    response.EnsureSuccessStatusCode();
                    var actual = await response.Content.ReadFromJsonAsync<TodoItem>();

                    Assert.IsNotNull(actual);
                    Assert.AreEqual(expected, actual);
                }
            }
        }
    }
}
