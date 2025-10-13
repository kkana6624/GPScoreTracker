using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.ValueObjects
{
    public record class TodoTitle
    {
        public string Value { get; init; }
        public TodoTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Title cannot be null or empty.", nameof(value));
            }
            Value = value;
        }
    }
}
