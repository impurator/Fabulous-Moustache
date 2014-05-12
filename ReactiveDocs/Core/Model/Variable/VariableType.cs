using System.ComponentModel;

namespace ReactiveDocs.Core.Model.Variable
{
    public enum VariableType
    {
        [Description("Basic")]
        Basic = 0,
        [Description("Integer")]
        Integer = 1,
        [Description("Decimal")]
        Float = 2,
        [Description("Boolean")]
        Boolean = 3,
        [Description("Set of Strings")]
        StringSet = 4
    }
}
