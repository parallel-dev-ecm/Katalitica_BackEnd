using System;

namespace Katalitica_API.Controllers
{
    public class ParameterResource
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public ParameterResource(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string GetStringValue()
        {
            return Value?.ToString();
        }
    }
}
