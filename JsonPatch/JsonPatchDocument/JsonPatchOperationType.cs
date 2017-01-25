using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonPatch
{
    public enum JsonPatchOperationType
    {
        [Description("add")]
        Add = 0,

        [Description("remove")]
        Remove = 1,

        [Description("replace")]
        Replace = 2,

        [Description("move")]
        Move = 3,

        [Description("copy")]
        Copy = 4,

        [Description("test")]
        Test = 5
    }
}