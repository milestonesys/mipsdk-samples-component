using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigAPIClient.Panels.PropertyUserControls
{
    internal class TagItem
    {
        public string Name { get; set; }
        public object Value { get; set; }

        internal TagItem(String name, object value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
