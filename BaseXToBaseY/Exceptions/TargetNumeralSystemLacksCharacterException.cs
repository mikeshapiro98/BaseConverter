using System;

namespace BaseXToBaseY.Exceptions
{
    public class TargetNumeralSystemLacksCharacterException : Exception
    {
        public string NumeralSystemName { get; set; }

        public TargetNumeralSystemLacksCharacterException(string numeralSystemName)
        {
            NumeralSystemName = numeralSystemName;
        }
    }
}