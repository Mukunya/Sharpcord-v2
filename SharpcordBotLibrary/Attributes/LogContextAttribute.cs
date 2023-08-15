using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Struct|AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
    public sealed class LogContextAttribute : Attribute
    {
        readonly string context;

        public LogContextAttribute(string context)
        {
            this.context = context;
        }

        public string Context
        {
            get { return context; }
        }

    }
}
