using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSerializer.DataModel
{
    struct DataStruct
    {
        public DataStruct(string className,string name,string value)
        {
            this.className = className;
            this.name = name;
            this.value = value;
        }
        public string className;
        public string name;
        public string value;

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(className+ "|"+name+"|"+value);
            return stringBuilder.ToString();
        }
    }
}
