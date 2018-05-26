using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseConverter.Domain
{
    public class Number
    {
        public int originBase { get; set; }
        public List<char> originNumeralSystem { get; set; }
        public string originNumeralSystemName { get; set; }
        public int targetBase { get; set; }
        public List<char> targetNumeralSystem { get; set; }
        public string input { get; set; }
        public bool inputNegative { get; set; }
        public char[] inputArray { get; set; }
        public double inputAsDecimal { get; set; }
        public string inputAsDecimalString { get; set; }
        public char[] inputAsDecimalArray { get; set; }
        public string targetResult { get; set; }
    }
}
