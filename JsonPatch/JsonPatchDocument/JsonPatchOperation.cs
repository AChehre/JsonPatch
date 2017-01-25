using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonPatch
{
    public struct JsonPatchOperation
    {
        public JsonPatchOperationType Operation { get; set; }

        public string FromPath { get; set; }

        public string Path { get; set; }

        public object Value { get; set; }
    }
}