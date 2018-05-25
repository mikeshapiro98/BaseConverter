using System;

namespace BaseXToBaseY.Exceptions
{
    public class OriginNumeralSystemLacksCharacterException : Exception
    {
        public string NumeralSystemName { get; set; }

        public OriginNumeralSystemLacksCharacterException(string numeralSystemName)
        {
            NumeralSystemName = numeralSystemName;
        }
    }
}