using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Tests.ValueObject
{
    public class ToDoTitleTests
    {
        [Fact]
        public void CanCreateValidTodoTitle()
        {
            var validTitle = "Valid Title";
            var todoTitle = new TodoTitle(validTitle);
            Assert.Equal(validTitle, todoTitle.Value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCreateInvalidTodoTitle(string invalidTitle)
        {
            Assert.Throws<ArgumentException>(() => new TodoTitle(invalidTitle));
        }
    }
}
