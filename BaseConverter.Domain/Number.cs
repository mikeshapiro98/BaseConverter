using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseConverter.Domain
{
    public class Number
    {
        public static readonly List<char> MasterNumeralSystem = new List<char>() { '1', '0', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '/', ':', ';', '(', ')', '$', '&', '@', '"', ',', '?', '!', '\'', '[', ']', '{', '}', '#', '%', '^', '*', '+', '=', '_', '\\', '|', '~', '<', '>', '€', '£', '¥', '•', '₽', '¢', '₩', '§', '¿', '¡', 'ß' };

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
