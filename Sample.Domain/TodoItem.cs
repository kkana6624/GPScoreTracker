using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain
{
    public class TodoItem
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public bool IsCompleted { get; private set; }
        
        public TodoItem(Guid id, string title)
        {
            Id = id;
            Title = title;
            IsCompleted = false;
        }

        public void MarkAsCompleted()
        {
            IsCompleted = true;
        }
    }
}
