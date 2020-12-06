using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSerializer.DataModel
{
    struct ObjStruct
    {
        public ObjStruct(Object value)
        {
            this.value = value;
            this.guid = Guid.NewGuid();
        }
        public Object value;
        public Guid guid;

    }
}
