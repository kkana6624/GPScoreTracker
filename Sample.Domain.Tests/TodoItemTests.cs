using Sample.Domain;

namespace Sample.Domain.Tests
{
    public class TodoItemTests
    {
        [Fact]
        public void Test1()
        {
            var id = Guid.NewGuid();
            var title = "Test Todo Item";

            var todoItem = new TodoItem(id, title);

            Assert.Equal(id, todoItem.Id);
            Assert.Equal(title, todoItem.Title);
            Assert.False(todoItem.IsCompleted);
        }

        [Fact]
        public void CanMarkTodoItemAsCompleted()
        {
            var id = Guid.NewGuid();
            var title = "Test Todo Item";
            var todoItem = new TodoItem(id, title);

            todoItem.MarkAsCompleted();
            Assert.True(todoItem.IsCompleted);
        }
    }
}