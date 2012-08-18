using System;

namespace ISL.Server
{
    public class SpecialValue
    {
        SpecialValue()
        {
        }

        SpecialValue(uint icurrentMana)
        {
            currentMana = icurrentMana;

        }

        uint currentMana;
    }
}

