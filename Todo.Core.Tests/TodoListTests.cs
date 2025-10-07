using Xunit;
using Todo.Core;
using System.Linq;
using System.IO;

namespace Todo.Core.Tests
{
    public class TodoListTests
    {
        [Fact]
        public void Add_IncreasesCount()
        {
            var list = new TodoList();
            list.Add("  task  ");
            Assert.Equal(1, list.Count);
            Assert.Equal("task", list.Items.First().Title);
        }

        [Fact]
        public void Remove_ById_Works()
        {
            var list = new TodoList();
            var item = list.Add("a");
            Assert.True(list.Remove(item.Id));
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void Find_ReturnsMatches()
        {
            var list = new TodoList();
            list.Add("Buy milk");
            list.Add("Read book");
            var found = list.Find("buy").ToList();
            Assert.Single(found);
            Assert.Equal("Buy milk", found[0].Title);
        }

        [Fact]
        public void Save()
        {
            var list = new TodoList();
            list.Add("Task 1");
            list.Add("Task 2");
            string testFile = "test_save.json";
            list.Save(testFile);
            Assert.True(File.Exists(testFile));
            var content = File.ReadAllText(testFile);
            Assert.Contains("Task 1", content);
            Assert.Contains("Task 2", content);
        }

        [Fact]
        public void Load()
        {
            var originalList = new TodoList();
            var task = originalList.Add("Test task");
            task.MarkDone();
            string testFile = "test_load.json";
            originalList.Save(testFile);
            var loadedList = new TodoList();
            loadedList.Load(testFile);
            Assert.Equal(1, loadedList.Count);
            Assert.Equal("Test task", loadedList.Items.First().Title);
            Assert.True(loadedList.Items.First().IsDone);
        }
    }
}