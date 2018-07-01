using System;

public static class EnumUtilities
{
    public static bool HasFlag(this Enum enumerationValue,Enum checkValue)
    {
        int valueRepresentation = Convert.ToInt32(enumerationValue);
        int checkValueRepresentation = Convert.ToInt32(checkValue);
        return (valueRepresentation & checkValueRepresentation) == checkValueRepresentation;
    }
}