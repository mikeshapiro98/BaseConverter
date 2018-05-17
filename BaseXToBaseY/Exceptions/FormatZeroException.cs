using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseXToBaseY.Exceptions
{
    public class FormatZeroException : Exception
    {
        public int OriginBase { get; set; }
        public int TargetBase { get; set; }

        public FormatZeroException(int originBase, int targetBase)
        {
            OriginBase = originBase;
            TargetBase = targetBase;
        }
    }
}