using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonPatch
{
    public class JsonPatchDocument
    {
        private List<JsonPatchOperation> _operations = new List<JsonPatchOperation>();

        public List<JsonPatchOperation> Operations => _operations;

        public void Add(JsonPatchOperation operation)
        {
            _operations.Add(operation);
        }
    }
}