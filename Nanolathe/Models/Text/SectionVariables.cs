using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanolathe.Models.Text
{
    internal class SectionVariables : Dictionary<string, object>
    {
        public TValue Get<TValue>(string key)
            => (TValue)this[key];

        public void Set<TValue>(string key, TValue value)
            => this[key] = value;
    }
}
